using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField]  Slider healthBar;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        healthBar.maxValue = playerController.GetHealth();
    }

    // Update is called once per frame
    void Update()
    {
        Slider();
    }
    public void Slider()
    {
        healthBar.value = playerController.GetHealth();
    }
}
