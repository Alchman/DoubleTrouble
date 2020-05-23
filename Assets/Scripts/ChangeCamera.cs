using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public float speed;
    public GameObject camera;

    void Update()
    {

        Vector3 pos = transform.position;
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 0;
            camera.SetActive(false);
        }
        pos.z += Input.GetAxis("Vertical") * speed * Time.unscaledDeltaTime;
        pos.x += Input.GetAxis("Horizontal") * speed * Time.unscaledDeltaTime;
        transform.position = pos;


    }

  

       
}