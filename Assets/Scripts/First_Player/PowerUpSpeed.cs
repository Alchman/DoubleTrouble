using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpeed : MonoBehaviour
{
    [Tooltip("Скорость которую нужно добавить игроку ")] [SerializeField] int powerSpeed = 1000;
    [Tooltip("Время на которое отлючается ручное управления во время ускорения ")] [SerializeField] float timeDeactiveInput = 1;
    [Tooltip("Сила с которой его подкинет вверх по направлению движения ")] [SerializeField] int throwForce = 1000;
    [Tooltip("Если true то эффект действует только в одном направлении (зелёная стрелка положения объекта), " +
        "если false, то эффект действует по направлению движения игрока ")]
    [SerializeField] bool fixedDirection;

    [SerializeField] private ParticleSystem batytEffect;

    public Vector3 GetDirectionSpeed(Vector3 playerDirection)
    {
        QuestManager.Instance.CheckQuests(QuestManager.QuestStates.JUMPPAD);
        Vector3 direction;
        if (fixedDirection)
        {
            direction = transform.up * powerSpeed;
            batytEffect.Play();
        }
        else
        {
            direction = playerDirection * powerSpeed;
            batytEffect.Play();
        }
        direction.y = throwForce;
        return direction;
      

    }
    public float GetTimeDeactiveInput()
    {
        return timeDeactiveInput;
    }

}
