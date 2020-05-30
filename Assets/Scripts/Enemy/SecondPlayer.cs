using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum StateSecondPlayer{
    Start,
    Atack,
    Reload
}

public class SecondPlayer : GenericSingletonClass<SecondPlayer>{
    private Rigidbody rb;

    public LayerMask layerMask;
    public Weapon    activeWeapon;
 
    [Header("Bullets")] public int pistolBullets;
    public                     int rifleBullets;
    public int rocketBullets;

    private Collider          target;
    private float             nextFire;
    private StateSecondPlayer currentStateSecondPlayer;


    Dictionary<BulletType, int> bullets = new Dictionary<BulletType, int>();
    Dictionary<ResourceType, int> resourses = new Dictionary<ResourceType, int>();
 
    public int gearsCount;
    public int woodCount;
    public int metalCount;
    public int stoneCount;
    public int regenCount;
    Health health;

    public ResoursesUI tableResourses;

   


    [SerializeField] private float RadiusCanon = 50f; //TODO move to weapon class

    void Start() {
        bullets.Add(BulletType.PISTOL, pistolBullets);
        bullets.Add(BulletType.RIFLE,  rifleBullets);
        bullets.Add(BulletType.ROCKET,  rocketBullets);

        resourses.Add(ResourceType.GEARS, gearsCount);
        resourses.Add(ResourceType.WOOD, woodCount);
        resourses.Add(ResourceType.METAL, metalCount);
        resourses.Add(ResourceType.STONE, stoneCount);
        resourses.Add(ResourceType.REGEN, regenCount);

        health = GetComponent<Health>();

     
        rb       = GetComponent<Rigidbody>();
        health.OnDeath += DoDeath;
        nextFire = 1f;

    }

    void Update() {
        switch(currentStateSecondPlayer) {
            case StateSecondPlayer.Start :
                // print("StateSecondPlayer.Start");
                currentStateSecondPlayer = StateSecondPlayer.Atack;
                break;

            case StateSecondPlayer.Atack :
                // print("StateSecondPlayer.Atack ");
                CheckEnemy();
                AutoShooting();

                break;
            case StateSecondPlayer.Reload :
                // print("StateSecondPlayer.Reload");
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
        Shoot();
    }

    private void Shoot() {
        nextFire -= Time.deltaTime;
        if(nextFire <= 0) {
            if(bullets[activeWeapon.bulletType] < 0) {
                //no bullets
                //print("no bullets");
                return;
            }

            bullets[activeWeapon.bulletType]--;
            activeWeapon.Fire(transform.rotation);

            if(activeWeapon.NeedsReload()) {
                //play sound
                //enable animation
                activeWeapon.Reload();
                //print("Reload");
                if(bullets[activeWeapon.bulletType] > 0) {
                    nextFire = activeWeapon.reloadTime;
                }
                else {
                    //TODO change weapon

                    //state -> NO BULLETS
                   // print("//state -> NO BULLETS");
                    nextFire = float.PositiveInfinity;
                }
            }
            else {
                nextFire = activeWeapon.fireRate;
            }
        }
    }

  public void AddResourses(ResourceType resourceType, int amount )
    {
        resourses[resourceType] += amount;
       
    }

    public void AddAmmo(BulletType bulletType, int amount)
    {
        bullets[bulletType] += amount;
    }

    public void DoDeath()
    {
       gameObject.SetActive(false);
        print("Game Over");
    }

    public void HealthUpdate(int count)
    {
        health.ChangeHealth(count);
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            tableResourses.Show() ;
        }
    }

   
    private void OnTriggerExit(Collider other)
    { 
       
        tableResourses.Hide();



    }

    public int GetResourses(ResourceType resourceType)
    {
        return resourses[resourceType];
    }

    public int GetBullets(BulletType bulletType)
    {
        return bullets[bulletType];
    }




}