using System;
using UnityEngine;

public class PickUpObject : MonoBehaviour
{
    private Resources resources;
    private Regeneration regeneration;
    
    private void Awake() {
        resources = GetComponent<Resources>();
        regeneration = GetComponent<Regeneration>();
    }

    private void OnCollisionEnter(Collision other) {

        if(other.collider.transform.CompareTag("Player"))
        {
            if (regeneration != null)
            {
                if (FirstPlayer.Instance.Health.HealthLeft < FirstPlayer.Instance.Health.MaxHealth)
                {
                    FirstPlayer.Instance.Health.ChangeHealth(regeneration.health);
                    FirstPlayer.Instance.GetEffectHealFirstPlayer();
                }
                else
                {
                    return;
                }
            }
                
            if (resources != null)
            {
                FirstPlayer.Instance.GetEffectConstructFirstPlayer();
                ResourceType currentRes = resources.resourceType;
                int currentCount = resources.count;
                SecondPlayer.Instance.AddResourses(currentRes, currentCount);
                QuestManager.Instance.CheckQuests(QuestManager.QuestStates.COLLECTRESOURSES);
                QuestManager.Instance.CheckQuests(QuestManager.QuestStates.COLLECTCOUNTRESOURS, currentCount );
            }

            Destroy(gameObject);
        }
      
    }
}
