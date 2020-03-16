using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushThings : MonoBehaviour
{
  
    [SerializeField] float pushRunWithoutForce;
    [SerializeField] Transform player;
    [SerializeField] float hightY;
    [SerializeField] bool push;
  
   


    PlayerController playerController;
    Rigidbody rigidbody;
    CharacterHp characterHp;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
        characterHp = FindObjectOfType<CharacterHp>();
    }

    // Update is called once per frame
  
    public void Push(Vector3 force)
    {
        if (push)
        {
            rigidbody.AddForce(force, ForceMode.Impulse);

        }



    }
  
    private void OnCollisionEnter(Collision collision)
    {
        //Толчок при столкновении с предметом
        if (collision.gameObject.tag == "Ground")
        {

            push = true;


        }
        else  if (push && collision.gameObject.tag == "Player")
        {
            Debug.Log(1);
            var direction = transform.position - player.transform.position;
            direction.y += hightY;
            rigidbody.AddForce(direction.normalized * pushRunWithoutForce);
            Debug.DrawLine(player.position, player.position + direction);
            push = false;
        }

        if (collision.gameObject.tag == "SecondPlayer")
        {
            characterHp.RegenerationHealth();
            Destroy(gameObject);
        }



    }
    
}
