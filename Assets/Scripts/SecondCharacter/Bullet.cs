using UnityEngine;

public class Bullet : MonoBehaviour{
    private                  Rigidbody rb;
    [SerializeField] private float     speed = 1f;

    void Start() {
        rb          = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void FireFamege() {
    }
}
