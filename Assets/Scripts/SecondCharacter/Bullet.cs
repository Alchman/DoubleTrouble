using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    
    private Rigidbody rb;


    private void Awake() {
      
    }

    void Start() {


        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.forward *5f;
    }

    // Update is called once per frame
    void Update()
    {
     
    }
}
