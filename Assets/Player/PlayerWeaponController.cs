using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeaponController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public bool shootingUnlocked = false;
    public bool canShoot = false;
    public float bulletSpeed;

    public GunMeterController gunMeterController;

    void Start() {
        if (!shootingUnlocked) return;
        StartCoroutine(StartShootingCooldown());
    }
    void Update() {
        if (!shootingUnlocked) return;
        if (Input.GetMouseButtonDown(0) && canShoot) {
            Shoot();
        }
    }

    void Shoot() {
        canShoot = false;
        StartCoroutine(StartShootingCooldown());
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 bulletDirection = (mousePosition - transform.position).normalized;
        Vector3 bulletSpawn = transform.position + 0.5f * bulletDirection;
        Quaternion bulletRotation = Quaternion.LookRotation(bulletDirection, transform.up);
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawn, bulletRotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection.normalized * bulletSpeed;
        Destroy(bullet, 3f);
    }

    IEnumerator StartShootingCooldown() {
        gunMeterController.StartTimer(10f);
        yield return new WaitForSeconds(10.0f);
        canShoot = true;
    }
}
