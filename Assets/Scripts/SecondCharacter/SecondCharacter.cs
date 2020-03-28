using UnityEngine;

public class SecondCharacter : MonoBehaviour
{
    private Rigidbody rb;
    private Enemy enemy;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;
    private Collider target;
    private float futureTimeForTarget;
    [SerializeField] private Transform Bullet;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        futureTimeForTarget += Time.time + 1f;
    }

    void Update()
    {
        CheckEnemy();
      Shooting();
    }


    private void CheckEnemy() {
        // if(futureTimeForTarget < Time.time) {
        // return;    
        // }
      // print("futureTimeForTarget");
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
        futureTimeForTarget += Time.time + 1f;
        // return target.transform.position;
        
    }

    private void  Shooting() {

        if(target == null) {
            return;
        }

        transform.LookAt(target.transform.position);
        BulletFly();
    }

    private void BulletFly() {
        if(Time.time > futureTimeForTarget) {
            
        }
        Instantiate(Bullet, transform.position, transform.rotation);
    }
   
}
