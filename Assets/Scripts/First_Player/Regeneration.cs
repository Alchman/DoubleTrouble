using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour
{
    // [SerializeField][Tooltip("эффект лута")] private Animator animEffectLoot;
    [SerializeField][Tooltip("время ожидания эффекта")] private float waitEffect =1;
    private AnimEffect animEffectLoot;
    private Animator animator;
    private void Start() {
        animEffectLoot = FindObjectOfType<AnimEffect>();
        animator = animEffectLoot.GetComponent<Animator>();
    }
    // Achive_Patrons
    //  Achive_Shest
    // Acive_Smt
    //  Achive_Heal  зеленый

    [Tooltip("Кол-во восстановления здоровья ")] public int health=30;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer") {
            StartCoroutine(EffectWait());
            
            SecondPlayer.Instance.HealthUpdate(health);
            QuestManager.Instance.CheckQuests(QuestManager.QuestStates.PUSHTOMIKE);
        }
    }

    IEnumerator EffectWait() {
        yield return new WaitForSeconds(waitEffect);
        animator.SetTrigger("Achive_Heal");
        Destroy(gameObject);
    }
}
