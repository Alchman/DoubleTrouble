using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : GenericSingletonClass<FirstPlayer>
{
    [SerializeField] Animator animator;
    public LayerMask pushMask;

    [Header("Radius")]
    [Tooltip("Радиус проверки рядом предметов")] [SerializeField] float radiusCheck = 1;

    [Header("Force")]
    [Tooltip("Сила  удара ")] [SerializeField] float forcePush = 10;
    [SerializeField] float forcePushOnRun = 3;

    [Header("Hight")]
   
    [Header("Damage")]
    [Tooltip("Урон по врагу ударом ногой")] [SerializeField] float damageFoot = 10;

    [Header("Speed")]
    [Tooltip("Скорость движения")] [SerializeField] float moveSpeed = 7;
    [Tooltip("Динамическая скорость игрока")] private float speedPlayer;

    [Tooltip("Начальная скорость движения")] private float startSpeed;


    [Tooltip("Сила с которой подпрыгнет игрок")] [SerializeField] float jumpForce = 10;
    public float gravityScale = -10;


    [Tooltip("На чем стоит игрок")] [SerializeField] LayerMask whatIsGround;
    [Tooltip("Точка проверки 'земли' под ногами")] [SerializeField] Transform groundCheck;
    [Tooltip("Радиус проверки 'земли' под ногами")] [SerializeField] float groundCheckRadius = 1;
    [Tooltip("Точка проверки 'стены' перед игроком")] [SerializeField] Transform wallCheck;
    [Tooltip("Длина проверки 'стены' перед игроком")] [SerializeField] float wallCheckDistance = 1;
    bool isGrounded = false;


    [Tooltip("Сила пинания с места")] [SerializeField] float forceShotIdle;
    [Tooltip("Сила пинания на бегу")] [SerializeField] float forceShotOnRun;
    [Tooltip("Сила пинания при беге с шифтом")] [SerializeField] float forceShotSpeedUp;

    
    [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightYforShot = 2f;
    [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightYforShotOnRun = 2f;
    [Tooltip("Высота пиннания при беге с шифтом")] [SerializeField] float hightYforShotSpeedUp;





    [Tooltip("Сила пинания при задевании")] [SerializeField] float forceShotOnCollision;
    [Tooltip("Высота на которую кидается предмет при его задевании")] [SerializeField] float hightYOnCollision = 1.5f;


    [Tooltip("Во сколько раз увеличиться скорость при нажатиии на shift")] [Range(0, 2)] [SerializeField] float accelerationSpeed;

    [Header("Coef")]
    [Tooltip("Коеф зависящий от скорости влияющий на силу удара предмета ")] [Range(0, 5)] [SerializeField] float coefSpeed = 0.32f;

    [Header("Position")]
    [Tooltip("Позиция 1 круга видимости предметов перед игроком ")] [SerializeField] Transform capsulePosition1;
    [Tooltip("Позиция 2 круга видимости предметов перед игроком ")] [SerializeField] Transform capsulePosition2;
    

    Rigidbody rigidbody;
    Health health;
    PlayerStates currentState;
    public bool allowInput = true;
    Coroutine activeInput;


    enum PlayerStates
    {
        IDLE,
        MOVE,
        RUN
    }
    void Start()
    {
        health = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody>();
        health.OnDeath += DoDeath;
        startSpeed = moveSpeed;
        currentState = PlayerStates.IDLE;

    }

  private  void Update()
    {

        CheckEnemy();
       
        Jump();


    }

    private void FixedUpdate()
    {
        Move();
        ApplyGravity();

    }

    public void ApplyGravity()
    {
        Vector3 gravity = gravityScale * Vector3.up;
        rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }

    private void Move()
    {
        if (allowInput)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            direction.Normalize();
            //direction = Vector3.ClampMagnitude(direction, moveSpeed);
            speedPlayer = direction.magnitude;
            if (direction.magnitude > 0)
            {
                rigidbody.MoveRotation(Quaternion.LookRotation(direction));

                direction *= moveSpeed;
                currentState = PlayerStates.MOVE;
                animator.SetTrigger("run");

                if (Input.GetButton("Fire3"))
                {
                    direction *= accelerationSpeed;
                    currentState = PlayerStates.RUN;
                }

                if (!isGrounded)
                {
                    bool isWall = Physics.Raycast(wallCheck.position, direction, wallCheckDistance, whatIsGround);
                    if (isWall)
                    {
                        direction = Vector3.zero;
                    }
                }

                direction.y = rigidbody.velocity.y;
                rigidbody.velocity = direction;

            }
            else
            {
                animator.SetTrigger("idle");
                currentState = PlayerStates.IDLE;
            }

        }
        else
        {
            
        }

    }

  
    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, whatIsGround);
            if (isGrounded)
            {
              
                rigidbody.AddForce(new Vector3(0, jumpForce));
                isGrounded = false;
                animator.SetTrigger("jump");
            }

        }



    }

    public void CheckEnemy()
    {
        if ((Input.GetButtonDown("Fire1")))
        {
            Collider[] allItemsInRadius = Physics.OverlapCapsule(capsulePosition1.position, capsulePosition2.position, radiusCheck, pushMask); ;
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
                Vector3 direction= Vector3.zero;
                switch (currentState)
                {
                    case PlayerStates.IDLE:
                         direction = CalculateDirection(target.transform.position, forceShotIdle, hightYforShot);
                       
                        break;
                    case PlayerStates.MOVE:
                        direction = CalculateDirection(target.transform.position, forceShotOnRun, hightYforShotOnRun);
                      
                        break;
                    case PlayerStates.RUN:
                        direction = CalculateDirection(target.transform.position, forceShotSpeedUp, hightYforShotSpeedUp);
                     
                        break;

                }
                animator.SetTrigger("hit_leg");
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


        Gizmos.color = Color.green;
        Gizmos.DrawRay(wallCheck.position, transform.forward * wallCheckDistance);

        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    Vector3 CalculateDirection(Vector3 from, float forcePush, float hightY)
    {
        var direction = from - transform.position;
        direction.y += hightY;
        direction = direction * forcePush * (1 + coefSpeed * speedPlayer);
        return direction;
    }


   

    private void OnTriggerEnter(Collider other)
    {
        Pushable pushable = other.gameObject.GetComponent<Pushable>();
        if (pushable != null && pushable.PushOnRun)
        {
                    Vector3 direction = CalculateDirection(other.transform.position, forcePushOnRun, hightYOnCollision);
                    pushable.Push(direction);     
        }
        SpeedModificator speedInPlane = other.gameObject.GetComponent<SpeedModificator>();
        if (speedInPlane)
        {
           
            moveSpeed *= speedInPlane.GetSpeedFactor(); ;
        }
        PowerUpForce powerUpForce = other.gameObject.GetComponent<PowerUpForce>();
        if (powerUpForce)
        {
            rigidbody.AddForce(transform.up * powerUpForce.GetForce());
        }
        PowerUpSpeed powerUpSpeed = other.gameObject.GetComponent<PowerUpSpeed>();
        if (powerUpSpeed)
        {
            allowInput = false;
            rigidbody.AddForce(powerUpSpeed.GetDirectionSpeed());
            activeInput = StartCoroutine(ActiveInput());
            

            
        }


    }


    private void OnTriggerExit(Collider other)
    {
        SpeedModificator speedInPlane = other.gameObject.GetComponent<SpeedModificator>();
        if (!speedInPlane)
        {
            moveSpeed = startSpeed;
        }
       
    }

    IEnumerator ActiveInput()
    {
        PowerUpSpeed powerUpSpeed = FindObjectOfType<PowerUpSpeed>();
        yield return new WaitForSeconds(powerUpSpeed.GetTimeDeativeInput());
        allowInput = true;

    }

        public void DoDeath()
    {
        Destroy(gameObject);
    }

}
