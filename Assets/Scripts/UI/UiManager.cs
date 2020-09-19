using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {

    [SerializeField] private GameObject PanelGameOver;

    [Header("Resources")]
    [SerializeField] public Text gears;
    [SerializeField] public Text rifle;

    [SerializeField] private ResoursesUI resourcesTable;
    [SerializeField] private float showResourcesDistance = 10f;
    
    [Header("Health")]
    [SerializeField] public HealthUI firstPlayerHealth;
    [SerializeField] public HealthUI secondPlayerHealth;


    private SecondPlayer secondPlayer;

    private void Start() {
        SecondPlayer.Instance.Health.OnDeath += GameOver;
        FirstPlayer.Instance.Health.OnDeath += GameOver;

        firstPlayerHealth.SetHealth(FirstPlayer.Instance.Health);
        secondPlayerHealth.SetHealth(SecondPlayer.Instance.Health);
    }

    private void Update()
    {


        ShowResources();
        float distance = Vector3.Distance(SecondPlayer.Instance.transform.position, FirstPlayer.Instance.transform.position);
        if (distance < showResourcesDistance)
        {
            resourcesTable.Show();
        }
        else
        {
            resourcesTable.Hide();
        }
    }

    private void GameOver() {
        UIAudio.Instance.GameOverSound();
        // health.OnDeath -= GameOver;
        
        PanelGameOver.SetActive(true);
        Time.timeScale = 0;

    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowResources()
    {
        gears.text = SecondPlayer.Instance.GetResourses(ResourceType.GEARS).ToString();
        rifle.text = SecondPlayer.Instance.GetBullets(BulletType.RIFLE).ToString();
    }
}
