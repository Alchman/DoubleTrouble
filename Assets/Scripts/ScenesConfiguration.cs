using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesConfiguration : MonoBehaviour
{
    public GameObject enabledMenu;
   
    // Start is called before the first frame update
    void Start()
    {

    }
    public void LoadNextScene()
    {
        Scene activeScene = SceneManager.GetActiveScene();
        int currentSceneIndex = activeScene.buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);

    }


    public void ExitGame()
    {
        Application.Quit();
    }

    public void LoadScene(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        enabledMenu.SetActive(false);


    }


    // Update is called once per frame
    void Update()
    {

    }
}
