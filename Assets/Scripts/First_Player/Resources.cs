using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public ResourceType resourceType;

    [SerializeField] int count;
    // Start is called before the first frame update
    SecondPlayer secondPlayer;


    private void Start()
    {
        secondPlayer = GetComponent<SecondPlayer>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            Destroy(gameObject);
            secondPlayer.AddResourses(resourceType, count);
           
        }
    }



}
