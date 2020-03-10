using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHp : MonoBehaviour
{
  public int health = 100;
  public Slider slider;

  private void Update()
  {
    slider.value = health;
  }
}
