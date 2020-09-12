using System.Collections;
using UnityEngine;
using UnityEngine.AI;
// ReSharper disable All

enum StateEnemy{
    Start,
    Move,
    Atack,
    StopPush,
    Dead
}

public class EnemyController : MonoBehaviour {
    [SerializeField] [Tooltip("Скорость передвижения")]
    private float speed = 1f;

    [SerializeField] [Tooltip("Время стана для моба")]
    private float stunTime = 3;

    [SerializeField] [Tooltip("Дистанция до плеера для атаки")]
    private float playerdistance = 5f;

    [SerializeField] [Tooltip("Частота удара")]
    private float attackRate = 1f;

    [SerializeField] [Tooltip("Сила дамага для энеми по второму персонажу")]
    private float damage = 30f;
    
    public int DamagePlayer { get; set; } 

    [SerializeField] [Tooltip("Аниматор для врага")]
    private Animator animator;
    
    [SerializeField] [Tooltip("Эффект крови")]
    private ParticleSystem effectBlood;
    
    [SerializeField] [Tooltip("Эффект крови")]
    private ParticleSystem effectDeath;

    [Header("Sounds")]
    [SerializeField] private AudioClip dieSound;

    private SecondPlayer secondPlayer;
    private FirstPlayer firstPlayer;
    private Rigidbody    rb;
    private float        distanceToTarget;
    private NavMeshAgent agent;
    private StateEnemy   currientStateEnemy;
    private Health       health;
    private Health       healthSecondPlayer;
    private Health       healthFirstPlayer;
    private float        nextAttack;
    private Pushable     pushable;
    private NavMeshAgent navMeshAgent;
    private Coroutine    waitCoroutineEnemy;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb                  =  GetComponent<Rigidbody>();
        firstPlayer        =   FirstPlayer.Instance;
        secondPlayer        =  SecondPlayer.Instance;
        agent               =  GetComponent<NavMeshAgent>();
        health              =  GetComponent<Health>();
        healthFirstPlayer   =    firstPlayer.GetComponent<Health>();
        healthSecondPlayer  =  secondPlayer.GetComponent<Health>();
        health.OnDeath      += OnDeath;
        health.OnDamage     += BloodHitAffect;
        pushable            =  GetComponent<Pushable>();
        navMeshAgent        =  GetComponent<NavMeshAgent>();
        pushable.PushObject += Push;
    }

    private void Update() {
        switch(currientStateEnemy) {
            case StateEnemy.Start :

                currientStateEnemy = StateEnemy.Move;
                break;

            case StateEnemy.StopPush :
                //print("все я стою");
                // agent.isStopped = true;

                break;

            case StateEnemy.Move :
                agent.isStopped = false;
                MoveToTarget();
                break;
            case StateEnemy.Atack :
                agent.isStopped = true;
                distanceToTarget = Vector3.Distance(transform.position, secondPlayer.transform.position);
                if(distanceToTarget > 7) {
                    currientStateEnemy = StateEnemy.Move;
                }

                TargetAtack();
                break;
            case StateEnemy.Dead:
                break;
        }
    }

    private void Push() {
        //print("pnuli");
        GetComponent<Rigidbody>().isKinematic = false;
        navMeshAgent.enabled                  = false;

        if(waitCoroutineEnemy != null) {
            StopCoroutine(waitCoroutineEnemy);
        }

        waitCoroutineEnemy = StartCoroutine(WaitPush());
        animator.SetTrigger("death");
        currientStateEnemy = StateEnemy.StopPush;
    }

    IEnumerator WaitPush() {
        //print("карутина стою");
        yield return new WaitForSeconds(stunTime);
        if (currientStateEnemy != StateEnemy.Dead)
        {
            navMeshAgent.enabled = true;                                
            animator.SetTrigger("walk");                                
            GetComponent<Rigidbody>().isKinematic = true;               
            currientStateEnemy                    = StateEnemy.Move;
        }
    }

    private void BloodHitAffect() {
        if(effectBlood) {
        effectBlood.Play();
        }
    }

    private void OnDeath() {
        // agent.isStopped = true;
        audioSource.PlayOneShot(dieSound);
        navMeshAgent.enabled = false;
        animator.SetTrigger("death");

        if(effectDeath != null) {
        effectDeath.gameObject.SetActive(true);
        effectDeath.Play();
        }
        currientStateEnemy = StateEnemy.Dead;
        BoxCollider boxCol = gameObject.GetComponent<BoxCollider>();
        boxCol.enabled = false;
        Destroy(gameObject, 3f);
    }

    private void MoveToTarget() {
        if(!secondPlayer.gameObject.activeSelf) {
            agent.Stop();
        }

        agent.SetDestination(secondPlayer.transform.position);
        distanceToTarget = Vector3.Distance(transform.position, secondPlayer.transform.position);
        if(distanceToTarget < playerdistance) {
            nextAttack         = Time.time + attackRate;
            currientStateEnemy = StateEnemy.Atack;
        }
    }

    private void TargetAtack() {
        if(Time.time < nextAttack) {
            return;
        }
        animator.SetTrigger("attack");      
        healthSecondPlayer.ChangeHealth(-damage);
        nextAttack = Time.time + attackRate;
    }

}
