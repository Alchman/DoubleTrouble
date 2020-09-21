using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocator : MonoBehaviour
{
    public QuestManager.QuestStates quest;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (QuestManager.Instance.currentQuest == quest)
            {
                QuestManager.Instance.CheckQuests(quest);
                if (quest == QuestManager.QuestStates.FINDOIL)
                {
                    Destroy(gameObject, 0.5f);
                }
            }
            //QuestManager.Instance.CheckQuests(QuestManager.QuestStates.FINDAIRPLANE);
            //QuestManager.Instance.CheckQuests(QuestManager.QuestStates.FINDOIL);
            //QuestManager.Instance.CheckQuests(QuestManager.QuestStates.ESCAPE);


        }
    }
}
