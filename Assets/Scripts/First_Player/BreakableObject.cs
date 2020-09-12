using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    [Tooltip("Предметы которые заспаунятся при разрушении предмета с BreakableObject")] [SerializeField] GameObject[] gameLoot;
    [Tooltip("Предметы которые заспаунятся при разрушении предмета с BreakableObject")] [SerializeField] GameObject[] gameLoot1;
    [Tooltip("Сила с которой вылетят предметы находящиеся у предмета с BreakableObject")] [SerializeField] float push=15;
    [Tooltip("Уничтожать ли объект при разрушении")] [SerializeField] bool destroyObject = true;

    Pushable pushable;
    Health health;

    void Start()
    {
        health = GetComponent<Health>();
        health.OnDeath += DoDeath;
    }

    public void DoDeath()
    {  
        if (destroyObject)
        {
            Destroy(gameObject);
            QuestManager.Instance.CheckQuests(QuestManager.QuestStates.DESTROYSUITCASE);
        }

        if (gameLoot != null && gameLoot.Length > 0)
        {
            var randomLoot = Random.Range(0, gameLoot.Length);
            SpawnObject(gameLoot[randomLoot]);
        }

        if (gameLoot1 != null && gameLoot1.Length > 0)
        {
            var randomLoot1 = Random.Range(0, gameLoot1.Length);
            SpawnObject(gameLoot1[randomLoot1]);
        }
    }

    private void SpawnObject(GameObject go)
    {
        GameObject loot = Instantiate(go, transform.position, Quaternion.identity);

        pushable = loot.GetComponent<Pushable>();

        if (pushable != null)
        {
            Vector3 direction = new Vector3(0, 1, 0);
            direction = direction.normalized * push;
            pushable.Push(direction, true);
        }
    }
}
