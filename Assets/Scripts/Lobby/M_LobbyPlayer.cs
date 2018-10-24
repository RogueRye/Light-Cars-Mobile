using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class M_LobbyPlayer : NetworkLobbyPlayer {


    public Button joinButton;
    public TMP_Text myText;
    public TMP_Text btnText;
    [HideInInspector]
    public LobbyManager owner;


    void Awake()
    {
      
    }

    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();
        //get parent somehow
        owner = NetworkManager.singleton as LobbyManager;
        if(owner.lobbyMenu != null)
            gameObject.transform.SetParent(owner.lobbyMenu.transform.GetChild(0));
        

       
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Init();
    }


    private void Init()
    {
        

        myText.text = string.Format("Player {0}", owner.numPlayers);

        if (isLocalPlayer)
        {           
           
            joinButton.enabled = true;
            btnText.text = "JOIN";
        }
        else
        {
           
            joinButton.enabled = false;
            btnText.text = "...";
        }
    }


    public void JoinMatch()
    {
        SendReadyToBeginMessage();

        if(owner.numPlayers >= owner.minPlayers)
        {
           // SendSceneLoadedMessage();
        }
    }

}
