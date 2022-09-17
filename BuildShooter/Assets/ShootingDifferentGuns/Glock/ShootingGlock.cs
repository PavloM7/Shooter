using UnityEngine;
using System.Collections;

public class ShootingGlock : MonoBehaviour
{
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip shootingAudio;

    [SerializeField] private Animator animator;

    [SerializeField] private Transform bulletPosition;

    [SerializeField] private ParticleSystem shootingEffect;

    [Range(0, 0.5f)]
    [SerializeField] private float fireSpeed = 0.08f;
    [Range(0, 30)]
    [SerializeField] private int burst = 8;

    [Range(0, 1)]
    [SerializeField] private float timeBetweenBrust = 0.3f;
    [Range(0.1f, 5)]
    [SerializeField] private float reloadTime = 2.2f;

    [Range(1, 100)]
    [SerializeField] private int damage = 100; // FIX (damage always = 8)

    private int ammoUsed = 0;
    [Range(1, 100)]
    [SerializeField] private int magazine = 30;
    private int ammoInMagazine;

    [SerializeField] private GameObject Shooting_obj;
    private Shooting _Shooting;

    private bool isCoroutine;

    private void Awake()
    {
        _Shooting = Shooting_obj.GetComponent<Shooting>();

        ammoInMagazine = magazine;
    }

    private void OnEnable()
    {
        isCoroutine = false;
        ammoUsed = 0;

        _Shooting.DrawAmmo(ammoInMagazine);
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isCoroutine == false)
            {
                StartCoroutine(FireRate());
            }
        }
        else if (Input.GetKeyDown("r"))
        {
            StartCoroutine(Reloading());
        }
    }

    private IEnumerator Reloading()
    {
        yield return new WaitForSeconds(reloadTime);
        ammoInMagazine = magazine;
        ammoUsed = 0;

        _Shooting.DrawAmmo(ammoInMagazine);
    }

    private IEnumerator FireRate()
    {
        isCoroutine = true;

        ammoUsed++;
        ammoInMagazine--;

        _Shooting.DrawAmmo(ammoInMagazine);

        SoundAndEffects();

        _Shooting.Shoot(damage);
        yield return new WaitForSeconds(fireSpeed);


        //Check reloading
        if (ammoInMagazine <= 0)
        {
            yield return new WaitForSeconds(reloadTime);
            StartCoroutine(Reloading());
        }

        //Check brust
        if (ammoUsed >= burst)
        {
            yield return new WaitForSeconds(timeBetweenBrust);
            
            ammoUsed = 0;
        }

        isCoroutine = false;
    }

    private void SoundAndEffects()
    {
        animator.SetTrigger("Kickback");

        Effects();
        Audio();

        void Effects()
        {
            shootingEffect.Play();

        }
        void Audio()
        {
            audioS.PlayOneShot(shootingAudio);
        }
    }
}
