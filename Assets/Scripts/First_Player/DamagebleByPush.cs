using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagebleByPush : MonoBehaviour
{
    [SerializeField] float damageCoef;

    Health health;
    // Start is called before the first frame update
    void Start()
    {
        health = GetComponent<Health>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void DoDamage(float damage)
    {
        damage *= damageCoef;
        //health update health
    }
}
