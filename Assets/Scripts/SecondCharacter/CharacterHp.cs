using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHp : MonoBehaviour
{
  public float health = 100;
    public int bulletsAll;  
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

    public void RegenerationHealth(float regeneration)
    {
        health += regeneration;
    }

    public void RegenerationBullets(int bullets)
    {
        bulletsAll += bullets;
    }

}
