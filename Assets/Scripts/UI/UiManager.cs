using UnityEngine;

public class UiManager : MonoBehaviour{

    [SerializeField] private GameObject PanelGameOver;
    
    private Health health;
    private SecondPlayer secondPlayer;

    private void Start() {
        secondPlayer = FindObjectOfType<SecondPlayer>();
        health = secondPlayer.GetComponent<Health>();
        health.OnDeath += GameOver;

    }
    private void GameOver() {
        print("PanelGameOver.SetActive(true);");
        PanelGameOver.SetActive(true);
        // Time.timeScale = 0;

    }
}
