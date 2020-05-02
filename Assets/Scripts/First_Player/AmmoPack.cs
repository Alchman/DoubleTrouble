using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPack : MonoBehaviour
{
    public BulletType bulletType;
   [SerializeField] int amount;

    SecondPlayer secondPlayer;
    // Start is called before the first frame update
    void Start()
    {
        secondPlayer = GetComponent<SecondPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "SecondPlayer")
        {
            Destroy(gameObject);
            secondPlayer.AddAmmo(bulletType, amount);

        }
    }
}
