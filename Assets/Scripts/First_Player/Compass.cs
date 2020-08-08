using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public RectTransform arrow;


    private void Update()
    {
        ChangeMissionDirection();
    }

    public void ChangeMissionDirection()
    {
        Vector3 dir = SecondPlayer.Instance.transform.position - FirstPlayer.Instance.transform.position;
        Quaternion missionDirection = Quaternion.LookRotation(dir);

        missionDirection.z = -missionDirection.y ;
        missionDirection.x = 0;
        missionDirection.y = 0;

        arrow.localRotation = missionDirection;
    }

}
