using UnityEngine;
using UnityEngine.AI;

enum StateEnemy{
    Move,
    Atack
}

public class EnemyController : MonoBehaviour{
    [SerializeField] [Tooltip("Скорость передвижения")]
    private float speed = 14f;

    [SerializeField] [Tooltip("Дистанция до плеера для атаки")]
    private float playerdistance = 5f;

    private SecondPlayer secondPlayer;
    private Rigidbody    rb;
    private float        distanceToTarget;
    private NavMeshAgent agent;
    private StateEnemy   currientStateEnemy;

    void Start() {
        rb           = GetComponent<Rigidbody>();
        secondPlayer = FindObjectOfType<SecondPlayer>();
        agent        = GetComponent<NavMeshAgent>();
    }

    private void Update() {
        switch(currientStateEnemy) {
            case StateEnemy.Move :
                MoveToTarget();
                break;
            case StateEnemy.Atack :

                TargetAtack();
                break;
        }

        MoveToTarget();
    }

    private void OnDeath() {
        print("enemy death");
        Destroy(gameObject);
    }

    private void MoveToTarget() {
        agent.SetDestination(secondPlayer.transform.position);
        distanceToTarget = Vector3.Distance(transform.position, secondPlayer.transform.position);
        if(distanceToTarget < playerdistance) {
            currientStateEnemy = StateEnemy.Atack;
        }
    }

    private void TargetAtack() {
        print("Enemy attack target");
    }
}
