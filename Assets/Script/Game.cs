using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SWNetwork;
using UnityEngine.UI;
using UnityEngine.Events;
using System;


public class Game : MonoBehaviour
{
    // Start is called before the first frame update
    public List<string> playerName = new List<string>();
    public  List<string> playerId = new List<string>();
    int  l ;
    public GameObject playerArea;
    float playerAreaPosX;
    float playerAreaPosY;
    public GameObject Card;
    public Text Player;
    public GameObject DrawCards;
    public GameObject Submit;
    public GameObject Finalize;
  
    const string FINAL = "final";
    const string TURNSYSTEM = "TurnSystem";
    const string ONGAMESTART = "onGameStart";
  
    public Text Turn;
    public Text Result;
   
    RoomRemoteEventAgent roomRemoteEventAgent;
  
 
    MatchingCards MatchingCards;
    [SerializeField] GameObject matchcards;

    [SerializeField] GameObject TIMER;
    Timer Timer;
   
    public List<string> CardMatch = new List<string>();
    
    string FirstCard ;
    string SecondCard;
     RoomPropertyAgent roomPropertyAgent;
    const string FIRSTCARD = "FirstCard";
    const string SECONDCARD = "SecondCard";

    const string TIMERSET = "Timer";
  
    int i=0;
   int j=1;
   string Name;
   //bool Mark = true;
 
   
 
     

      void Awake()
        {
            

            NetworkClient.Lobby.GetPlayersInRoom((successful, reply, error) =>
            {
                if (successful)
                {
                     foreach(SWPlayer swPlayer in reply.players)
                    {
                        string PlayerName = swPlayer.GetCustomDataString();
                        string PlayerId = swPlayer.id;

                        playerName.Add(PlayerName);
                        playerId.Add(PlayerId);
                        
                        
                        if(PlayerId.Equals(NetworkClient.Instance.PlayerId)){
                            Player.text = ""+PlayerName;
                        }
                        
                    }
                       
                    roomRemoteEventAgent = FindObjectOfType<RoomRemoteEventAgent>();
                   // Debug.Log(roomRemoteEventAgent);
                     roomPropertyAgent = FindObjectOfType<RoomPropertyAgent>();            
        //Debug.Log(roomPropertyAgent);
        Timer = TIMER.GetComponent<Timer>();
        l = playerName.Count;
       // Debug.Log(l);
       

                    onGameStart();
                   
                   
                }
                else
                {
                    Debug.Log("Failed to get players in room.");
                }

        });




      
        }



        void OnDestroy(){
            Result.text = " ";
            DrawCards.SetActive(false);
            Finalize.SetActive(false);
            Submit.SetActive(false);
//playerArea.SetActive(false);
          
        }
       
      
       public void onGameStart(){
            if(playerId[i].Equals(NetworkClient.Instance.PlayerId)){
                Turn.text = "Your turn to Describe the card";
                DrawCards.SetActive(false);
                Finalize.SetActive(true);
               // playerArea.SetActive(true);
                Submit.SetActive(false);
                //OnSetPlayerArea();
                OnClick();
               
                
            }else{
                DrawCards.SetActive(false);
                Finalize.SetActive(false);
                Submit.SetActive(false);
                playerArea.SetActive(false);
            
                Turn.text = playerName[i]+" turn to Describe the card";
            
            }
        }

        

       
       
        public void onfinalButtonClick(){
            SWNetworkMessage message = new SWNetworkMessage();
           message.Push(true); 
           roomRemoteEventAgent.Invoke(FINAL,message);
        
        }

        public void OnMatched(){
            MatchingCards = matchcards.GetComponent<MatchingCards>();
            Name = MatchingCards.cardName;
            roomPropertyAgent.Modify(FIRSTCARD, Name);
        
        }

         public void SecondCardData(){
            MatchingCards = matchcards.GetComponent<MatchingCards>();
            Name = MatchingCards.cardName;
            //Debug.Log(Name);
            roomPropertyAgent.Modify(SECONDCARD, Name);
        
        }

        public void RoomProprtyFunction(){
            FirstCard =  roomPropertyAgent.GetPropertyWithName(FIRSTCARD).GetStringValue();
           // Debug.Log(FirstCard);
        }

       


        public void OnSelectButoon(){
            Timer.StopTimer();
            SWNetworkMessage message1 = new SWNetworkMessage();
           message1.Push(true);
           roomRemoteEventAgent.Invoke(TURNSYSTEM,message1);
        }

        public void RoomProprtyFunctionSecoundCard(){
            SecondCard =  roomPropertyAgent.GetPropertyWithName(SECONDCARD).GetStringValue();
            //Debug.Log(SecondCard);
        }
      
      
           

     public  void final(SWNetworkMessage msg){
        
        //Debug.Log(msg);        
        
       
            if(playerId[j].Equals(NetworkClient.Instance.PlayerId)){
                    
                    DrawCards.SetActive(false);
                    Submit.SetActive(true);
                    Finalize.SetActive(false);
                    playerArea.SetActive(true);
                    //OnClick();
                    Invoke("OnClick", 0);
                   // Timer.StartTimer(20);
                    Turn.text = "Your Turn to Select the card";
                    Timer.StartTimer(20);
                    }else{
                        //Debug.Log(CardMatch);                       
                        Turn.text = playerName[j]+" Turn to Select the card";
                    }
        
}

   
       

     public   void TurnSystem(SWNetworkMessage msgg){   
       // Debug.Log(FirstCard);
        //  Debug.Log(SecondCard);
            if(FirstCard.Equals(SecondCard)){
                
                if(playerId[j].Equals(NetworkClient.Instance.PlayerId)){
                    Result.text = "YOU WON";
                    
                    
                    }else{
                        //Debug.Log(CardMatch);                       
                        Result.text = playerName[j]+" WON";
                    } 
                    TurnChangeCorrect();
                 CloneDestroy();
                 Invoke("OnDestroy", 2);
                  
                   Invoke("onGameStart", 3);
                   
            }else{
                     if(playerId[j].Equals(NetworkClient.Instance.PlayerId)){
                    Result.text = "YOU LOST";
                    CloneDestroy();
                    
                    }else{
                        //Debug.Log(CardMatch);                       
                        Result.text = playerName[j]+" LOST";
                    } 
                  //  Result.text = playerName[j] + " Lost";
                  TurnChangeWrong();
                    //CloneDestroy();
                 //OnDestroy();
                  
                  //onfinalButtonClick(); 
                  if(j==i){
            Result.text = "Round Ended";
            //j = j-1;
            TurnChangeCorrect();
            CloneDestroy();
                 Invoke("OnDestroy", 2);
                  
                   Invoke("onGameStart", 3);

        }else{
                  final(msgg);
        }

                }
                    
     }
        
 void TurnChangeCorrect(){
    if(i==l-2){
        i++;
        j=0;
    }else if(i<l-2){
        i++;
        j=i+1;

    } else{
        i=0;
        j=i+1;
    }
     
}

void TurnChangeWrong(){
    if(j<l-1){
        j++;
        
    }else{
        j=0;
        
    }
    }
          
 void OnClick()
    {
        for(int i=0; i < 4; i++){
            GameObject card = Instantiate(Card, new Vector2(0,0), Quaternion.identity);
            card.transform.SetParent(playerArea.transform, false);

        
    }
    }

  public void CloneDestroy(){
    
       
        GameObject[] cards;
        cards = GameObject.FindGameObjectsWithTag("Cards");
        //Debug.Log(cards.Length);
        foreach(GameObject card in cards){
        Destroy(card);
        }
}

public void OnTimeOverSet(){
     SWNetworkMessage message = new SWNetworkMessage();
           message.Push(true); 
           roomRemoteEventAgent.Invoke(TIMERSET,message);
}

public void TimerOverSet(SWNetworkMessage msgg){
    Result.text = "TIME UP";
    CloneDestroy();
    TurnChangeWrong();
    if(j==i){
            Result.text = "Round Ended";
            //j = j-1;
            TurnChangeCorrect();
         //   CloneDestroy();
                 Invoke("OnDestroy", 2);
                  
                   Invoke("onGameStart", 3);

        }else{
                  final(msgg);
        }
}


}