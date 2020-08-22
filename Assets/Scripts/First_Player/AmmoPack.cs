using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    [Tooltip("Выбор типа пули ")] public BulletType bulletType;
    [Tooltip("Количество пуль в паке ")] [SerializeField] int amount= 10;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            Destroy(gameObject,1f);
            SecondPlayer.Instance.AddAmmo(bulletType, amount);

        }
    }
}
