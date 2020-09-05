using System.Collections;
using UnityEngine;
using DG.Tweening;

public class Resources : MonoBehaviour
{
    [Tooltip("Присвоение предмету определенного ресурса")] public ResourceType resourceType;
    [Tooltip("Количество ресурса ")] public int count=1;

    [SerializeField][Tooltip("время ожидания эффекта")] private float    waitEffect =1;
    
    // Achive_Patrons
    //  Achive_Shest
    // Acive_Smt
    //  Achive_Heal  зеленый
    
    private AnimEffect  animEffectLoot;
    private Animator   animator;
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animEffectLoot = FindObjectOfType<AnimEffect>();
        animator       = animEffectLoot.GetComponent<Animator>();
    }
    
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            // Destroy(gameObject,1f);
            StartCoroutine(EffectWait());
            rb.isKinematic = true;
            gameObject.transform.DOMove(SecondPlayer.Instance.transform.position, SecondPlayer.Instance.takeItemTime);
            SecondPlayer.Instance.AddResourses(resourceType, count);
            QuestManager.Instance.CheckQuests(QuestManager.QuestStates.KICKBIGCHUNK);
        }
    }
    
    IEnumerator EffectWait() {
        yield return new WaitForSeconds(waitEffect);
        animator.SetTrigger("Achive_Shest");
        Destroy(gameObject);
    }
}
