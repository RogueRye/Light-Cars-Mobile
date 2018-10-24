using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;
using TMPro;

public class HostSetup : MonoBehaviour {

    LobbyManager lobbyManager;
    TMP_Text hostName;
    MatchInfoSnapshot match;
    GameObject lobbyParent;

	// Use this for initialization
	void Start () {
        lobbyManager = NetworkManager.singleton as LobbyManager;

        lobbyParent = lobbyManager.lobbyMenu.transform.parent.gameObject;
        Debug.Log(lobbyParent);
	}

    public void Init(MatchInfoSnapshot _match)
    {
        match = _match;
        hostName = transform.GetChild(0).GetComponent<TMP_Text>();
        if (hostName != null)
            hostName.text = match.name;

    }


    public void Join()
    {
        if(lobbyManager == null)
        {
            lobbyManager = NetworkManager.singleton as LobbyManager;
        }

        var go = lobbyParent.GetComponentsInChildren<Transform>(true);
        foreach (var item in go)
        {
            item.gameObject.SetActive(true);
        }

        lobbyManager.joinRoom.gameObject.SetActive(false);

        lobbyManager.matchMaker.JoinMatch(match.networkId, "", "", "", 0, 0, lobbyManager.OnMatchJoined);

    }
}
