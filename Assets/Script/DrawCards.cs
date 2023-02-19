using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Mirror;


public class DrawCards : MonoBehaviour
{
  //  public PlayerManager PlayerManager;
//public Button btn;
  public GameObject Card;
  
public GameObject PlayerArea;
   

    


    public void OnClick()
    {
        for(int i=0; i < 4; i++){
            GameObject card = Instantiate(Card, new Vector2(0,0), Quaternion.identity);
            card.transform.SetParent(PlayerArea.transform, false);
        }
    }

    
}
