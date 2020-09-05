using System;
using UnityEngine;

public class PickUpObject : MonoBehaviour{
    private Resources resources;
    private Regeneration regeneration;
    private void Awake()
    {
        resources = GetComponent<Resources>();
        regeneration = GetComponent<Regeneration>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.collider.transform.CompareTag("Player"))
        {
            if (resources != null)
            {
                print("pickUp " + transform.name);
                ResourceType currentRes = resources.resourceType;
                int currentCount = resources.count;
                SecondPlayer.Instance.AddResourses(currentRes, currentCount);
                Destroy(gameObject);
            }
            if (regeneration != null)
            {
                FirstPlayer.Instance.HealthUpdate(regeneration.HealthPlayer);
                Destroy(gameObject);
            }
        }
    }
}
