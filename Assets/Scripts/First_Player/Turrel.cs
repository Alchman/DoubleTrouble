using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : MonoBehaviour
{
    [SerializeField]private float RadiusCanon = 50f;
    public LayerMask layerMask;
    
    [Header("Audio")]
    [SerializeField] private AudioClip shootSound;

    private Vector3 direction;
    private float nextFire;

    Weapon activeWeapon;
    private Collider target;

    private float angle;
    private AudioSource audioSource;

    
    Dictionary<BulletType, int> bullets = new Dictionary<BulletType, int>();
    // Start is called before the first frame update
    void Start()
    {
        nextFire = 1f;
        activeWeapon = GetComponent<Weapon>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemy();
        AutoShooting();
    }

    private void CheckEnemy()
    {
        // Debug.Log("Check");
        Collider[] allItemsInRadius =
            Physics.OverlapSphere(transform.position, RadiusCanon,
                                  layerMask); //массив для всех кто попадет в радиус поражения  оружия
        float minDistance = float.MaxValue;
      
        foreach (Collider itemEnemyPos in allItemsInRadius)
        {
           
            var distance =
                Vector3.Distance(transform.position,
                                 itemEnemyPos.transform.position); // определить дситанцию от своей позиции до позиции врага из OverlapSphere 
            // target = itemEnemyPos;
            direction = itemEnemyPos.transform.position -
                        transform
                           .position;                                         // вычелсить направления движения врага  для того что бы бросить в него луч
            Debug.DrawRay(transform.position, direction, Color.yellow, 0.1f); // желтый луч для визуального отслеживания
        
                if (distance < minDistance)
                {
                    
                    // Определить врага когда  он попал в круг сферы (синий)  и записать в таргет (для стрельбы)
                    target = itemEnemyPos;
                    minDistance = distance;
              
                }
            
        }
    }

    private void AutoShooting()
    {
        if (target == null)
        {
            return;
        }

        Shoot();
        // Debug.Log("Shot");
     
     
    }


    private void Shoot()
    {
        //  метод для выстрел по врагу
        nextFire -= Time.deltaTime;
        if (nextFire <= 0)
        {
           
            activeWeapon.Fire(target.transform.position);
            // Debug.Log("Shoot");
            nextFire = activeWeapon.fireRate;
            audioSource.PlayOneShot(shootSound);
        }
      
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadiusCanon);
    }
}
