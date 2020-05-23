using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [Tooltip("Присвоение предмету определенного ресурса")] public ResourceType resourceType;
    [Tooltip("Количество ресурса ")] [SerializeField] int count=1;
    // Start is called before the first frame update

    
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            Destroy(gameObject,1f);
            SecondPlayer.Instance.AddResourses(resourceType, count); 
        }
    }
}
