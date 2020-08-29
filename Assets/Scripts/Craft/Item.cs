using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CraftItem")]
public class Item : ScriptableObject
{
    [SerializeField] public GameObject item;
    [SerializeField] public int cost;
    [SerializeField] public GameButton buy;

  
}
