using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;

public class JoinRoom : MonoBehaviour {

    public GameObject hostPrefab;

    LobbyManager lobbyManager;

	// Use this for initialization
	void Start () {
        lobbyManager = LobbyManager.singleton as LobbyManager;
        gameObject.SetActive(false);
	}
	
	
    public void RefreshList()
    {
        Debug.Log("Refreshing");
        for (int i = 0; i < transform.GetChild(0).childCount; i++)
        {
            Destroy(transform.GetChild(0).GetChild(i).gameObject);
        }

        if (lobbyManager == null)
        {
            lobbyManager = LobbyManager.singleton as LobbyManager; 
        }

        if (lobbyManager.matchMaker == null)
        {
            lobbyManager.StartMatchMaker();
        }

        lobbyManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
    }

    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {
        if (!success)
        {
            print("Please refresh");
            //we are going to refresh the list
        }



        foreach (var match in matchList)
        {
            GameObject listGO = Instantiate(hostPrefab);
            listGO.transform.SetParent(transform.GetChild(0));
            var hostSetup = listGO.GetComponent<HostSetup>();
            hostSetup.Init(match);
        }

    }
}
