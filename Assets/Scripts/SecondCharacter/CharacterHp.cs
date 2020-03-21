using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHp : MonoBehaviour
{
  public float health = 100;
  public Slider slider;

    Regeneration regeneration;

 /* private void Update()
  {
    slider.value = health;
  }*/

    private void Start()
    {
        regeneration = FindObjectOfType<Regeneration>();
    }

    public void RegenerationHealth()
    {
        health += regeneration.GetRegeneration();
    }

}
