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
   [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightY;

    private float speedPlayer;

    PushThings pushThings;
    Rigidbody rigidbody;
    DamageDealer damageDealer;
    PushThingsWithHealth pushThingsWithHealth;
   

    // Start is called before the first frame update
    void Start()
    {
        pushThingsWithHealth = FindObjectOfType<PushThingsWithHealth>();
        rigidbody = GetComponent<Rigidbody>();
        damageDealer = FindObjectOfType<DamageDealer>();
        pushThings = FindObjectOfType<PushThings>();
     
    }

    // Update is called once per frame
    void Update()
    {
        PushAway();
        PushThingsWithHelath();
        PushEnemy();
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

    public void PushAway()
    {
        if (Input.GetKeyDown(KeyCode.F))

        {
            var direction = things.position - transform.position;
            Debug.Log(direction + "Things");
            if (direction.sqrMagnitude < radiusForPush * radiusForPush)
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
            Debug.Log(("Enter Push enemy"));
            var direction = enemy.position - transform.position;
            Debug.Log(direction + "Enemy");
            if (direction.sqrMagnitude < radiusForPush * radiusForPush)
            {
              
                Debug.DrawLine(enemy.position, enemy.position + direction);
                pushThings.Push(direction.normalized * forcePushForEnemy);
                Debug.Log("Bum");
            }

        }
                


    }

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
