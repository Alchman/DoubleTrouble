using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    [SerializeField] private float timeRespawn = 12f;
    
    
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
   
    IEnumerator SpawnEnemy()
    {
        while(true)
        {
            Spawner();
            yield return new WaitForSeconds(timeRespawn);
        }
    }

    private void Spawner()
    {
        Instantiate(Enemy, transform.position, Quaternion.identity);
    }
}
