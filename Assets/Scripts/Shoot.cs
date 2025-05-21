using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Threading;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public LayerMask mask;

    public InputActionProperty shootAction;

    public AudioClip shootSoundClip;

    public AudioSource source;

    public float cooldownTime;

    private bool canShoot = true;

    private Coroutine cooldownCoroutine;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Shooting();

    }


    public void Shooting()
    {
        float shootActivate = shootAction.action.ReadValue<float>();

        if (shootActivate == 1.0f && canShoot)
        {

            StartCooldownCoroutione();

        }
    }

    public void PlayAudio()
    {
        source.clip = shootSoundClip;
        source.Play();

    }

    public void Fire()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 2000000, mask, QueryTriggerInteraction.Ignore))
        {
            Destroy(hitInfo.transform.gameObject);
        }

    }

    private IEnumerator Cooldown()
    {
        canShoot = false;
        PlayAudio();
        Fire();
    
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;

    }

    public void StartCooldownCoroutione()
    {
        if(cooldownCoroutine != null)
        {
            StopCoroutine(cooldownCoroutine);
        }
        StartCoroutine(Cooldown());
        
    }

}