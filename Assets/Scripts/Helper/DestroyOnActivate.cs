using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnActivate : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject);
    }
}
