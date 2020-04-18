using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] GameObject[] gameLoot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnDeath()
    {
        health.OnDeath();
        Destroy(gameObject);
        if (gameLoot != null)
        {
        var randomLoot  = Random.Range(0, gameLoot.Length);
        Instantiate(gameLoot[randomLoot], transform.position, Quaternion.identity);
           
        }
       
       

    }
}
