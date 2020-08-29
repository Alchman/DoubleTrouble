using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour{
    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other != null) {

            if(other.CompareTag("Enemy")) {
                animator.SetTrigger("Granata_Explosion");
                Destroy(other.gameObject);
               print("granata popala"); 
            }
            
        }
    }
}
