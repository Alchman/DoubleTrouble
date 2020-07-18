using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagebleByPush : MonoBehaviour
{
    [Tooltip("Коэффициент урона:На него умножается количество урона." +
        "При значении 1 - наносится полный урон." +
        "При значении 0.5 - половина урона." +
        "При значении 0 - не наносится урон")] [Range(0, 1)] [SerializeField] float damageCoef=1;

    Health health;
    void Start()
    {
        health = GetComponent<Health>();
    }

    public void DoDamage(float damage)
    {
        damage *= damageCoef;
        health.ChangeHealth(-damage);
    }
}
