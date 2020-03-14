using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushThings : MonoBehaviour
{
    [SerializeField] float height;


    PlayerController playerController;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
  
    public void Push(Vector3 force)
    {
   
        rigidbody.AddForce(force, ForceMode.Impulse);



    }
  
}
