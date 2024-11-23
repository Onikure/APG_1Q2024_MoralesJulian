using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform cameraHolder;
    [SerializeField] private float damage = 10f;
    [SerializeField] private float fireRate = 0.1f;
    [SerializeField] private int magazineSize = 30;
    [SerializeField] private float reloadTime = 1.5f;

    private int currentAmmo;
    private bool isReloading = false;
    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = magazineSize;
    }

    void Update()
    {
        if (isReloading) return;

        if (currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        if (currentAmmo <= 0) return;

        currentAmmo--;
        RaycastHit hit;
    }

    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = magazineSize;
        isReloading = false;
    }
}
