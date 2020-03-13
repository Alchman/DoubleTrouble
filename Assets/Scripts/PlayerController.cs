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




    PushThings pushThings;
    Rigidbody rigidbody;
    DamageDealer damageDealer;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = FindObjectOfType<Rigidbody>();
        damageDealer = FindObjectOfType<DamageDealer>();
        pushThings = FindObjectOfType<PushThings>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PushAway();
    }
    public void Move()
    {

        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")) * moveSpeed;
       direction = Vector3.ClampMagnitude(direction, moveSpeed);

        if (direction != Vector3.zero)
        {
            rigidbody.MovePosition(transform.position + direction * Time.deltaTime);
            rigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }
    }

    public void PushAway()
    {
        if(Input.GetKeyDown(KeyCode.F))

        {
            var direction = target.position - player.position;
            if (direction.sqrMagnitude < radiusForPush * radiusForPush)
            {
                pushThings.Push();
                
            }
        }


    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            
            health= health- damageDealer.GetDamage();
        }
    }

    public float GetHealth()
    {
        return health;
    }
    public float GetForcePush()
    {
        return forcePush;
    }
   
}
