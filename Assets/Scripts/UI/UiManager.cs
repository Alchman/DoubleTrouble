using UnityEngine;

public class UiManager : MonoBehaviour{

    [SerializeField] private GameObject PanelGameOver;

    [Header("Resources")]
    [SerializeField] private ResoursesUI resourcesTable;
    [SerializeField] private float showResourcesDistance = 10f;

    private Health health;
    private SecondPlayer secondPlayer;

    private void Start() {
        health = SecondPlayer.Instance.GetComponent<Health>();
        health.OnDeath += GameOver;
    }

    private void Update()
    {
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
        //PanelGameOver.SetActive(true);
        // Time.timeScale = 0;

    }
}
