using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    public float speed;
    public Camera mainCamera;
    public Camera camera;
  
    
    bool moveCamera = false;

  
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {

            if (moveCamera)
            {
                DisableMove();
            }
            else
            {
                EnabledMove();
            }
          
        }
        if (moveCamera)
        {
            MoveCamera();
        }

    }

    private void MoveCamera()
    {
        Vector3 pos = camera.transform.position;
        pos.z += Input.GetAxisRaw("Vertical") * speed * Time.unscaledDeltaTime;
        pos.x += Input.GetAxisRaw("Horizontal") * speed * Time.unscaledDeltaTime;
        camera.transform.position = pos;
    }

    private void EnabledMove()
    {
        Time.timeScale = 0;
        camera.transform.position = mainCamera.transform.position;
        camera.transform.rotation = mainCamera.transform.rotation;
        camera.orthographicSize = mainCamera.orthographicSize;
        mainCamera.gameObject.SetActive(false);
        camera.gameObject.SetActive(true);
        moveCamera = true;
    }

    private void DisableMove()
    {
        mainCamera.gameObject.SetActive(true);
        camera.gameObject.SetActive(false);
        moveCamera = false;
        Time.timeScale = 1;
    }

  
}