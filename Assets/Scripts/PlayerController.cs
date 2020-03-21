using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  
    [SerializeField] float moveSpeed;
    [SerializeField] float coefSpeed;//Range
    [SerializeField] float health;
    [SerializeField] float forcePush;
    [SerializeField] float damagePush;
    [SerializeField] float forcePushForEnemy;
   
   
    [SerializeField] Transform things;
    [SerializeField] Transform thingsWithHealth;
    [SerializeField] float radiusForPush;
    [SerializeField] Transform enemy;

    [SerializeField] float radiusCheck;
    [SerializeField] float maxDistanceForCheck;

    public LayerMask layerMask;
   [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightY;

    private float speedPlayer;

    PushThings pushThings;
    Rigidbody rigidbody;
    DamageDealer damageDealer;
    PushThingsWithHealth pushThingsWithHealth;
   

    // Start is called before the first frame update
    void Start()
    {
       
        rigidbody = GetComponent<Rigidbody>();
        damageDealer = FindObjectOfType<DamageDealer>();
       
     
    }

    // Update is called once per frame
    void Update()
    {
        CheckEnemy();
       /* PushAway();
        PushThingsWithHelath();
        PushEnemy();*/
    }

    void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed;
       direction = Vector3.ClampMagnitude(direction, moveSpeed);
        speedPlayer = direction.magnitude;
        if (direction != Vector3.zero)
        {
          // Debug.Log(direction.magnitude);
             rigidbody.velocity = direction;
         //   rigidbody.MovePosition(transform.position + direction * Time.deltaTime);
            rigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }
    }



    private void CheckEnemy()
    {
        if ((Input.GetKeyDown(KeyCode.F)))
        {
            Collider[] allItemsInRadius = Physics.OverlapSphere(transform.position, radiusCheck, layerMask);

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

            PushThings pushThings = target.GetComponent<PushThings>();
            if (pushThings != null)
            {
                var direction = target.transform.position - transform.position;
                if (target.gameObject.tag == "Enemy")
                {
                    pushThings.Push(direction.normalized * forcePushForEnemy);
                }
                if (target.gameObject.tag == "ThingsForPush")
                {
                    direction.y += hightY;
                    pushThings.Push(direction.normalized * forcePush * (1 + coefSpeed * speedPlayer));
                }
            }
          PushThingsWithHealth  pushThingsWithHealth = target.GetComponent<PushThingsWithHealth>();
            if (pushThingsWithHealth != null)
            {
                var direction = thingsWithHealth.position - transform.position;
                pushThingsWithHealth.Damage();
            }
        }

    }
    /*  public void PushAway()
      {
          if (Input.GetKeyDown(KeyCode.F))

          {
            
              Debug.Log(direction + "Things");
              if ()
              {
                  direction.y += hightY;
                  Debug.DrawLine(things.position, things.position + direction);
                  pushThings.Push(direction.normalized *forcePush* (1 + coefSpeed * speedPlayer));
                  Debug.Log("Bum");
              }

          }
      }
      public void PushThingsWithHelath()
      {
          if (Input.GetKeyDown(KeyCode.F))

          {
              var direction = thingsWithHealth.position - transform.position;
              Debug.Log(direction + "ThingsWithHEalth");
              if (direction.sqrMagnitude < radiusForPush * radiusForPush)
              {
                  pushThingsWithHealth.Damage();
                  Debug.Log("Damage");
              }
          }

      }
      public void PushEnemy()
      {
          if (Input.GetKeyDown(KeyCode.F))
          {

              var direction = enemy.position - transform.position;
              Debug.Log(direction + "Enemy");
              if (direction.sqrMagnitude < radiusForPush * radiusForPush)
              {

                  Debug.DrawLine(enemy.position, enemy.position + direction);
                  pushThings.Push(direction.normalized * forcePushForEnemy);
                  Debug.Log("Bum Enemy");
              }

          }



      }*/

    public float GetHealth()
    {
        return health;
    }
    public float GetDamage()
    {
        return damagePush;
    }
     private void OnDrawGizmosSelected()
     {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, radiusForPush);
     }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.LogError(collision);
            health -= damageDealer.GetDamage();
        }
    }
}
