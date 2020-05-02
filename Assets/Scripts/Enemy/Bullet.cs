using System;
using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FireBullet(float damage, float speed)
    {
        DamageDealer dd = GetComponent<DamageDealer>();
        dd.damage = damage;

        rb.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
    }
}
