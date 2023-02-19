using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDataBase : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Card> cardList = new List<Card>();

 string name;
Sprite thisSprite;
public Image thatImage;
public Text nameText;
int i=0;

void Awake(){
cardList.Add(new Card("Lion",     Resources.Load<Sprite>("1")));
cardList.Add(new Card("bird", Resources.Load<Sprite>("2")));
cardList.Add(new Card("Tree", Resources.Load<Sprite>("3")));
cardList.Add(new Card("Tiger", Resources.Load<Sprite>("5")));

 int x = Random.Range(0, 4);
 
    name = cardList[x].name;
    thisSprite = cardList[x].thisImage;

    nameText.text = ""+name;
    thatImage.sprite = thisSprite;
    //i++;
}
void Update(){
   
}
}
