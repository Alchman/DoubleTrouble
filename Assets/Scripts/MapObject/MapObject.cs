using System.Collections;
using UnityEngine;

public class MapObject : MonoBehaviour{

    [SerializeField][Tooltip("Время на которое отключается кинематика обьекта для пинания")] private float timeOfKinematic =1f;
    
    private Coroutine coroutineOfKinematic;
    private Pushable pushable;
    private Rigidbody rb;
    private void Start() {

        rb = GetComponent<Rigidbody>();
        pushable = GetComponent<Pushable>();
        pushable.PushObject += PuchControl;
        StartCoroutine(OfKinematic(timeOfKinematic * 4));
    }
 
    private void PuchControl() {
        // print("PuchControl");
           if(coroutineOfKinematic != null) {
            StopCoroutine(coroutineOfKinematic);
        }
        coroutineOfKinematic = StartCoroutine(OfKinematic(timeOfKinematic));
        
    }

    IEnumerator OfKinematic(float time) {
        rb.isKinematic = false;
        // print("corutin");
        yield return new WaitForSeconds(time);
        rb.isKinematic = true;
    }
    
}

