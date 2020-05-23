using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public float speed;
    public GameObject camera;
    public GameObject player;
    Vector3 oldPosition;
    bool moveCamera = false;

    void Update()
    {

        Vector3 pos = transform.position;
      
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Time.timeScale = 0;
            camera.SetActive(false);
             oldPosition = transform.position;
            moveCamera = true;
        }
        if (moveCamera)
        {
            pos.z += Input.GetAxisRaw("Vertical") * speed * Time.unscaledDeltaTime;
            pos.x += Input.GetAxisRaw("Horizontal") * speed * Time.unscaledDeltaTime;
            transform.position = pos;
        }
        
        if (Input.GetKeyDown(KeyCode.F2))
        {
            moveCamera = false;
            Time.timeScale = 1;
            transform.position = oldPosition;
            camera.SetActive(true);
         
           
        }


    }

  

       
}