using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Random;

[CreateAssetMenu(menuName = "QuestItem")]
public class Quest : ScriptableObject
{
    [SerializeField] public string text;
    [SerializeField] public Sprite image;
    [SerializeField] public float delay;
    [SerializeField] public int numberOfTime;
    [SerializeField] public QuestManager.TypeItem itemToCraft;




}
