using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpForce : MonoBehaviour
{
    [Tooltip("Сила с которой трамплин подкинет вверх")] [SerializeField] float forceUp;

    public float GetForce()
    {
        return forceUp;
    }

}
