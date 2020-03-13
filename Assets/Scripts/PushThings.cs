using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushThings : MonoBehaviour
{
    [SerializeField] float forcePush;
   
   

    PlayerController playerController;
    Rigidbody rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rigidbody = FindObjectOfType<Rigidbody>();
    }

    // Update is called once per frame
  
    public void Push()
    {
        rigidbody.AddForce(Vector3.up,ForceMode.Impulse );


    }
   /* private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusForPush);
    }*/
}
