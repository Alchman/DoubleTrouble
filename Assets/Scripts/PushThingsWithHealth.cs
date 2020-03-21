using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushThingsWithHealth : MonoBehaviour
{
    [SerializeField] float health;
    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Damage()
    {
        health -= playerController.GetDamage();
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
    



}
