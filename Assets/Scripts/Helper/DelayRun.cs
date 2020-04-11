using System;
using System.Collections;
using UnityEngine;

public class DelayRun : MonoBehaviour{
    public static void Execute(Action callback, float timer, GameObject targetObject) {
        var runComponent = targetObject.AddComponent<DelayRun>();
        runComponent.Execute(callback, timer);
    }

    private void Execute(Action callback, float timer) {
        StartCoroutine(WaitAndExecute(callback, timer));
    }

    private IEnumerator WaitAndExecute(Action callback, float timer) {
        yield return new WaitForSeconds(timer);
        callback?.Invoke();
        Destroy(this);
    }
}
