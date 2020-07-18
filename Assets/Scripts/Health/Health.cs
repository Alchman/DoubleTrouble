using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    public Action OnHealthUpdate = delegate { };
    public Action OnDamage = delegate { };

    public Action OnDeath = delegate { };

    [SerializeField]
    [Tooltip("Максимальное количество здоровья")]
    float maxHealth;

    public float HealthLeft { get; private set; }

    public float MaxHealth
    {
        get { return maxHealth; }
    }

    private void Awake()
    {
        HealthLeft = MaxHealth;
    }

    public void ChangeHealth(float amount)
    {
        HealthLeft += amount;
        OnHealthUpdate();
        if (amount < 0)
        {
            OnDamage();
        }

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
            if (damageDealer.layerMask == (damageDealer.layerMask | (1 << gameObject.layer)))
            {
                ChangeHealth(-damageDealer.damage);
            }
        }

    }
}
