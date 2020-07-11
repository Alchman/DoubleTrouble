using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    [Tooltip("Скорость которую нужно добавить игроку ")] [SerializeField] float powerSpeed;
    [Tooltip("Время на которое отлючается ручное управления во время ускорения ")] [SerializeField] float timeDeativeInput;
    [Tooltip("Сила с которой его подкинет вверх по направлению ускорения ")] [SerializeField] float forceY;

    public Vector3 GetDirectionSpeed()
    {
        Vector3 dir = transform.up * powerSpeed;
        dir.y = forceY;
        return dir;
    }
    public float GetTimeDeativeInput()
    {
        return timeDeativeInput;
    }
   
}
