using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int currentHealth = 100;

    private Coroutine invunabilityCoroutine;

    public bool canTakeDMG = true;

    public float invunabilityTime;


    public AudioSource source;
    public AudioClip[] clips;


    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void Start()
    {
        



    }

    // Update is called once per frame
    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
        }


    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }




    public void DMGTaken()
    {


        int damageTaken = 2;

        currentHealth = currentHealth - damageTaken;


        source.clip = clips[0];
        source.Play();
        source.pitch = UnityEngine.Random.Range(.7f, 1f);
        



        if (currentHealth <= 1)
        {
           // GameLost();

        }



    }

    private void GameLost()
    {


    }




    public void StartInvunabilityCoroutione()
    {
        if (invunabilityCoroutine != null)
        {
            StopCoroutine(invunabilityCoroutine);
        }
        StartCoroutine(Invunability());

    }

    private IEnumerator Invunability()
    {
        canTakeDMG = false;
        
        DMGTaken();
       

        yield return new WaitForSeconds(invunabilityTime);
        canTakeDMG = true;

    }


}
