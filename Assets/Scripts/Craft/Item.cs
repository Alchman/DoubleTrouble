using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CraftItem")]
public class Item : ScriptableObject
{
    public GameObject item;
    public int cost;
    public GameButton buy;
    public QuestManager.TypeItem itemType;


}
