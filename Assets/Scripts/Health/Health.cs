using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnHealthUpdate = delegate { };
    public Action OnDeath = delegate { };

    [SerializeField] float maxHealth;

    public float HealthLeft { get; private set; }
    public float MaxHealth { get; }


    public void ChangeHealth(float amount)
    {
        HealthLeft -= amount;

        OnHealthUpdate();

        if (HealthLeft <= 0)
        {
            OnDeath();
        }
    }

    private void OnTriggerEnter(Collider other) {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if(damageDealer != null) {
            print("damage diller");
            ChangeHealth(damageDealer.damage);
        }
       
    }
}
