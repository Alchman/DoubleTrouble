using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : MonoBehaviour
{
    public LayerMask pushMask;

    [Tooltip("Радиус проверки рядом предметов")] [SerializeField] float radiusCheck;
    [Tooltip("Сила  удара ")] float forcePush;
    [SerializeField] Health health;
    [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightY;
    [Tooltip("Урон по врагу ударом ногой")] [SerializeField] float damageFoot;
    [Tooltip("динамическая скорость игрока")] private float speedPlayer;
    [Tooltip("Скорость движения")] [SerializeField] float moveSpeed;
    [Tooltip("Коеф зависящий от скорости влияющий на силу удара предмета ")] [Range(0, 5)] [SerializeField] float coefSpeed;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
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
        if ((Input.GetKeyDown(KeyCode.F)))
        {
            Debug.Log("Press");
            Collider[] allItemsInRadius = Physics.OverlapSphere(transform.position, radiusCheck, pushMask);

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
            Pushible pushible = target.GetComponent<Pushible>();
          
            if (pushible != null)
            {
                var direction = target.transform.position - transform.position;
                direction.y += hightY;

                pushible.Push(direction * forcePush * (1 + coefSpeed * speedPlayer));
                Debug.Log("Do Push");
            }

            DamagebleByPush damagebleByPush = target.GetComponent<DamagebleByPush>();
            if (damagebleByPush != null)
            {
              
                damagebleByPush.DoDamage(damageFoot);
            }


        }
    }

    Vector3 CalculateDirection(Vector3 forcePush)
    {
        Vector3 direction = Vector3.zero;

        return direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Pushible pushible = collision.gameObject.GetComponent<Pushible>();
        if (pushible != null && pushible.PushOnRun)
        {
            //pushible.Push with push on run force
        }
    }

   public void OnDeath()
    {
        health.OnDeath();
        Destroy(gameObject);
    }

}
