using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResoursesUI : MonoBehaviour
{
    public Text pistol;
    public Text rifle;
    public Text wood;
    public Text gears;
    public Text stone;
    public Text metal;
    public Text rocket;

    SecondPlayer player;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        wood.text = SecondPlayer.Instance.GetResourses(ResourceType.WOOD).ToString();
        gears.text = SecondPlayer.Instance.GetResourses(ResourceType.GEARS).ToString();
        stone.text = SecondPlayer.Instance.GetResourses(ResourceType.STONE).ToString();
        metal.text = SecondPlayer.Instance.GetResourses(ResourceType.METAL).ToString();

        pistol.text = SecondPlayer.Instance.GetBullets(BulletType.PISTOL).ToString();
        rifle.text = SecondPlayer.Instance.GetBullets(BulletType.RIFLE).ToString();
        rocket.text = SecondPlayer.Instance.GetBullets(BulletType.ROCKET).ToString();
    }
   
}
