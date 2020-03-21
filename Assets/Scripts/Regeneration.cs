using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour

{
    [SerializeField] float regeneration;

    SecondCharacter secondCharacter;
    // Start is called before the first frame update
    void Start()
    {
        secondCharacter = FindObjectOfType<SecondCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public float GetRegeneration()
    {
        return regeneration;
    }

   
}
