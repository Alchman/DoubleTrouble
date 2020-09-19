using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : GenericSingletonClass<Compass>
{
    public RectTransform arrow;
    public RectTransform questArrow;
    public Transform questLocation;

    private void Update()
    {
        ChangeMissionDirection(SecondPlayer.Instance.transform, arrow);
        if (questLocation != null)
        {

            questArrow.gameObject.SetActive(true);
            ChangeMissionDirection(questLocation.transform, questArrow);
        }
        else
        {
            questArrow.gameObject.SetActive(false);
        }
    }

    public void ChangeMissionDirection( Transform directionObject, RectTransform arrow )
    {
        Vector3 dir = directionObject.position - FirstPlayer.Instance.transform.position;
        Quaternion missionDirection = Quaternion.LookRotation(dir);

        missionDirection.z = -missionDirection.y ;
        missionDirection.x = 0;
        missionDirection.y = 0;
        arrow.localRotation = missionDirection;
    }    

   

}
