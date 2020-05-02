using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    [Tooltip("Выбор типа пули ")] public BulletType bulletType;
    [Tooltip("Количество пуль в паке ")] [SerializeField] int amount= 10;
    SecondPlayer secondPlayer;
 
    void Start()
    {
        secondPlayer = GetComponent<SecondPlayer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            Destroy(gameObject);
            secondPlayer.AddAmmo(bulletType, amount);

        }
    }
}
