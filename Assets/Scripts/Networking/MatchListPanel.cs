using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Match;
public class MatchListPanel : MonoBehaviour {

    [SerializeField]
    private JoinButton joinBtnPrefab;


    private void Awake()
    {
        AvailableMatchesList.OnAvailableMatchesChanged += AvailableMatchesList_OnAvailableMatchesChanged;
    }

    private void AvailableMatchesList_OnAvailableMatchesChanged(List<MatchInfoSnapshot> matches)
    {
        ClearExistingButtons();
        CreateNewButtons(matches);
    }

    private void CreateNewButtons(List<MatchInfoSnapshot> matches)
    {
        foreach(var match in matches)
        {
            var button = Instantiate(joinBtnPrefab);
            button.Init(match, transform);
        }
    }

    private void ClearExistingButtons()
    {
        var buttons = GetComponentsInChildren<JoinButton>();
        foreach(var button in buttons)
        {
            Destroy(button.gameObject);
        }
    }



}
