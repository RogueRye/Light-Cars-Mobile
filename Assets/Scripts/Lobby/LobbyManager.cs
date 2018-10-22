using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : NetworkLobbyManager {

    public GameObject lobbyMenu;

    private void Start()
    {
        lobbyMenu.SetActive(false);
    }

    public override void OnStartHost()
    {
        base.OnStartHost();

        lobbyMenu.SetActive(true);
    }

}
