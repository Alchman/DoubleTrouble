using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushThings : MonoBehaviour
{
    [Tooltip("Сила удара при столкновении с предметом")] [SerializeField] float pushRunWithoutForce;
    [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightY;
    [Tooltip("Было ли столкновение")] [SerializeField] bool push;
    [SerializeField] Transform player;

    PlayerController playerController;
    Rigidbody rigidbody;
  
    DamageDealer damageDealer;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        rigidbody = GetComponent<Rigidbody>();
      
        damageDealer = FindObjectOfType<DamageDealer>();
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
    }
    
}
