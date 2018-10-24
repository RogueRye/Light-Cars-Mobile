﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {

    public TMP_InputField matchNameField;
    public LobbyManager lobbyManager;
    public JoinRoom joinRoom;

    public void OnHost()
    {
        lobbyManager.StartMatchMaker();
        lobbyManager.matchMaker.CreateMatch(matchNameField.text, (uint)lobbyManager.maxPlayers, true, "","", "", 0, 0, lobbyManager.OnMatchCreate);

    }

    public void OnJoin()
    {
        lobbyManager.StartMatchMaker();
        joinRoom.gameObject.SetActive(true);
        joinRoom.RefreshList();
    }
}
