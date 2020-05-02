using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    [Tooltip("Предметы которые заспаунятся при разрушении предмета с BreakableObject  ")] [SerializeField] GameObject[] gameLoot;
    [Tooltip("Предметы которые заспаунятся при разрушении предмета с BreakableObject ")] [SerializeField] GameObject[] gameLoot1;
    [Tooltip("Сила с которой вылетят предметы находящиеся у предмета с BreakableObject ")] [SerializeField] float push=15;
    Pushable pushable;
    Health health;

    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath = DoDeath;
    }

    public void DoDeath()
    {  
        Destroy(gameObject);
        if (gameLoot != null && gameLoot.Length > 0)
        {
            var randomLoot = Random.Range(0, gameLoot.Length);
            var randomLoot1 = Random.Range(0, gameLoot1.Length);

           GameObject loot =  Instantiate(gameLoot[randomLoot], transform.position, Quaternion.identity);
            GameObject loot1 = Instantiate(gameLoot1[randomLoot1], transform.position, Quaternion.identity);

            pushable = loot.GetComponent<Pushable>();
            pushable = loot1.GetComponent<Pushable>();

            Vector3 direction = new Vector3(0, 1, 0);
            Debug.Log(direction);
            direction = direction.normalized * push;
            pushable.Push(direction,true);
        }
    }
}
