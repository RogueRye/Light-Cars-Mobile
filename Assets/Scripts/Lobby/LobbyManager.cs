using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class LobbyManager : NetworkLobbyManager {

    public GameObject lobbyMenu;
    public GameObject joinRoom;
    public GameObject canvas;
    [HideInInspector]
    public GameObject myCanvas;

    private void Start()
    {
        if(myCanvas == null)
        {
            myCanvas = Instantiate(canvas);
        }

        joinRoom = myCanvas.transform.Find("JoinRoom").gameObject;
        lobbyMenu = myCanvas.transform.Find("Lobby Parent").Find("Lobby").gameObject;


        if(lobbyMenu != null)
            lobbyMenu.SetActive(false);
      
    }



    public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
    {

        var lobby = lobbyPlayer.GetComponent<M_LobbyPlayer>();

        var player = gamePlayer.GetComponent<PlayerController>();

        player.SetNameAndNumber(lobby.playerName, lobby.playerNumber);
        
        return base.OnLobbyServerSceneLoadedForPlayer(lobbyPlayer, gamePlayer);

    }

    public override void OnClientSceneChanged(NetworkConnection conn)
    {

        if (SceneManager.GetActiveScene().name == playScene)
        {
            myCanvas.SetActive(false);
        }
        else
        {
            myCanvas.SetActive(true);
        }

        base.OnClientSceneChanged(conn);


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
