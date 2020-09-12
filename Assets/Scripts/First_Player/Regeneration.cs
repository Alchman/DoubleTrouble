using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Regeneration : MonoBehaviour
{
    // [SerializeField][Tooltip("эффект лута")] private Animator animEffectLoot;
    [SerializeField][Tooltip("время ожидания эффекта")] private float waitEffect =1;
    [Tooltip("Кол-во восстановления здоровья ")] public int health=30;
    [Tooltip("Дилей включения триггера ")] public float triggerDelay = 2f;
    
    private AnimEffect animEffectLoot;
    private Animator animator;
    private Rigidbody rb;
    
    
    // Achive_Patrons
    //  Achive_Shest
    // Acive_Smt
    //  Achive_Heal  зеленый

    private float delay;
    
    private void Start() {
        rb = GetComponent<Rigidbody>();
        animEffectLoot = FindObjectOfType<AnimEffect>();
        animator = animEffectLoot.GetComponent<Animator>();
        delay = triggerDelay;
    }

    
    
    private void OnTriggerEnter(Collider other)
    {
        if (delay > 0) return;
        
        if (other.gameObject.tag == "SecondPlayer") {
            StartCoroutine(EffectWait());
            
            rb.isKinematic = true;
            gameObject.transform.DOMove(SecondPlayer.Instance.transform.position, SecondPlayer.Instance.takeItemTime);
            SecondPlayer.Instance.HealthUpdate(health);
            QuestManager.Instance.CheckQuests(QuestManager.QuestStates.PUSHTOMIKE);
        }
    }

    private void Update()
    {
        delay -= Time.deltaTime;
    }

    IEnumerator EffectWait() {
        yield return new WaitForSeconds(waitEffect);
        animator.SetTrigger("Achive_Heal");
        Destroy(gameObject);
    }
}
