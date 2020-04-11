using UnityEngine;

public class SecondCharacter : MonoBehaviour
{
    private Rigidbody rb;
    private Enemy enemy;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;
    private Collider target;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Move();
        CheckEnemy();
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

    private Vector3 CheckEnemy() {
        Collider[] allItemsInRadius = Physics.OverlapSphere(transform.position, 100f, layerMask);
       print(allItemsInRadius.Length);

        float     minDistance = float.MaxValue;
       // target = null;
        
        foreach (Collider item in allItemsInRadius)
        {
            // var direction = item.transform.position - transform.position;
            var distance = Vector3.Distance(transform.position, item.transform.position);
            if (distance < minDistance)
            {
                target      = item;
              minDistance = distance;
            }
        }
        print(target);
        target.transform.localScale = new Vector3(2, 2, 2) ;
        return target.transform.position;
    }

    private void  Shooting(Vector3 target) {
        
    }
}