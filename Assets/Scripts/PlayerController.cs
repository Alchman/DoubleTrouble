using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  
    [SerializeField] float moveSpeed;
    [SerializeField] float health;
    [SerializeField] float forcePush;
    [SerializeField] Transform player;
    [SerializeField] Transform target;
    [SerializeField] float radiusForPush;
    [SerializeField] float hightY;
   





     PushThings pushThings;
    Rigidbody rigidbody;
    DamageDealer damageDealer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        damageDealer = FindObjectOfType<DamageDealer>();
        pushThings = FindObjectOfType<PushThings>();
    }

    // Update is called once per frame
    void Update()
    {
        PushAway();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * moveSpeed;
       direction = Vector3.ClampMagnitude(direction, moveSpeed);

        if (direction != Vector3.zero)
        {
            /* rigidbody.velocity = direction;*/
            rigidbody.MovePosition(transform.position + direction * Time.deltaTime);

            rigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }
    }

    public void PushAway()
    {
        if (Input.GetKeyDown(KeyCode.F))

        {
            //проверять предметы в определенном радиусе
           // Debug.Log("Push");
            var direction = target.position - player.position;
            Debug.Log(direction);
            if (direction.sqrMagnitude < radiusForPush * radiusForPush)
            {
                direction.y += hightY;
                Debug.DrawLine(target.position, target.position + direction);
                pushThings.Push(direction.normalized * forcePush);
                Debug.Log("Bum");

            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Debug.LogError(collision);
            health-=  damageDealer.GetDamage();
        }
    }

    public float GetHealth()
    {
        return health;
    }
 
    
     private void OnDrawGizmosSelected()
   {
       Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, radiusForPush);
   }

}
