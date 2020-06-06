using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ResoursesUI : MonoBehaviour
{
    public Text pistol;
    public Text rifle;
    public Text wood;
    public Text gears;
    public Text stone;
    public Text metal;
    public Text rocket;

    [Tooltip("Скорость выезда таблицы")] [SerializeField] [Range(0, 1)] float duration;

    RectTransform rectTransform;
    SecondPlayer player;

   

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

    }
    // Start is called before the first frame update
 

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
    public void Show()
    {
        gameObject.SetActive(true);
        rectTransform.DOAnchorPosX(620, duration);
       

    }
    public void Hide()
    {
        rectTransform.DOAnchorPosX(620, duration).SetRelative(true).OnComplete(() => gameObject.SetActive(false));
    }

}






