using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{

    [SerializeField] int health;
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
            secondPlayer.HealthUpdate(health);

        }
    }
}
