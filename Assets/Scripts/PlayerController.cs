using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Tooltip("Скорость движения")] [SerializeField] float moveSpeed;
    [Tooltip("Коеф зависящий от скорости при ударе предмета ")] [Range (0,5)] [SerializeField] float coefSpeed;
    [Tooltip("Коеф зависящий от скорости при ударе врага ")] [Range (0,5)] [SerializeField] float coefSpeedEnemy;
    [Tooltip("Кол-во здоровья")] [SerializeField] float health;
    [Tooltip("Сила удара ")] [SerializeField] float forcePush;
    [Tooltip("Сила удара врага")] [SerializeField] float forcePushForEnemy;
    [SerializeField] float damageFoot;
   
    [Tooltip("Радиус проверки рядом предметов")] [SerializeField] float radiusCheck;
   [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightY;

    public LayerMask layerMask;

    [Tooltip("динамическая скорость игрока")] private float speedPlayer;

    Rigidbody rigidbody;
    DamageDealer damageDealer;
  
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
             rigidbody.velocity = direction;
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
                if (target.gameObject.tag == "ThingsForPush")
                {
                    direction.y += hightY;
                    pushThings.Push(direction.normalized * forcePush * (1 + coefSpeed * speedPlayer));
                    
                }
            }
            PushEnemy pushEnemy = target.GetComponent<PushEnemy>();
            if (pushEnemy != null)
            {
                var direction = target.transform.position - transform.position;
                if (target.gameObject.tag == "Enemy")
                {
                    Debug.Log("find enemy");
                    pushEnemy.Push(direction.normalized * forcePushForEnemy*(1 + coefSpeedEnemy * speedPlayer));
                    pushEnemy.Damage(damageFoot);
                }
            }

            PushThingsWithHealth  pushThingsWithHealth = target.GetComponent<PushThingsWithHealth>();
            if (pushThingsWithHealth != null)
            {
              
                pushThingsWithHealth.Damage(damageFoot);
            }
        }

    }
  
    public float GetHealth()
    {
        return health;
    }
    
    
    

}
