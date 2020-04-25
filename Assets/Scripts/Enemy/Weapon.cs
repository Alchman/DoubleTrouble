using System;
using UnityEngine;

public class Weapon : MonoBehaviour{
    private Bullet bullet;
    private BulletType bulletType;

    public float magzineSize;
    public float buletSpeed;
    public float bulletDamage;
    public float reloadTime;
    public float fireRate;

    private void Start() {
        bullet = FindObjectOfType<Bullet>();
    }

    public void Fire() {
        bullet.FireDamege(bulletDamage, buletSpeed);
    }
}
