using System;
using UnityEngine;
using Lean.Pool;

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

    public void Fire(Quaternion rotation)
    {
        magazineBulletLeft--;
        
        Bullet bullet = LeanPool.Spawn(bulletPrefab, transform.position, rotation);
        bullet.FireBullet(bulletDamage, buletSpeed);
    }
    
    public void Reload()
    {
        magazineBulletLeft = magazineSize;
    }

    public bool NeedsReload()
    {
        return magazineBulletLeft <= 0;
    }
}
