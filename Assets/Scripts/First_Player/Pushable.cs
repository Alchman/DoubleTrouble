using System;
using UnityEngine;

public class Pushable : MonoBehaviour
{
    [Tooltip("Коэффициент массы: Изменяется от 0 до 1." +
        "На него умножается сила толчка." +
        "При значении 1 - отлетает с полной силой." +
        "При значении 0.5 - с половиной." +
        "При значении 0 - не летит ")] [Range(0, 1)] [SerializeField] float massCoef=0.5f;
    [Tooltip("Можно или нельзя пнуть предмет во время бега ")] [SerializeField] bool pushOnRun;
    [SerializeField] int returnDamage;

    [Tooltip("Высота полёта предмета при ударе")] [SerializeField] float pushHeight = 10;
    [SerializeField] private AudioClip impactSound;
    private Animator animator;
    
    public Action PushObject = delegate {};

    public bool IsOnGround { get; private set; }
    Rigidbody rb;
    public bool PushOnRun { get { return pushOnRun; } }

    void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        IsOnGround = true;
        //Invoke(nameof(ResetGround), 0.2f);
    }

    public int Push(Vector3 force, bool ignoreGround= false)
    {
       
        if (IsOnGround || ignoreGround == true)
        { 
            //rb.isKinematic = false;
            PushObject();
            force *= massCoef;
            force.y = +pushHeight;
            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(force, ForceMode.Impulse);
            IsOnGround = false;
           
            
            if (impactSound != null)
            {
      
                AnimPushBanka();
                AudioManager.PlaySound(impactSound);
            }


            Invoke(nameof(ResetGround), 3f);
            return returnDamage;
        }
        return  0;
      
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        ResetGround();
    }

    void ResetGround()
    {
        //rb.isKinematic = true;
        IsOnGround = true;
    }

    private void AnimPushBanka() {

        if(animator != null) {
            animator.SetTrigger("Sand_Poof");
        }
   
        
    }


}
