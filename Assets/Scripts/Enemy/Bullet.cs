using System.Collections;
using Lean.Pool;
using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public class Bullet : MonoBehaviour{
    private Rigidbody rb;
    Coroutine         destroyBullet;

    void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator DestroyBullet() {
        yield return new WaitForSeconds(5f);
        LeanPool.Despawn(gameObject);
    }

    public void FireBullet(float damage, float speed) {
        DamageDealer dd = GetComponent<DamageDealer>();
        dd.damage     = damage;
        rb.velocity   = transform.forward * speed;
        destroyBullet = StartCoroutine(DestroyBullet());
    }

    private void OnTriggerEnter(Collider other) {
        LeanPool.Despawn(gameObject);
        StopAllCoroutines();
    }
}
