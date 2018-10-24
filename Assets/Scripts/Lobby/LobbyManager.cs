using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LobbyManager : NetworkLobbyManager {

    public GameObject lobbyMenu;
    public GameObject joinRoom;
    public GameObject canvas;



    private void Start()
    {
        if(lobbyMenu != null)
            lobbyMenu.SetActive(false);
      
    }

    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {
        canvas.SetActive(false);

        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);

    }

    public override void OnStartHost()
    {
        base.OnStartHost();
        if (lobbyMenu != null)
        {
            lobbyMenu.transform.parent.gameObject.SetActive(true);
            lobbyMenu.SetActive(true);
        }


    }




}
