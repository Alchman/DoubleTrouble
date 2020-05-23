using System;
using UnityEngine;
using Lean.Pool;
using System.Collections;

[RequireComponent(typeof(DamageDealer))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    Coroutine destroyBullet;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
       

    }
    private void OnEnable()
    {
      //  LeanPool.Despawn(gameObject,6f);
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(5f);
        LeanPool.Despawn(gameObject);
    }

        public void FireBullet(float damage, float speed)
    {
        DamageDealer dd = GetComponent<DamageDealer>();
        dd.damage = damage;

        rb.velocity = transform.forward * speed;

        destroyBullet = StartCoroutine(DestroyBullet());

    }

    private void OnTriggerEnter(Collider other) {
       
        LeanPool.Despawn(gameObject);
       StopAllCoroutines();
       
    }
}
