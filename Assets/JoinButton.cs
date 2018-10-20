using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class JoinButton : MonoBehaviour {

    
    private TMP_Text text;

    private void Awake()
    {
        text = GetComponent<TMP_text>();
    }

    public void Init()
    {

    }
}
