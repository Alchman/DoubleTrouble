using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftManager : MonoBehaviour
{
    private GameObject itemObject;
    private int itemCost;
    private int enumItem;
    
   
    
    [SerializeField] float force;
    [SerializeField] Button[] buttonCreate;
    [SerializeField] float delay = 0;
    [SerializeField] Text[] countText;
    [SerializeField] Item[] allItems;

   



    Item item;

    public void Start()
    {
        for (int i = 0; i < buttonCreate.Length; i++)
        {
            countText[i].text = allItems[i].cost.ToString();
        }
    }



    public void ChoiseItem(int index)
    {

        item = allItems[index];
        itemCost = item.cost;
        itemObject = item.item;
       
    }



    public void Create(int indexItem)
    {
        ChoiseItem(indexItem);
        SecondPlayer.Instance.MinusResourses(ResourceType.GEARS, itemCost);
        GameObject game = Instantiate(itemObject, SecondPlayer.Instance.transform.position, Quaternion.identity);
        SecondPlayer.Instance.Ejection(game, delay, force);
        Check();

    }

    public void Check()
    {
        for (int i = 0; i < buttonCreate.Length; i++)
        {
            if (SecondPlayer.Instance.GetResourses(ResourceType.GEARS) > allItems[i].cost)
            {

                buttonCreate[i].interactable = true;
          
            }
            else
            {

                buttonCreate[i].interactable = false;
             

            }


        }
    }
}


