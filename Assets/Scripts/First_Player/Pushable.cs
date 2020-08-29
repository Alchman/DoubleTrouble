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

    [Tooltip("Высота полёта предмета при ударе")] [SerializeField] float pushHeight = 10;
    [SerializeField] private AudioClip impactSound;
    
    public Action PushObject = delegate {};

    bool isOnGround;
    Rigidbody rb;
    public bool PushOnRun { get { return pushOnRun; } }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        isOnGround = true;
        //Invoke(nameof(ResetGround), 0.2f);
    }

    public void Push(Vector3 force, bool ignoreGround= false)
    {
        if (isOnGround || ignoreGround == true)
        {
            //rb.isKinematic = false;
            PushObject();
            force *= massCoef;
            force.y = +pushHeight;
            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(force, ForceMode.Impulse);
            isOnGround = false;
            
            if (impactSound != null)
            {
                AudioManager.PlaySound(impactSound);
            }


            Invoke(nameof(ResetGround), 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ResetGround();
    }

    void ResetGround()
    {
        //rb.isKinematic = true;
        isOnGround = true;
    }


}
