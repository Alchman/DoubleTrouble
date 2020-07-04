using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPlayer : GenericSingletonClass<FirstPlayer>
{
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

    [Tooltip("Проверка если ли 'земля' под ногами")] [SerializeField] Transform groundCheck;

    [Tooltip("На чем стоит игрок")] [SerializeField] LayerMask whatIsGround;
    bool isGrounded = true;


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

   // Rigidbody rigidbody;
    Health health;
    PlayerStates currentState;

    CharacterController characterController;

    [SerializeField] float jumpHeight = 20f;
  
    [SerializeField] float gravityAcceleration = 1f;
    [SerializeField] float groundCheckRadius = 0.5f;
    float gravity;

    enum PlayerStates
    {
        IDLE,
        MOVE,
        RUN
    }
    void Start()
    {

        health = GetComponent<Health>();
        characterController = GetComponent<CharacterController>();
        health.OnDeath += DoDeath;
        startSpeed = moveSpeed;
        currentState = PlayerStates.IDLE;

    }

  private  void Update()
    {

        CheckEnemy();
       
        Jump();
        Move();
   //     ApplyGravity();


    }

   /* private void FixedUpdate()
    {
        Move();
        ApplyGravity();

    }*/

  
    private void Move()
    {

        float inputForward = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        float inputRight = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
       
        Vector3 direction;
      
        direction = transform.forward * inputForward;
        direction += transform.right * inputRight;
        speedPlayer = direction.magnitude;
        if (direction != Vector3.zero)
        {
            
           
       
            transform.rotation
                = Quaternion.LookRotation(direction);
         
            if (Input.GetButton("Fire3"))
            {
               

                currentState = PlayerStates.RUN;
            }
        }
        else
        {
            currentState = PlayerStates.IDLE;
        }

      
        bool isGrounded = Physics.CheckSphere(transform.position,radiusCheck,whatIsGround);
        if (isGrounded)
        {
            Debug.Log(isGrounded);
            if (gravity < 0)
            {
                gravity = gravityScale;
            }
            if (Input.GetButtonDown("Jump"))
            {
               
                gravity = jumpHeight;
                Debug.Log("Jump");
               
            }
        }
        else
        {
            gravity += gravityScale
                * Time.deltaTime;
        }
       
        direction.y = gravity * Time.deltaTime +
            (gravityScale * Time.deltaTime * Time.deltaTime) / 2;
        characterController.Move(direction);



    }
    private void OnDrawGizmosSelected()
    {
        //draw ground check
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }

        private void Jump()
    {
       // Vector3 gravity = gravityScale * Vector3.up;
       // characterController.SimpleMove(gravity);

      
        

       /*     isGrounded = Physics.Linecast(transform.position, groundCheck.position, whatIsGround);
        if (isGrounded)
        {
            if (gravity < 0)
            {
                gravity = gravityScale;
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gravity = jumpHeight;
                isGrounded = false;
            }
          
        }
        else
        {
            gravity += gravityScale
                * Time.deltaTime;
        }
        Vector3 moveDirection;
        moveDirection.y = gravity * Time.deltaTime +
            (gravityScale * Time.deltaTime * Time.deltaTime) / 2;
        characterController.Move(moveDirection);

*/



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

    }
    private void OnTriggerExit(Collider other)
    {
        SpeedModificator speedInPlane = other.gameObject.GetComponent<SpeedModificator>();
        if (!speedInPlane)
        {
            moveSpeed = startSpeed;
        }
       
    }

   

    public void DoDeath()
    {
        Destroy(gameObject);
    }

}
