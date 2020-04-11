using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushEnemy : MonoBehaviour
{

    [Tooltip("Здоровье врага")] [SerializeField] float health;
    PlayerController playerController;
    Rigidbody rigidbody;
  
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
      
    }
    public void Push(Vector3 force)
    {
            rigidbody.AddForce(force, ForceMode.Impulse);   
       
    }

    public void Damage(float damageFoot)
    {
        health -= damageFoot;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
