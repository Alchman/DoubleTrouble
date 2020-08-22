using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Craft : MonoBehaviour
{
    [SerializeField] GameObject turrel;
    [SerializeField] int countTurrel;
    [SerializeField] float force;
    [SerializeField] Button buttonCreate;
    [SerializeField] float delay = 0;




    void Update()
    {
        Check();
    }

    public void Create()
    {
        SecondPlayer.Instance.MinusResourses(ResourceType.GEARS, countTurrel);
        GameObject game = Instantiate(turrel, SecondPlayer.Instance.transform.position, Quaternion.identity);
        SecondPlayer.Instance.Ejection(game, delay, force);
    }

    public void Check()
    {
        if (SecondPlayer.Instance.GetResourses(ResourceType.GEARS) > countTurrel)
        {

            buttonCreate.interactable = true;

        }
        else
        {
            buttonCreate.interactable = false;
        }
    }
}
