using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    [Tooltip("Скорость которую нужно добавить игроку ")] [SerializeField] float powerSpeed;
    [Tooltip("Время на которое отлючается ручное управления во время ускорения ")] [SerializeField] float timeDeactiveInput;
    [Tooltip("Сила с которой его подкинет вверх по направлению движения ")] [SerializeField] float stoneThrowForce;
    [Tooltip("Если true то эффект действует только в одном направлении, " +
        "если false, то эффект действует по направлению вдижения игрока ")]
    [SerializeField] bool fixedDirection;


    public Vector3 GetDirectionSpeed(Vector3 playerDirection)
    {
        Vector3 direction;
        if (fixedDirection)
        {
            direction = transform.up * powerSpeed;
        }
        else
        {
            direction = playerDirection * powerSpeed;
        }
        direction.y = stoneThrowForce;
        return direction;

    }
    public float GetTimeDeactiveInput()
    {
        return timeDeactiveInput;
    }

}
