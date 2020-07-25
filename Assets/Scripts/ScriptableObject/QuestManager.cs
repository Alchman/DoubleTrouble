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
        JUMP = 1,
        PUSH = 2

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
        currentQuest++;
        Quest nextQuest = allQuests[(int)currentQuest];
        textQuest.text = nextQuest.text;
        image.sprite = nextQuest.image;

    }

    IEnumerator DelayQuest(float delay)
    {

        yield return new WaitForSeconds(delay);
        if (delayQuests != null)
        {

            QuestsFinish();
        }



    }

    public void CheckQuests(QuestStates quest)
    {

        if (currentQuest == quest)
        {
            if (delayQuests == null)
            {
                float delay = allQuests[(int)currentQuest].delay;
                delayQuests = StartCoroutine(DelayQuest(delay));
            }
        }
          
    }


}




