using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{

    public GameObject enabledMenu;
    public GameObject continueGame;
    public GameObject exitGame;
   
  





    private bool pause;
    // Start is called before the first frame update
    void Start()
    {
        pause = false;
        enabledMenu.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            enabledMenu.SetActive(true);

            if (!pause)
            {
                PauseActive();
              

            }
            else
            {
                PauseEnabled();
                enabledMenu.SetActive(false);
            }
          
          

        }
    }

    public void PauseActive()
    {
      
        Time.timeScale = 0;
        pause = true;
        enabledMenu.SetActive(true);

    }

    public void PauseEnabled()
    {
        Time.timeScale = 1;
        pause = false;
       
    }

    public void ContinueGame()
    {
        PauseEnabled();
        enabledMenu.SetActive(false);


        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(transform.position, transform.forward * 100, Color.yellow);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            Transform objHit = hit.transform;

            if (objHit.CompareTag("UI"))
            {

                Time.timeScale = 0;
                enabledMenu.SetActive(false);

            }


        }
    }

}
