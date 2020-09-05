using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum StateSecondPlayer
{
    Start,
    Atack,
    Reload
}

public class SecondPlayer : GenericSingletonClass<SecondPlayer>
{
    public LayerMask layerMask;
    [Tooltip("активное оружие")] public Weapon activeWeapon;

    [Header("Bullets")]
    [Tooltip("Пули для пистолета")]
    public int pistolBullets;

    [Tooltip("Пули для винтовки")] public int rifleBullets;
    [Tooltip("Пули для рокетницы")] public int rocketBullets;

    private Collider target;
    private float nextFire;
    private StateSecondPlayer currentStateSecondPlayer;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator barricadeAnimator;
    [SerializeField][Tooltip("эффект для выстрела из пистолета")] private ParticleSystem effectShoot;

    Dictionary<BulletType, int> bullets = new Dictionary<BulletType, int>();
    Dictionary<ResourceType, int> resourses = new Dictionary<ResourceType, int>();

    public int gearsCount;
    public int woodCount;
    public int metalCount;
  


    Health health;
    [Tooltip("Сила с которой выкинется камень вторым игроком")] [SerializeField] float stoneThrowForce;
    private Vector3 direction;
    private Vector3 directon2;
    private float angle;

    [Tooltip("Время через которое выкинет камень второй игрок")] [SerializeField] float delayForceTime;
    Coroutine delayForce;


    [Tooltip("радиус поражения для оружия")]
    [SerializeField]
    private float RadiusCanon = 50f; //TODO move to weapon class


    [Header("Sounds")] 
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private AudioClip noAmmoSound;
    
    public Health Health
    {
        get { return health; }
    }
    
    private bool hasAmmo; //just for sound effect
    
    void Start()
    {
        bullets.Add(BulletType.PISTOL, pistolBullets);
        bullets.Add(BulletType.RIFLE, rifleBullets);
        bullets.Add(BulletType.ROCKET, rocketBullets);

        resourses.Add(ResourceType.GEARS, gearsCount);
        resourses.Add(ResourceType.WOOD, woodCount);
        resourses.Add(ResourceType.METAL, metalCount);
      
        health = GetComponent<Health>();
    
        health.OnDeath += DoDeath;
        health.OnDamage += DoDamage;
      
        directon2 = Quaternion.AngleAxis(80, Vector3.up) * transform.forward;
        angle = Vector3.Angle(transform.forward, directon2);
    }

    void Update()
    {
        // Debug.DrawRay(transform.position, transform.forward * 30f, Color.green, 0.1f); //взгляд от плеера ровно  вперед

        switch (currentStateSecondPlayer)
        {
            case StateSecondPlayer.Start:

                currentStateSecondPlayer = StateSecondPlayer.Atack;
                break;

            case StateSecondPlayer.Atack:

                CheckEnemy();
                AutoShooting();

                break;
            case StateSecondPlayer.Reload: 
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 30f); //смотрим прямо
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, RadiusCanon);

        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, directon2 * 30); //смотрим по углу
    }

    private void CheckEnemy()
    {
        Collider[] allItemsInRadius =
            Physics.OverlapSphere(transform.position, RadiusCanon,
                                  layerMask); //массив для всех кто попадет в радиус поражения  оружия
        float minDistance = float.MaxValue;
        float normolizeAngle = 0;
        foreach (Collider itemEnemyPos in allItemsInRadius)
        {
            var distance =
                Vector3.Distance(transform.position,
                                 itemEnemyPos.transform.position); // определить дситанцию от своей позиции до позиции врага из OverlapSphere 
            // target = itemEnemyPos;
            direction = itemEnemyPos.transform.position -
                        transform
                           .position;                                         // вычелсить направления движения врага  для того что бы бросить в него луч
            //Debug.DrawRay(transform.position, direction, Color.yellow, 0.1f); // желтый луч для визуального отслеживания
            float angleEnemy = Vector3.SignedAngle(transform.forward, direction, Vector3.up); // вычеслить угол относительно врага и ветора вперед - зеленый луч

            if (angleEnemy > 1 && angleEnemy < angle)
            {
                // если враг попал у угол обстрела, то начать стрельбу 

                if (distance < minDistance)
                {
                    // Определить врага когда  он попал в круг сферы (синий)  и записать в таргет (для стрельбы)
                    //Debug.DrawRay(transform.position, direction, Color.yellow, 1f);
                    target = itemEnemyPos;
                    minDistance = distance;
                    normolizeAngle = angleEnemy / angle;
                }
            }
        }

        animator.SetFloat("direction_blend", normolizeAngle);
        // direction = target.transform.position - transform.position;
        // Debug.DrawRay(transform.position, direction, Color.yellow, 0.2f);
    }

    private void AutoShooting()
    {
        // автострельба - если есть враг есть на сцене
        if (target == null)
        {
            // print("No target");
            return;
        }

        // transform.LookAt(target.transform.position);
        Shoot();
    }

    private void Shoot()
    {
        //  метод для выстрел по врагу
        nextFire -= Time.deltaTime;
        if (nextFire <= 0)
        {
            if (bullets[activeWeapon.bulletType] <= 0)
            {
                //no bullets
                //print("no bullets");
                if (hasAmmo)
                {
                    audioSource.PlayOneShot(noAmmoSound);
                    hasAmmo = false;
                }
                return;
            }

            hasAmmo = true;

            bullets[activeWeapon.bulletType]--;
            activeWeapon.Fire(target.transform.position);
            audioSource.PlayOneShot(shootSound);
            effectShoot.Play();
            animator.SetTrigger("shoot");

            if (activeWeapon.NeedsReload())
            {
                //play sound
                //enable animation
                activeWeapon.Reload();
                //print("Reload");
                if (bullets[activeWeapon.bulletType] > 0)
                {
                    nextFire = activeWeapon.reloadTime;
                }
                else
                {
                    //TODO change weapon

                    //state -> NO BULLETS
                    // print("//state -> NO BULLETS");
                    nextFire = float.PositiveInfinity;
                }
            }
            else
            {
                nextFire = activeWeapon.fireRate;
            }
        }
    }

    public void AddResourses(ResourceType resourceType, int amount)
    {
        resourses[resourceType] += amount;
    }
    public void MinusResourses(ResourceType resourceType, int amount)
    {
        resourses[resourceType] -= amount;
    }

    public void AddAmmo(BulletType bulletType, int amount)
    {
        bullets[bulletType] += amount;
    }

    public void DoDeath()
    {
        gameObject.SetActive(false);
        // print("Game Over");
    }

    public void DoDamage()
    {
        barricadeAnimator.SetTrigger("barrier_hit");
    }

    public void HealthUpdate(int count)
    {
        health.ChangeHealth(count);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Stone")
        {
            Ejection(other.gameObject, delayForceTime, stoneThrowForce);
        }
    }

    public void Ejection(GameObject gameObject , float delay,float force)
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        gameObject.transform.position = transform.position;
        StartCoroutine(DelayForce(rb, delay, force));

    }

    IEnumerator DelayForce(Rigidbody rigidbody, float delay, float force)
    {
        yield return new WaitForSeconds(delay);
        Vector3 dir = transform.forward;
        dir.y = 1;
        rigidbody.AddForce(dir * force);

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
