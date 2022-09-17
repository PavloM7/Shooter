using UnityEngine;
using System.Collections;

public class ShootingAK47 : MonoBehaviour
{
    [SerializeField] private AudioSource audioS;
    [SerializeField] private AudioClip shootingAudio;

    [SerializeField] private Transform bulletPosition;

    [SerializeField] private ParticleSystem shootingEffect;

    [Range(0.01f, 0.5f)]
    [SerializeField] private float fireSpeed = 0.08f;

    [Range(0.1f, 5)]
    [SerializeField] private float reloadTime = 2.5f;

    [Range(1, 100)] private int damage = 8;

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

        _Shooting.DrawAmmo(ammoInMagazine);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (isCoroutine == false)
            {
                StartCoroutine(FireRate());
            }
        }
    }

    private IEnumerator FireRate()
    {
        isCoroutine = true;
        
        ammoInMagazine--;

        _Shooting.DrawAmmo(ammoInMagazine);

        SoundAndEffects();

        _Shooting.Shoot(damage);
        yield return new WaitForSeconds(fireSpeed);


        //Check reloading
        if (ammoInMagazine <= 0)
        {
            yield return new WaitForSeconds(reloadTime);
            ammoInMagazine = magazine;

            _Shooting.DrawAmmo(ammoInMagazine);
        }

        isCoroutine = false;
    }

    private void SoundAndEffects()
    {
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
