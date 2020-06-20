using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedModificator : MonoBehaviour
{
    [SerializeField] [Range(0, 2)] float speedFactor;

   public float GetSpeedFactor()
    {
        return speedFactor;
    }
}
