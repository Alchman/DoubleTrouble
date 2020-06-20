using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedInPlane : MonoBehaviour
{
    [SerializeField] [Range(0, 2)] float speedAccelarationForPlane;

   public float GetSpeedAcceleration()
    {
        return speedAccelarationForPlane;
    }
}
