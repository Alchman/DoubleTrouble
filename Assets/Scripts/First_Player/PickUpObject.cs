using System;
using UnityEngine;

public class PickUpObject : MonoBehaviour{
    private Resources resources;
    private void Awake() {
        resources = GetComponent<Resources>();
    }

    private void OnCollisionEnter(Collision other) {

        if(other.collider.transform.CompareTag("Player")) {
            print("pickUp " + transform.name);
            ResourceType currentRes = resources.resourceType;
            int currentCount = resources.count;
        SecondPlayer.Instance.AddResourses(currentRes, currentCount);
        Destroy(gameObject);
        }
      
    }
}
