using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    private SecondCharacter _secondCharacter;
    void Start()
    {
        _secondCharacter = FindObjectOfType<SecondCharacter>();
    }

    void Update()
    {
        Vector3 target = _secondCharacter.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, target, 0.1f);
    }
}
