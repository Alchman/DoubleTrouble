using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{

    [Tooltip("Кол-во восстановления здоровья ")] [SerializeField] int health=30;
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
            Destroy(gameObject,1f);
            secondPlayer.HealthUpdate(health);
        }
    }
}
