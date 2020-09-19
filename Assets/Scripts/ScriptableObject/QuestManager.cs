using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

public class QuestManager : GenericSingletonClass<QuestManager>
{
    [SerializeField] QuestStates firstQuest = QuestStates.MOVE;
    

    [SerializeField] Text textQuest;
    [SerializeField] Image image;
    [SerializeField] GameObject questUi;

    [SerializeField] QuestStates lastQuest;
   
    [SerializeField] public TypeItem typeItem;

    [SerializeField] GameObject oil;

    [SerializeField] GameObject airplane;


    public enum TypeItem { TURELL, AID, CHEST,BATUT, KORM, GRENADE, NONE }



    public Text currentTime;
  

    [SerializeField] Quest[] allQuests;

    private Coroutine delayQuests;

    public QuestStates currentQuest;
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
        KICKBIGCHUNK = 9,
        CRAFTJUMPPAD = 10,
        KICKPAD =11,
        JUMPPAD = 12,
        COLLECTCOUNTRESOURS= 13,
        DESTROYSUITCASE = 14, 
        DESTROYMONSTERS = 15,
        CRAFTTURREL = 16,
        FINDRECORDER = 17,
        FINDAIRPLANE = 18,
        FINDOIL = 19,
        ESCAPE = 20




    }

    void Start()
    {

        oil.gameObject.SetActive(false);
        currentQuest = firstQuest;
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

    public void CheckItemQuest(QuestStates quest, TypeItem typeItem)
    {
        if (currentQuest == quest && typeItem == allQuests[(int)currentQuest].itemToCraft)
        {
            CheckQuests(quest);
        }
    }

    public void CheckQuests(QuestStates quest, int amount = 1)
    {
        if (currentQuest == quest )
        {

            if (currentQuest == QuestStates.FINDAIRPLANE || currentQuest == QuestStates.ESCAPE)
            {
              
                Compass.Instance.questLocation = airplane.transform ;
            }
            if (currentQuest == QuestStates.FINDOIL)
            {
                oil.gameObject.SetActive(true);
                Compass.Instance.questLocation = oil.transform;
            }
           
           


            if (numberOfTimes > 0)
            {
                currentOfTime+= amount;
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




