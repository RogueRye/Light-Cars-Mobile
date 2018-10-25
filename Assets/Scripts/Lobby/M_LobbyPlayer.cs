using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class M_LobbyPlayer : NetworkLobbyPlayer
{

    [SyncVar]
    public string playerName;
    [SyncVar]
    public int playerNumber = -1;

    public Button joinButton;
    public TMP_Text myText;
    public TMP_Text btnText;
    [HideInInspector]
    public LobbyManager owner;


    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();
        //get parent somehow


        owner = NetworkManager.singleton as LobbyManager;


        if (connectionToClient != null)
            playerNumber = connectionToClient.connectionId + 1;

        playerName = string.Format("Player {0}", playerNumber);
        myText.text = playerName;

        if (owner.lobbyMenu != null)
            gameObject.transform.SetParent(owner.lobbyMenu.transform.GetChild(0));

        ForeignInit();

    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Init();
    }

    private void Init()
    {

        joinButton.enabled = true;
        btnText.text = "JOIN";
       
    }

    private void ForeignInit()
    {
        btnText.text = "...";
        joinButton.enabled = false;
       // myText.text = playerName;
    }


    public void JoinMatch()
    {
        SendReadyToBeginMessage();
    }

}
