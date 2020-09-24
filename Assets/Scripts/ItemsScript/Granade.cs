using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public int damage = 1000;
    public float radius = 10;
    public float pushForce = 5;

    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }
    bool exploded = false;
    private void OnTriggerEnter(Collider other) {
        if (exploded) return; 

        if(other != null) {
            if(other.CompareTag("Enemy")) {
                animator.SetTrigger("Granata_Explosion");
                //Health enemy = other.GetComponent<Health>();
                //enemy.ChangeHealth(-damage);
                Explode();
                // Destroy(other.gameObject);
            }
            
        }
    }

    private void Explode()
    {
        exploded = true;
         Collider[] allItemsInRadius = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider target in allItemsInRadius)
        {
            Pushable pushable = target.GetComponent<Pushable>();
            if (pushable != null)
            {
                Vector3 direction = target.transform.position - transform.position;
                direction.y = 1;

                pushable.Push(direction.normalized * pushForce);
            }

            DamagebleByPush damagebleByPush = target.GetComponent<DamagebleByPush>();
            if (damagebleByPush != null)
            {
                damagebleByPush.DoDamage(damage);
            }
        }

        GetComponent<AudioSource>().Play();
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
