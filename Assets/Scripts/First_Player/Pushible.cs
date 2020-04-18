using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushible : MonoBehaviour
{
    [SerializeField] float massCoef;
    bool pushOnRun;
    [SerializeField] GameObject player;
    
    [Tooltip("Высота на которую кидается предмет")] [SerializeField] float hightY;
    FirstPlayer firstPlayer;
    // Start is called before the first frame update
    void Start()
    {
        pushOnRun = true;
       // firstPlayer.GetComponent<FirstPlayer>().SetBoolean(true);
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Push(Vector3 forcePush)
    {  
        forcePush*= massCoef;
       
    }
    public void PushOnRun(Vector3 pushOnRunForce)
    {
        var direction = transform.position - player.transform.position;
        direction.y += hightY;
        pushOnRunForce *= massCoef;
    }
   

}
