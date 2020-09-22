using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour{
    [SerializeField] private List<GameObject> enemys;
    // [SerializeField] private GameObject Enemy;
    [SerializeField][Tooltip("время респауна моба в секундах")] private float      timeRespawn = 12f;
    [SerializeField][Tooltip("Дилэй начала спауна врагов")] private int      firstSpawnDelay = 20;

    public static int CountEnemy {get; set;}

    private void Awake()
    {
        CountEnemy = 0;
    }

    void Start() {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy() {
        yield return new WaitForSeconds(firstSpawnDelay);
        while(true) {
            Spawner();
            yield return new WaitForSeconds(timeRespawn);
        }
    }

    private void Spawner() {
        if(CountEnemy < EnemyManager.Instance.EnemySpownCount) {
            GameObject respenemy = enemys[Random.Range(0, enemys.Count)];
            // GameObject enemy       = Instantiate(Enemy, transform.position, Quaternion.identity);
            GameObject enemy       = Instantiate(respenemy, transform.position, Quaternion.identity);
            Health     enemyHealth = enemy.GetComponent<Health>();
            enemyHealth.OnDeath += EnemyDie;
            CountEnemy++;
        }
    }

    private void EnemyDie() {
        CountEnemy--;
    }
}
