using UnityEngine;

public class Bullet : MonoBehaviour{
    private Rigidbody rb;
    //
    // [SerializeField] [Tooltip("Скорость пули")]
    private float speed = 40f;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    public void FireDamege(float damage, float speed) {
        rb.velocity = transform.forward * speed;
    }
}
