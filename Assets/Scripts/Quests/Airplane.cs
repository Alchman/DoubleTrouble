using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : GenericSingletonClass<Airplane>
{
    [SerializeField] GameObject pos;
    private void Start()
    {
        transform.position = pos.transform.position;
    }
}
