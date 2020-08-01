using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModificator : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Модификатор изменения скорости, если значение < 0, то персонаж замедляется, " +
        "если значение > 1  скорость персонажа увеличивается")]
    [Range(0, 2)] float speedFactor;

    public float GetSpeedFactor()
    {
        return speedFactor;
    }
}
