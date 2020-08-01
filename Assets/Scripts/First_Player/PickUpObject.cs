using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {


        if(other.collider.transform.CompareTag("Player")) {
            print("pickUp " + transform.name);
        Destroy(gameObject);
        SecondPlayer.Instance.AddResourses(ResourceType.GEARS, 300);
        }
      
    }
}
