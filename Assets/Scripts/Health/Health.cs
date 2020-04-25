using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnHealthUpdate = delegate { };
    public Action OnDeath = delegate { };

    [SerializeField] float maxHealth;

    public float HealthLeft { get; private set; }
    public float MaxHealth { get { return maxHealth; } }



    private void Awake()
    {
        HealthLeft = MaxHealth;
    }

    public void ChangeHealth(float amount)
    {
        HealthLeft -= amount;

        if (HealthLeft <= 0)
        {
            OnDeath();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        DamageDealer damageDealer = other.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            ChangeHealth(damageDealer.damage);
        }
    }
}
