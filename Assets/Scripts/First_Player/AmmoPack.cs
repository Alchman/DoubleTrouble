using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    [Tooltip("Выбор типа пули ")] public BulletType bulletType;
    [Tooltip("Количество пуль в паке ")] [SerializeField] int amount= 10;

   [SerializeField][Tooltip("время ожидания эффекта")] private float    waitEffect =1;
    // Achive_Patrons
    
    private AnimEffect animEffectLoot;
    private Animator   animator;
    private void Start() {
        animEffectLoot = FindObjectOfType<AnimEffect>();
        animator       = animEffectLoot.GetComponent<Animator>();
    }
    
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            StartCoroutine(EffectWait());
            // Destroy(gameObject,1f);
            SecondPlayer.Instance.AddAmmo(bulletType, amount);

        }
    }
    
    IEnumerator EffectWait() {
        yield return new WaitForSeconds(waitEffect);
        animator.SetTrigger("Achive_Patrons");
        Destroy(gameObject);
    }
}
