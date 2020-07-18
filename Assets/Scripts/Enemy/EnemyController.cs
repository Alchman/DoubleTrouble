using System.Collections;
using UnityEngine;
using UnityEngine.AI;

enum StateEnemy{
    Start,
    Move,
    Atack,
    StopPush
}

public class EnemyController : MonoBehaviour{
    [SerializeField] [Tooltip("Скорость передвижения")]
    private float speed = 1f;

    [SerializeField] [Tooltip("Дистанция до плеера для атаки")]
    private float playerdistance = 5f;

    [SerializeField] [Tooltip("Частота удара")]
    private float attackRate = 1f;

    [SerializeField] [Tooltip("Сила дамага для энеми")] private float damage = 30f;

    [SerializeField][Tooltip("Аниматор для врага")] private Animator animator;
    
    private SecondPlayer secondPlayer;
    private Rigidbody    rb;
    private float        distanceToTarget;
    private NavMeshAgent agent;
    private StateEnemy   currientStateEnemy;
    private Health       health;
    private Health       healthSecondPlayer;
    private float        nextAttack;
    private Pushable pushable;
    private NavMeshAgent navMeshAgent;
    private Coroutine waitCoroutineEnemy;
   
    

    void Start() {
        rb                 =  GetComponent<Rigidbody>();
        secondPlayer       =  SecondPlayer.Instance;
        agent              =  GetComponent<NavMeshAgent>();
        health             =  GetComponent<Health>();
        healthSecondPlayer =  secondPlayer.GetComponent<Health>();
        health.OnDeath     += OnDeath;
        pushable = GetComponent<Pushable>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        pushable.PushEnemy += Push;
    }

    private void Update() {
        switch(currientStateEnemy) {
            case StateEnemy.Start :

                currientStateEnemy = StateEnemy.Move;
                break;
            
            case StateEnemy.StopPush :
              print("все я стою");
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
        }
    }

    private void Push() {
        print("pnuli");
        navMeshAgent.enabled = false;
        
        if(waitCoroutineEnemy != null) {
            StopCoroutine(waitCoroutineEnemy);
        }
        waitCoroutineEnemy = StartCoroutine(WaitPush());
        animator.SetTrigger("death");
        currientStateEnemy = StateEnemy.StopPush;
    }

    IEnumerator WaitPush() {
        print("карутина стою");
        yield return new WaitForSeconds(3);
        navMeshAgent.enabled = true;
        animator.SetTrigger("walk");
        currientStateEnemy = StateEnemy.Move;
    }

    private void OnDeath() {
        Destroy(gameObject);
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

        healthSecondPlayer.ChangeHealth(damage);
        nextAttack = Time.time + attackRate;
    }
}
