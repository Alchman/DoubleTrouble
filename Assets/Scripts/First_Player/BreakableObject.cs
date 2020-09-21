using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{

    [Tooltip("Предметы которые заспаунятся при разрушении предмета с BreakableObject")] [SerializeField] GameObject[] gameLoot;
    [Tooltip("Предметы которые заспаунятся при разрушении предмета с BreakableObject")] [SerializeField] GameObject[] gameLoot1;
    [Tooltip("Сила с которой вылетят предметы находящиеся у предмета с BreakableObject")] [SerializeField] float push=15;
    [Tooltip("Уничтожать ли объект при разрушении")] [SerializeField] bool destroyObject = true;

    [Header("Чёрны ящик")]
    [Tooltip("Может ли из объекта выпасть чёрный ящик по квесту")] [SerializeField] bool canSpawnRecorder = false;
    [Tooltip("Шанс спауна чёрного ящика при активном квесте")] [SerializeField] float recorderChance = 0.5f;
    [Tooltip("Префаб чёрного ящика")] [SerializeField] private GameObject resourceBox;


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


        if (canSpawnRecorder && QuestManager.Instance.currentQuest == QuestManager.QuestStates.FINDRECORDER)
        {
            if (Random.value <= recorderChance)
            {
                SpawnObject(resourceBox);
            }
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
