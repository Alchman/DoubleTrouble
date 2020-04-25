using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    public LayerMask pushMask;

    [Tooltip("Радиус проверки рядом предметов")] [SerializeField] float radiusCheck;
    [Tooltip("Сила  удара ")] [SerializeField] float forcePush;
    [SerializeField] float forcePushOnRun;
    
    [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightYforRun;
    [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightYforShot;
    [Tooltip("Урон по врагу ударом ногой")] [SerializeField] float damageFoot;
    [Tooltip("динамическая скорость игрока")] private float speedPlayer;
    [Tooltip("Скорость движения")] [SerializeField] float moveSpeed;
    [Tooltip("Коеф зависящий от скорости влияющий на силу удара предмета ")] [Range(0, 5)] [SerializeField] float coefSpeed;
    [SerializeField] Transform capsulePosition1;
    [SerializeField] Transform capsulePosition2;
    Rigidbody rigidbody;
    Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody>();
        health.OnDeath += DoDeath;
      
    }

    // Update is called once per frame
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
            //pushable
            Pushable pushable = target.GetComponent<Pushable>();
          
            if (pushable != null)
            {
                Vector3 direction = CalculateDirection(target.transform.position, forcePush,hightYforShot);
                pushable.Push(direction);
                Debug.Log("Do Push");
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

    private void OnCollisionEnter(Collision collision)
    {

        Pushable pushable = collision.gameObject.GetComponent<Pushable>();
        if (pushable != null && pushable.PushOnRun)
        { 
            Vector3 direction = CalculateDirection(collision.transform.position, forcePushOnRun,hightYforRun);
                pushable.Push(direction);
            //pushible.Push with push on run force
        }
        
    }
   public void DoDeath()
    {
        Destroy(gameObject);
    }

}
