using System.Collections;
using UnityEngine;

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
    
    private void Start() {
        animEffectLoot = FindObjectOfType<AnimEffect>();
        animator       = animEffectLoot.GetComponent<Animator>();
    }
    
 
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            // Destroy(gameObject,1f);
            StartCoroutine(EffectWait());
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
