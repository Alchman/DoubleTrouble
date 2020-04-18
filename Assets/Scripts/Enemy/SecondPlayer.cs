using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondPlayer : MonoBehaviour{
    private Rigidbody rb;
    private Enemy     enemy;

    public                   float     sphereRadius;
    public                   float     maxDistance;
    public                   LayerMask layerMask;
    private                  Collider  target;
    private                  float     futureTimeForTarget;
    [SerializeField] private Transform Bullet;
    [SerializeField] private float     ReloadBullet = 1f;
    [SerializeField] private float     RadiusCanon  = 50f;



    void Start() {
        rb                  =  GetComponent<Rigidbody>();
        futureTimeForTarget += Time.time + 1f;
    }

    void Update() {
        CheckEnemy();
        AutoShooting();
    }

    private void CheckEnemy() {

        Collider[] allItemsInRadius = Physics.OverlapSphere(transform.position, RadiusCanon, layerMask);
     

        float minDistance = float.MaxValue;
       

        foreach(Collider item in allItemsInRadius) {
           
            var distance = Vector3.Distance(transform.position, item.transform.position);
            if(distance < minDistance) {
                target      = item;
                minDistance = distance;
            }
        }

    }

    private void AutoShooting() {

        if(target == null) {
            return;
        }

        transform.LookAt(target.transform.position);
        BulletFly();
    }

    private void BulletFly() {
        if(Time.time > futureTimeForTarget) {
            Instantiate(Bullet, transform.position, transform.rotation);
            futureTimeForTarget = Time.time + ReloadBullet;

        }
    }

}