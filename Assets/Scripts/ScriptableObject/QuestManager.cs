using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class QuestManager : GenericSingletonClass<QuestManager>
{

    [SerializeField] Text textQuest;
    [SerializeField] Image image;
    [SerializeField] GameObject questUi;

    [SerializeField] QuestStates lastQuest;



    public Text currentTime;
  

    [SerializeField] Quest[] allQuests;

    private Coroutine delayQuests;

    QuestStates currentQuest;
    int id;

    int currentOfTime = 0;
    int numberOfTimes;
    public enum QuestStates
    {
        MOVE = 0,
        PUSH = 1,
        PUSHOBj = 2,
        PUSHTOMIKE = 3,
        JUMP = 4,
        RUN= 5,
        PUSHENEMY = 6,
        PUSHOBJTOENEMY = 7,
        COLLECTRESOURSES = 8,
        KICKBIGCHUNK = 9
      
    }

    void Start()
    {
       

        currentQuest = QuestStates.MOVE;
        Quest quest = allQuests[(int)currentQuest];
        textQuest.text = quest.text;
        image.sprite = quest.image;
       numberOfTimes = quest.numberOfTime;
 
       currentTime.text = currentOfTime + "/" + numberOfTimes;

}


    public void QuestsFinish()
    {
        if (currentQuest == lastQuest)
        {
            questUi.gameObject.SetActive(false);
        }
        currentQuest++;
      
        Quest nextQuest = allQuests[(int)currentQuest];
        image.sprite = nextQuest.image;
        currentOfTime = 0;
        numberOfTimes = nextQuest.numberOfTime;
        textQuest.text = nextQuest.text;
        currentTime.text = currentOfTime + "/" + numberOfTimes;
      


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
            if (numberOfTimes > 0)
            {
                currentOfTime++;
                currentTime.text = currentOfTime + "/" + numberOfTimes;
                currentTime.gameObject.SetActive(true);
               
            }
            else
            {
                currentTime.gameObject.SetActive(false);
            }
            
            if (currentOfTime >= numberOfTimes)
            {
                if (delayQuests == null)
                {
                    float delay = allQuests[(int)currentQuest].delay;
                    delayQuests = StartCoroutine(DelayQuest(delay));
                }
            }
           
        }
       
       

    }


}




