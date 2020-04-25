using System.Collections.Generic;
using UnityEngine;

enum StateSecondPlayer{
    Start,
    Atack,
    Reload
}

public class SecondPlayer : MonoBehaviour
{
    private Rigidbody rb;

    public                   LayerMask layerMask;
    public                  Weapon    activeWeapon;

    [Header("Bullets")]
    public int pistolBullets;
    public int rifleBullets;

    private                  Collider  target;
    private                  float     nextFire;
    private StateSecondPlayer currentStateSecondPlayer;

    Dictionary<BulletType, int> bullets = new Dictionary<BulletType, int>();

    [SerializeField] private float     RadiusCanon  = 50f; //TODO move to weapon class

    void Start() {
        bullets.Add(BulletType.PISTOL, pistolBullets);
        bullets.Add(BulletType.RIFLE, rifleBullets);

        rb                  =  GetComponent<Rigidbody>();
        nextFire = 1f;
    }

    void Update() {
        
        switch(currentStateSecondPlayer) {
            case StateSecondPlayer.Start:
                print("StateSecondPlayer.Start");
                currentStateSecondPlayer = StateSecondPlayer.Atack;
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
        //BulletFly();
        Shoot();
    }

    private void Shoot()
    {
        nextFire -= Time.deltaTime;
        if (nextFire <= 0)
        {
            if (bullets[activeWeapon.bulletType] < 0)
            {
                //no bullets
                return;
            }

            bullets[activeWeapon.bulletType]--;
            activeWeapon.Fire();

            if (activeWeapon.NeedsReload())
            {
                //play sound
                //enable animation
                if (bullets[activeWeapon.bulletType] > 0)
                {
                    nextFire = activeWeapon.reloadTime;
                }
                else
                {
                    //TODO change weapon

                    //state -> NO BULLETS
                    nextFire = float.PositiveInfinity;
                }
            }
            else
            {
                nextFire = activeWeapon.fireRate;
            }
        }

    }

    //private void BulletFly() {
    //    if(Time.time > nextFire) {
    //        MagazineWeapon();
    //        activeWeapon.magzineSize--;
    //        nextFire = Time.time + activeWeapon.reloadTime;
    //        print(activeWeapon.magzineSize + " magazine");
    //    }
    //}

    //private void MagazineWeapon() {
    //    if(activeWeapon.magzineSize <= 0) {
    //        print("закончилось патроны");
    //        currentStateSecondPlayer = StateSecondPlayer.Reload;
    //    }
    //}
}
