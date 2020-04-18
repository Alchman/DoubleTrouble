using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pushible : MonoBehaviour
{
    [SerializeField] float massCoef;
    [SerializeField] bool pushOnRun;
    [SerializeField] GameObject player;
    
    public bool PushOnRun { get; }

    // Start is called before the first frame update
    void Start()
    {
       // firstPlayer.GetComponent<FirstPlayer>().SetBoolean(true);
    }

  

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Push(Vector3 forcePush)
    {  
        forcePush *= massCoef;
       
    }

   

}
