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

    int currentOfTime = 0;
    int numberOfTimes= 0;
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
       numberOfTimes = quest.numberOfTime;
    }


    public void QuestsFinish()
    {
        
        currentQuest++;
        Quest nextQuest = allQuests[(int)currentQuest];
        textQuest.text = nextQuest.text;
        image.sprite = nextQuest.image;
        currentOfTime = 0;
        numberOfTimes = nextQuest.numberOfTime;

    }

    IEnumerator DelayQuest(float delay)
    {
       
        yield return new WaitForSeconds(delay);
        QuestsFinish();
        delayQuests = null;
    }

    public void CheckQuests(QuestStates quest)
    {
        if (currentQuest == quest)
        {
            currentOfTime++;
            Debug.Log(currentOfTime);
            if (currentOfTime >= numberOfTimes)
            {
                
                Debug.Log(currentOfTime);
                if (delayQuests == null)
                {
                    float delay = allQuests[(int)currentQuest].delay;
                    delayQuests = StartCoroutine(DelayQuest(delay));


                }
            }
         
        
          
               
            
        }

    }


}




