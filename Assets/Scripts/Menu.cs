using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : GenericSingletonClass<Menu>
{

    public GameObject enabledMenu;
    public GameObject continueGame;
    public GameObject exitGame;
   
  





    private bool pause;
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isEditor)
        {
            PauseDisable();
        }
        else
        {
            PauseActive();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
           // enabledMenu.SetActive(true);

            if (!pause)
            {
                PauseActive();
            }
            else
            {
                PauseDisable();
            }
        }
    }

    public void PauseActive()
    {
      
        Time.timeScale = 0;
        pause = true;
        enabledMenu.SetActive(true);

    }

    public void PauseDisable()
    {
        Time.timeScale = 1;
        pause = false;
        enabledMenu.SetActive(false);
       
    }

    public void Quit()
    {
        Application.Quit();
    }
    

    public void ContinueGame()
    {
        PauseDisable();
        enabledMenu.SetActive(false);
        UIAudio.Instance.StartGameSound();


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
