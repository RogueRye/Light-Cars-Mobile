using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour {

    public TMP_InputField matchNameField;
    public LobbyManager lobbyManager;
    public JoinRoom joinRoom;

    private void Start()
    {
        lobbyManager = NetworkManager.singleton as LobbyManager;
    }

    public void OnHost()
    {
        lobbyManager.StartMatchMaker();
        lobbyManager.matchMaker.CreateMatch(matchNameField.text, (uint)lobbyManager.maxPlayers, true, "","", "", 0, 0, lobbyManager.OnMatchCreate);

    }

    public void OnJoin()
    {

        lobbyManager.StartMatchMaker();  
        joinRoom.RefreshList();
    }
}
