using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] Bullet bulletPrefab;
    public float buletSpeed;
    public float bulletDamage;
    public BulletType bulletType;

    public int magazineSize;
    public float reloadTime;
    public float fireRate;

    int magazineBulletLeft;

    private void Start()
    {
        magazineBulletLeft = magazineSize;
    }

    public void Fire()
    {
        magazineBulletLeft--;
        Bullet bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        bullet.FireBullet(bulletDamage, buletSpeed);
    }

    public bool NeedsReload()
    {
        return magazineBulletLeft <= 0;
    }
}
