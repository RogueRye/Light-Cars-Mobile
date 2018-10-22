﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
public class M_LobbyPlayer : NetworkLobbyPlayer {


    public Button joinButton;
    public TMP_Text myText;
    public TMP_Text btnText;


    public override void OnClientEnterLobby()
    {
        base.OnClientEnterLobby();
        //get parent somehow
       
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Init();
    }


    private void Init()
    {
        if (isLocalPlayer)
        {           
            myText.text = "MyPlayer";
            joinButton.enabled = true;
            btnText.text = "JOIN";
        }
        else
        {
            myText.text = "OtherPlayer";
            joinButton.enabled = false;
            btnText.text = "...";
        }
    }



}
