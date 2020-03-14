using UnityEngine;

public class SecondCharacter : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = -Vector3.right * 10f;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = Vector3.right * 10f;
        }
    }
}