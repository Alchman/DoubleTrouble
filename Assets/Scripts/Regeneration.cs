﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Regeneration : MonoBehaviour

{
    [Tooltip("Кол-во здоровья для восстановления")] [SerializeField] float regeneration;
    


    CharacterHp characterHp;
    SecondCharacter secondCharacter;
    // Start is called before the first frame update
    void Start()
    {
        characterHp = FindObjectOfType<CharacterHp>();
        secondCharacter = FindObjectOfType<SecondCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "SecondPlayer")
        {
            characterHp.RegenerationHealth(regeneration);
            Destroy(gameObject);
        }
    }
      

   
}
