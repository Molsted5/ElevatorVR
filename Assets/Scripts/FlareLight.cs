using UnityEngine;
using System.Collections;

public class FlareLight: MonoBehaviour {
    private Vector3 offsetPosition;
    private Light pointLight;
    private Coroutine fadeCoroutine;
    private Coroutine despawnCoroutine;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    
    public GameObject flarePrefab;
    public float grabFadeDuration = 1f;
    public float releaseFadeDuration = 1f;
    public float grabTargetIntensity = 3f;
    public float releaseTargetIntensity = 8f;
    public float despawnDelay = 5f;
    public delegate void FlareDespawnHandler( float delay );
    public static event FlareDespawnHandler OnFlareDespawn;

    private void Awake() {
        pointLight = GetComponent<Light>();
        Initialize();
    }

    private void Start() {
        offsetPosition = transform.position - transform.parent.position;
    }

    private void LateUpdate() {
        transform.position = transform.parent.position + offsetPosition;
        transform.rotation = Quaternion.identity;
    }

    public void GrabLightFade() {
        if( fadeCoroutine != null ) {
            StopCoroutine( fadeCoroutine );
        }

        if( despawnCoroutine != null ) {
            StopCoroutine( despawnCoroutine );
            despawnCoroutine = null;
        }

        fadeCoroutine = StartCoroutine( FadeLightIntensity(grabFadeDuration, grabTargetIntensity) );
    }

    public void Initialize() {
        initialPosition = transform.parent.position;
        initialRotation = transform.parent.rotation;

        pointLight.intensity = 0;
        // perhaps add color or other values that might be overwritten in editor
    }

    public void ReleaseLightFade() {
        if( fadeCoroutine != null ) {
            StopCoroutine( fadeCoroutine );
        }

        fadeCoroutine = StartCoroutine( FadeLightIntensity(releaseFadeDuration, releaseTargetIntensity) );

        if( despawnCoroutine != null ) {
            StopCoroutine( despawnCoroutine );
        }

        despawnCoroutine = StartCoroutine( Respawn( despawnDelay ) );
    }

    private IEnumerator FadeLightIntensity(float duration, float targetIntensity) {
        float startIntensity = pointLight.intensity;
        float counter = 0f;

        while( counter < duration ) {
            pointLight.intensity = Mathf.Lerp( startIntensity, targetIntensity, counter / duration );
            counter += Time.deltaTime;
            yield return null;
        }

        pointLight.intensity = targetIntensity;
    }

    private IEnumerator Respawn( float delay ) {
        OnFlareDespawn?.Invoke( delay );

        yield return new WaitForSeconds( delay );

        Instantiate( flarePrefab, initialPosition, initialRotation );
        Destroy( transform.parent.gameObject );
    }
}
