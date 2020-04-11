using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    
    private Rigidbody rb;
    [SerializeField] private float speed =1f;
    
    private void Awake() {
      
    }

    void Start() {


        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
