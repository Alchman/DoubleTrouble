using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocator : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {

            QuestManager.Instance.CheckQuests(QuestManager.QuestStates.FINDOIL);
            QuestManager.Instance.CheckQuests(QuestManager.QuestStates.FINDRECORDER);
            QuestManager.Instance.CheckQuests(QuestManager.QuestStates.FINDAIRPLANE);
            QuestManager.Instance.CheckQuests(QuestManager.QuestStates.ESCAPE);
            Destroy(gameObject, 0.5f);


        }
    }
}
