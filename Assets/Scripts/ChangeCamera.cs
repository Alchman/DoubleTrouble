using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    private Vector3 moveDirection = Vector3.zero;
    public Camera playСamera;
    public int speedCamera;
    public int rotSpeedCamera;
    public float gravity = 20.0f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 0;

            MoveCamera();
        }
     
    }

    public void MoveCamera()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
      



    }

       
}