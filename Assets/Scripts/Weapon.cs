using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    [SerializeField] Camera camera;
    [SerializeField] float range = 100f;
    [SerializeField] float damage = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] Ammo ammoSlot;
    [SerializeField] float timeBetweenShots = 0.5f;
    [SerializeField] AmmoType ammoType;
    [SerializeField] TextMeshProUGUI ammoText;
    [SerializeField] Animator weaponAnimator;
    [SerializeField] float bulletSpread = 0.1f;
    [SerializeField] int numberOfPellets = 10;


    public bool canShoot = true;

    public enum WeaponType
    {
        RegularGun,
        AssaultRifle,
        Shotgun
    }

    [SerializeField] WeaponType weaponType;

    private void OnEnable()
    {
        canShoot = true;
    }

    void Update()
    {
        DisplayAmmo();

        if (Input.GetMouseButtonDown(0) && canShoot == true)
        {
            StartCoroutine(Shoot());

        }
    }

    IEnumerator Shoot()
    {
        canShoot = false;

        if (ammoSlot.GetCurrentAmmo(ammoType) > 0)
        {
            PlayMuzzleFlash();
            GetComponent<AudioSource>().Play();

            switch (weaponType)
            {
                case WeaponType.RegularGun:
                    ProcessRaycast();
                    // Here you can also handle additional logic for this weapon if needed
                    break;
                case WeaponType.AssaultRifle:
                    ProcessRaycast();
                    // Here you can also handle additional logic for this weapon if needed
                    break;
                case WeaponType.Shotgun:
                    for (int i = 0; i < numberOfPellets; i++)
                    {
                        ProcessRaycast();
                        // Here you can also handle additional logic for this weapon if needed
                    }
                    break;
            }

            ammoSlot.ReduceCurrentAmmo(ammoType);
            weaponAnimator.SetTrigger("shoot");
        }

        yield return new WaitForSeconds(timeBetweenShots);
        weaponAnimator.ResetTrigger("shoot");
        canShoot = true;
    }

    void ProcessRaycast()
    {
        Vector3 direction = GetSpreadDirection();
        RaycastHit hit;
        if (Physics.Raycast(camera.transform.position, direction, out hit, range))
        {
            CreateHitImpact(hit);
            EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
            if (target == null) return;
            target.TakeDamage(damage);
        }
        else
        {
            return;
        }
    }

    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }


    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, .1f);
    }

    void DisplayAmmo()
    {
        int currentAmmo = ammoSlot.GetCurrentAmmo(ammoType);
        ammoText.text = currentAmmo.ToString();
    }

    Vector3 GetSpreadDirection()
    {
        Vector3 direction = camera.transform.forward;
        direction.x += Random.Range(-bulletSpread, bulletSpread);
        direction.y += Random.Range(-bulletSpread, bulletSpread);
        return direction;
    }
}
