using System.Collections;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour{
    [SerializeField] private GameObject Enemy;
    [SerializeField][Tooltip("время респауна моба в секундах")] private float      timeRespawn = 12f;

    public static int CountEnemy {get; set;}

    void Start() {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy() {
        while(true) {
            Spawner();
            yield return new WaitForSeconds(timeRespawn);
        }
    }

    private void Spawner() {
        if(CountEnemy < EnemyManager.Instance.EnemySpownCount) {
            GameObject enemy       = Instantiate(Enemy, transform.position, Quaternion.identity);
            Health     enemyHealth = enemy.GetComponent<Health>();
            enemyHealth.OnDeath += EnemyDie;
            CountEnemy++;
        }
    }

    private void EnemyDie() {
        CountEnemy--;
    }
}
