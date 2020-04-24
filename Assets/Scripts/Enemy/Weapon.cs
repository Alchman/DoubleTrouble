using System;
using UnityEngine;

public class Weapon : MonoBehaviour{
    private Bullet bullet;
    private BulletType bulletType;
   
    public float  magzineSize {get; set;}
    public float buletSpeed {get; set;}
    public float bulletDamage {get; set;}
    public float reloadTime {get; set;}
    public float fireRate {get; set;}

    private void Start() {
        bullet = FindObjectOfType<Bullet>();
    }

    private void Fire() {
        bullet.FireDamege(51f, 30f);
    }
}
