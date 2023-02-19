using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using SWNetwork;

namespace GameLogic{
public class Lobby1 : MonoBehaviour
{
    // Start is called before the first frame update
    

    public enum LobbyState
    {
        Default,
        JoinedRoom,
    }
    public LobbyState State = LobbyState.Default;
    public InputField NicknameInputField;
     public GameObject StartRoomButton;
    public bool Debugging = false;
    //public Text Player;

    string username;



    private void Start()
    {
        StartRoomButton.SetActive(false);
        NetworkClient.Lobby.OnLobbyConnectedEvent += OnLobbyConnected;
        NetworkClient.Lobby.OnNewPlayerJoinRoomEvent += OnNewPlayerJoinRoomEvent;
        NetworkClient.Lobby.OnRoomReadyEvent += OnRoomReadyEvent;

    }

   private void OnDestroy()
    {
       // NetworkClient.Lobby.OnLobbyConnectedEvent -= OnLobbyConnected;
           if (NetworkClient.Lobby != null)
            {
                NetworkClient.Lobby.OnLobbyConnectedEvent -= OnLobbyConnected;
                NetworkClient.Lobby.OnNewPlayerJoinRoomEvent -= OnNewPlayerJoinRoomEvent;
            }

    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Checkin()
		{
			NetworkClient.Instance.CheckIn(username, (bool successful, string error) =>
			{
				if (!successful)
				{
					Debug.LogError(error);
                   // Player = username.ToString();
				}
			});
		}

        void RegisterToTheLobbyServer(){
            NetworkClient.Lobby.Register(username, (successful, reply, error) => {
            if (successful)
            {
                Debug.Log("Lobby registered " + reply);
                if (string.IsNullOrEmpty(reply.roomId))
                {
                    JoinOrCreateRoom();
                }
                else if (reply.started)
                {
                    State = LobbyState.JoinedRoom;
                    ConnectToRoom();
                }
                else
                {
                    State = LobbyState.JoinedRoom;
                   // ShowJoinedRoomPopover();
                    GetPlayersInTheRoom();
                }
            }
            else
            {
                Debug.Log("Lobby failed to register " + reply);
            }
        });
        }

        void JoinOrCreateRoom()
        {
            NetworkClient.Lobby.JoinOrCreateRoom(false, 4, 60, (successful, reply, error) => {
                if (successful)
                {
                    Debug.Log("Joined or created room " + reply);
                    State = LobbyState.JoinedRoom;
                    //ShowJoinedRoomPopover();
                    GetPlayersInTheRoom();
                }
                else
                {
                    Debug.Log("Failed to join or create room " + error);
                }
            });
        }

        void GetPlayersInTheRoom()
        {
            NetworkClient.Lobby.GetPlayersInRoom((successful, reply, error) => {
                if (successful)
                {
                    Debug.Log("Got players " + reply);
                    if(reply.players.Count <= 3)
                    {
                       // Player1Portrait.SetActive(true);
                       Debug.Log("player has to join the room");
                    }
                    else
                    {
                        //Player1Portrait.SetActive(true);
                        //Player2Portrait.SetActive(true);

                        if (NetworkClient.Lobby.IsOwner)
                        {
                            StartRoomButton.SetActive(true);

                        }else{
                            StartRoomButton.SetActive(false);
                        }
                            //ShowReadyToStartUI();
                                                    
                    }
                }
                else
                {
                    Debug.Log("Failed to get players " + error);
                }
            });
        }

        void StartRoom()
        {
            NetworkClient.Lobby.StartRoom((successful, error) => {
                if (successful)
                {
                    Debug.Log("Started room.");
                }
                else
                {
                    Debug.Log("Failed to start room " + error);
                }
            });
        }


        void ConnectToRoom()
        {
            // connect to the game server of the room.
            NetworkClient.Instance.ConnectToRoom((connected) =>
            {
                if (connected)
                {
                    //SceneManager.LoadScene("MultiplayerGameScene");
                    Debug.Log("Room is Connected");
                    SceneManager.LoadScene("GameSecen");
                }
                else
                {
                    Debug.Log("Failed to connect to the game server.");
                }
            });
        }



/*.......................Lobby Event..........*/
        void OnLobbyConnected()
		{
            RegisterToTheLobbyServer();
		}

        void OnNewPlayerJoinRoomEvent(SWJoinRoomEventData eventData)
        {
            
                //ShowReadyToStartUI();
                StartRoomButton.SetActive(true);
            
        }

         void OnRoomReadyEvent(SWRoomReadyEventData eventData)
        {
            ConnectToRoom();
        }
        

        public void OnStartRoomClicked()
        {
            Debug.Log("OnStartRoomClicked");
            // players are ready to player now.
            if (Debugging)
            {
                SceneManager.LoadScene("GameSecen");
            }
            else
            {
                // Start room
                StartRoom();
            }
        }



     public void OnConfirmNicknameClicked()
        {
            username = NicknameInputField.text;
            Debug.Log($"OnConfirmNicknameClicked: {username}");

           /* if (Debugging)
            {
                ShowJoinedRoomPopover();
                ShowReadyToStartUI();
            }
            else
            {*/
				//Use nickname as player custom id to check into SocketWeaver.
				Checkin();
            
        }

      
}
}
