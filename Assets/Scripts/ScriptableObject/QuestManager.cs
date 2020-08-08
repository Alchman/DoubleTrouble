using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class QuestManager : GenericSingletonClass<QuestManager>
{

    [SerializeField] Text textQuest;
    [SerializeField] Image image;


    [SerializeField] Quest[] allQuests;

    private Coroutine delayQuests;

    QuestStates currentQuest;


    public enum QuestStates
    {
        MOVE = 0,
        PUSH = 1,
        PUSHOBj = 2,
        PUSHTOMIKE = 3,
        JUMP = 4,
        RUN= 5,
        PUSHENEMY = 6,
        PUSHOBJTOENEMY = 7
      
    }

    void Start()
    {
        currentQuest = QuestStates.MOVE;
        Quest quest = allQuests[(int)currentQuest];
        textQuest.text = quest.text;
        image.sprite = quest.image;
    }


    public void QuestsFinish()
    {
        //Debug.Log("quests finish: " + currentQuest);
        currentQuest++;
        //Debug.Log("start quest: " + currentQuest);
        Quest nextQuest = allQuests[(int)currentQuest];
        textQuest.text = nextQuest.text;
        image.sprite = nextQuest.image;
    }

    IEnumerator DelayQuest(float delay)
    {
        //Debug.Log("delay start");

        yield return new WaitForSeconds(delay);

        //Debug.Log("delay finish");
        QuestsFinish();
        delayQuests = null;
    }

    public void CheckQuests(QuestStates quest)
    {
        if (currentQuest == quest)
        {
            if (delayQuests == null)
            {
            //    Debug.Log("Finish Quest with delay: " + currentQuest);

                float delay = allQuests[(int)currentQuest].delay;
                delayQuests = StartCoroutine(DelayQuest(delay));
            }
        }

    }


}




