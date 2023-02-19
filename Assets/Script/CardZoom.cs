using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardZoom : MonoBehaviour
{
    public GameObject ZoomCard;
    public GameObject Canvas;

    private GameObject zoomCard;
    private Sprite zoomSprite;
    public GameObject Cards;

    public void Awake()
    {
        //Canvas = GameObject.Find("Main Canvas");
        zoomSprite = Cards.GetComponent<Image>().sprite;
    }
   
    public  void OnMouseDown()
   {
        //if (!hasAuthority) return;
       zoomCard = Instantiate(ZoomCard, new Vector2(Input.mousePosition.x, Input.mousePosition.y + 10), Quaternion.identity);
        zoomCard.GetComponent<Image>().sprite = zoomSprite;
// zoomCard.transform.SetParent(Canvas.transform, true);
      // RectTransform rect = zoomCard.GetComponent < RectTransform>();
     // rect.sizeDelta = new Vector2(2, 5);
        
//Vector3 s = Vector3.one*2;
//this.transform.localScale = s;

    }
}
