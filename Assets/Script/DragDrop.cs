using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Mirror;
using SWNetwork;
using UnityEngine.UI;
using UnityEngine.Events;


public class DragDrop : MonoBehaviour
{
    public  GameObject button;
   
    public Text cardText; 
  //  public Text Result;
   // public string cardName ;
   // public bool example = true;
    MatchingCards MatchingCards;
    [SerializeField] GameObject matchcards;
  
     //  RoomRemoteEventAgent roomRemoteEventAgent;
      // const string ONMATCH = "OnMatch";
      // string c;
       

      
 
    public void Selection(){
       string c = cardText.text;
      // Debug.Log(c);
        button.GetComponent<Image>().color = Color.blue;
        MatchingCards = matchcards.GetComponent<MatchingCards>();
       MatchingCards.matchCard(c);
      }


        
    

   
}


