using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private GameObject Enemy;
    
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }
   
    IEnumerator SpawnEnemy()
    {
        Spawner();
        yield return new WaitForSeconds(7f);
        StartCoroutine(SpawnEnemy());
    }

    private void Spawner()
    {
        Instantiate(Enemy, transform.position, Quaternion.identity);
    }
}
