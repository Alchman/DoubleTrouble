using System.Collections.Generic;
using UnityEngine;

enum StateSecondPlayer{
    Start,
    Atack,
    Reload
}

public class SecondPlayer : MonoBehaviour{
    private Rigidbody rb;
    private Enemy     enemy;

    Dictionary<BulletType, int> bulets = new Dictionary<BulletType, int>();
    
    public                   float     sphereRadius;
    public                   float     maxDistance;
    public                   LayerMask layerMask;
    private                  Collider  target;
    private                  float     futureTimeForTarget;
    private                  Weapon    activeWeapon;
    private                  Bullet    bullet;
    private StateSecondPlayer currientStateSecondPlayer;
 
    [SerializeField] private Transform Bullet;
    [SerializeField] private float     ReloadBullet = 1f;
    [SerializeField] private float     RadiusCanon  = 50f;

    void Start() {
        activeWeapon              =  GetComponent<Weapon>();
        bullet              =  FindObjectOfType<Bullet>();
        rb                  =  GetComponent<Rigidbody>();
        futureTimeForTarget += Time.time + 1f;
        activeWeapon.magzineSize  =  15f;
    }

    void Update() {
        
        switch(currientStateSecondPlayer) {
            case StateSecondPlayer.Start:
                print("StateSecondPlayer.Start");
                currientStateSecondPlayer = StateSecondPlayer.Atack;
                break;

            case StateSecondPlayer.Atack :
                print("StateSecondPlayer.Atack ");
                CheckEnemy();
                AutoShooting();
             
                break;
            case StateSecondPlayer.Reload :
                print("StateSecondPlayer.Reload");
                break;
        }
        
    }

    private void CheckEnemy() {
        Collider[] allItemsInRadius = Physics.OverlapSphere(transform.position, RadiusCanon, layerMask);

        float minDistance = float.MaxValue;

        foreach(Collider item in allItemsInRadius) {
            var distance = Vector3.Distance(transform.position, item.transform.position);
            if(distance < minDistance) {
                target      = item;
                minDistance = distance;
            }
        }
    }

    private void AutoShooting() {
        if(target == null) {
            return;
        }

        transform.LookAt(target.transform.position);
        BulletFly();
    }

    private void BulletFly() {
        if(Time.time > futureTimeForTarget) {
            MagazineWeapon();
            activeWeapon.magzineSize--;
            Instantiate(Bullet, transform.position, transform.rotation);
            futureTimeForTarget = Time.time + ReloadBullet;
            print(activeWeapon.magzineSize + " magazine");
        }
    }

    private void MagazineWeapon() {
        if(activeWeapon.magzineSize <= 0) {
            print("закончилось патроны");
            currientStateSecondPlayer = StateSecondPlayer.Reload;
        }
    }
}
