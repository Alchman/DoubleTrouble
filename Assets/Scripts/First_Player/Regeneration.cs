using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{

    [Tooltip("Кол-во восстановления здоровья ")] [SerializeField] int health=30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            Destroy(gameObject,1f);
            SecondPlayer.Instance.HealthUpdate(health);
        }
    }
}
