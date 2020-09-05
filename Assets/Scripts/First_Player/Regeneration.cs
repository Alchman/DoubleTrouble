using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{

    [Tooltip("Кол-во восстановления здоровья для второго игрока ")] [SerializeField] int healthSecondPlayer = 30;
   // [Tooltip("Кол-во восстановления здоровья для первого игрока ")] 
    public int HealthPlayer { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            Destroy(gameObject,1f);
            SecondPlayer.Instance.HealthUpdate(healthSecondPlayer);
            QuestManager.Instance.CheckQuests(QuestManager.QuestStates.PUSHTOMIKE);
        }
    }
}
