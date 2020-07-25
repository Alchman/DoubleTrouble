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

    [SerializeField][Tooltip("сила удара для пинания вверх")]  private float pushEffect = 0;
    
    public Action PushEnemy = delegate {};
    
    bool  isOnGround;
    Rigidbody rigidbody;
    public bool PushOnRun { get { return pushOnRun; } }

    void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        isOnGround = true;
    }

    public void Push(Vector3 force, bool ignoreGround= false)
    {
        if (isOnGround || ignoreGround==true)
        {
            rigidbody.isKinematic = false;
            PushEnemy();
            force *= massCoef;
            force.y = +pushEffect;
            rigidbody.AddForce(force, ForceMode.Impulse);
            rigidbody.AddTorque(force, ForceMode.Impulse);  
            isOnGround = false;

            Invoke(nameof(ResetGround), 1f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ResetGround();
    }

    void ResetGround()
    {
        rigidbody.isKinematic = true;
        isOnGround = true;
    }
}
