using System;
using UnityEngine;

public class Health : MonoBehaviour{
    public Action OnHealthUpdate = delegate {
                                   };

    public Action OnDeath = delegate {
                            };

    public float HealthLeft {get; private set;}
    public float MaxHealth;

    private void Start() {
        HealthLeft = MaxHealth;
    }

    public void ChangeHealth(float amount) {
        HealthLeft -= amount;

        if(HealthLeft <= 0) {
            OnDeath();
        }
    }

    private void OnTriggerEnter(Collider other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer != null) {
            ChangeHealth(damageDealer.damage);
        }
    }
}
