using System.Collections;
using UnityEngine;

public class ProgressBar: MonoBehaviour {
    private Coroutine fillCoroutine;
    private RectTransform barTransform;

    [SerializeField] private float maxWidth = 1080f;
    

    private void OnEnable() {
        FlareLight.OnFlareDespawn += StartFilling;
    }

    private void OnDisable() {
        FlareLight.OnFlareDespawn -= StartFilling;
    }

    private void Awake() {
        barTransform = GetComponent<RectTransform>();
    }

    public void SetProgress( float progress ) {
        float clamped = Mathf.Clamp01( progress );
        barTransform.sizeDelta = new Vector2( clamped * maxWidth, barTransform.sizeDelta.y );
    }

    public void StartFilling( float duration ) {
        if( fillCoroutine != null ) {
            StopCoroutine( fillCoroutine );
        }

        fillCoroutine = StartCoroutine( FillOverTime( duration ) );
    }


    private IEnumerator FillOverTime( float duration ) {
        int steps = 0;
        int maxSteps = 10;
        float stepDuration = duration / ( maxSteps - 1 );

        while( steps < maxSteps ) {
            steps++;
            float progress = steps / (float)maxSteps;
            SetProgress( progress );

            yield return new WaitForSeconds( stepDuration );
        }

        SetProgress( 1 / (float)maxSteps );
    }

}
