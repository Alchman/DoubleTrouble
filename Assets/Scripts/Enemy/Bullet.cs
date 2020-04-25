using UnityEngine;

[RequireComponent(typeof(DamageDealer))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    //
    // [SerializeField] [Tooltip("Скорость пули")]
    private float speed = 40f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FireBullet(float damage, float speed)
    {
        DamageDealer dd = GetComponent<DamageDealer>();
        dd.damage = damage;

        rb.velocity = transform.forward * speed;
    }
}
