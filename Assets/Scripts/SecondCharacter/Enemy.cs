using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed = 14f;
    [SerializeField] private float playerdistance = 5f;
    
    private SecondCharacter _secondCharacter;
    private Rigidbody rb;
    private float distance;
    private NavMeshAgent agent;
    private Camera mainCamera;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _secondCharacter = FindObjectOfType<SecondCharacter>();
        mainCamera = Camera.main;
        agent = GetComponent<NavMeshAgent>();

    }

    void FixedUpdate()
    {
     // Move();
     //   ChekDistance();
     
    }

    private void Update()
    {

        if (Input.GetMouseButton(0))
        {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.ScreenPointToRay(Input.mousePosition),out hit))
        {
            agent.SetDestination(hit.point);
        }
            
        }
        
    }

    private void Move()
    {
        Vector3 target = _secondCharacter.transform.position;
        Vector3 enemy = transform.position;
        Vector3 direction = (target - enemy);
         direction.y = rb.velocity.y;
       // direction.y = 0;
        rb.velocity = direction.normalized * speed;
        distance =  Vector3.Distance(enemy, target);  
        print("moveEnemy");
    }
    
    private void ChekDistance()
    {
        if (distance < playerdistance)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
