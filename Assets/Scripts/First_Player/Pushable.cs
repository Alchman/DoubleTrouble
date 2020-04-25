using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    [SerializeField] float massCoef;
    [SerializeField] bool pushOnRun;
    
  
    

    bool  isOnGround;
    Rigidbody rigidbody;
    public bool PushOnRun { get { return pushOnRun; } }

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
       // firstPlayer.GetComponent<FirstPlayer>().SetBoolean(true);
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Push(Vector3 force)
    {
        if (isOnGround)
        {
            force *= massCoef;
            rigidbody.AddForce(force, ForceMode.Impulse);
            isOnGround = false;
            Debug.Log("Push");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Ground")
        {
            isOnGround = true;
          
        }
      
    }


}
