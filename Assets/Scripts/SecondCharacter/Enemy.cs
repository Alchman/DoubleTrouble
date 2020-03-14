using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] private float speed = 14f;
    [SerializeField] private float playerdistance = 5f;
    
    private SecondCharacter _secondCharacter;
    private Rigidbody rb;
    private float distance;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _secondCharacter = FindObjectOfType<SecondCharacter>();
        
    }

    void FixedUpdate()
    {

        
        Vector3 target = _secondCharacter.transform.position;
        Vector3 enemy = transform.position;
        Vector3 direction = (target - enemy);
        direction.y = rb.velocity.y;
      //  direction.y = 0;
        rb.velocity = direction.normalized * speed;
      distance =  Vector3.Distance(enemy, target);
        ChekDistance();
    }

    private void ChekDistance()
    {
        if (distance < playerdistance)
        {
            rb.velocity = Vector3.zero;
        }
    }
}
