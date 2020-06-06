using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : GenericSingletonClass<FirstPlayer>
{
    public LayerMask pushMask;

    [Header("Radius")]
    [Tooltip("Радиус проверки рядом предметов")] [SerializeField] float radiusCheck=1;

    [Header("Force")]
    [Tooltip("Сила  удара ")] [SerializeField] float forcePush=10;
    [SerializeField] float forcePushOnRun=3;

    [Header("Hight")]
    [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightYforRun= 1.5f;
    [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightYforShot=2f;
    
    [Header("Damage")]
    [Tooltip("Урон по врагу ударом ногой")] [SerializeField] float damageFoot=10;
   
    [Header("Speed")]
    [Tooltip("Скорость движения")] [SerializeField] float moveSpeed=7;
    [Tooltip("Динамическая скорость игрока")] private float speedPlayer;

    [Header("Coef")]
    [Tooltip("Коеф зависящий от скорости влияющий на силу удара предмета ")] [Range(0, 5)] [SerializeField] float coefSpeed=0.32f;
   
    [Header("Position")]
    [Tooltip("Позиция 1 круга видимости предметов перед игроком ")] [SerializeField] Transform capsulePosition1;
    [Tooltip("Позиция 2 круга видимости предметов перед игроком ")] [SerializeField] Transform capsulePosition2;
   
    Rigidbody rigidbody;
    Health health;
  
    void Start()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody>();
        health.OnDeath += DoDeath;
    }

    void Update()
    {
        CheckEnemy();
        Move();
    }

    public void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed;
        direction = Vector3.ClampMagnitude(direction, moveSpeed);
        speedPlayer = direction.magnitude;
        if (direction != Vector3.zero)
        {
            rigidbody.velocity = direction;
            rigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }
    }

    public void CheckEnemy()
    {
        if ((Input.GetButtonDown("Fire1")))
        {
            Collider[] allItemsInRadius = Physics.OverlapCapsule(capsulePosition1.position, capsulePosition2.position, radiusCheck, pushMask);;
            float minDistance = float.MaxValue;
            Collider target = null;
            foreach (Collider item in allItemsInRadius)
            { 
                var distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < minDistance)
                {
                    target = item;
                    minDistance = distance;
                }
            }
            if (target == null)
            {
                return;
            }
            Pushable pushable = target.GetComponent<Pushable>();
            if (pushable != null)
            {
                Vector3 direction = CalculateDirection(target.transform.position, forcePush,hightYforShot);
                pushable.Push(direction);
            }
            DamagebleByPush damagebleByPush = target.GetComponent<DamagebleByPush>();
            if (damagebleByPush != null)
            {
                damagebleByPush.DoDamage(damageFoot);
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(capsulePosition1.position, radiusCheck);
        Gizmos.DrawWireSphere(capsulePosition2.position, radiusCheck);
    }
    Vector3 CalculateDirection(Vector3 from, float forcePush,float hightY)
    {
        var direction = from - transform.position;
        direction.y += hightY;
      direction= direction * forcePush * (1 + coefSpeed * speedPlayer);
        return direction;
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Pushable pushable = collision.gameObject.GetComponent<Pushable>();
    //    if (pushable != null && pushable.PushOnRun)
    //    { 
    //        Vector3 direction = CalculateDirection(collision.transform.position, forcePushOnRun,hightYforRun);
    //            pushable.Push(direction);
    //    }   
    //}

    private void OnTriggerEnter(Collider other)
    {
        Pushable pushable = other.gameObject.GetComponent<Pushable>();
        if (pushable != null && pushable.PushOnRun)
        {
            Vector3 direction = CalculateDirection(other.transform.position, forcePushOnRun, hightYforRun);
            pushable.Push(direction);
        }
    }

    public void DoDeath()
    {
        Destroy(gameObject);
    }

}
