using UnityEngine;
using UnityEngine.Networking.Match;
using UnityEngine.Networking;
using TMPro;
public class JoinButton : MonoBehaviour {


    private TMP_Text btnText;
    private MatchInfoSnapshot m_Match; 

    private void Awake()
    {
        btnText = GetComponentInChildren<TMP_Text>();
    }

    public void Init(MatchInfoSnapshot match, Transform parent)
    {
        btnText.text = match.name;
        transform.SetParent(parent);
        transform.localScale = Vector3.one;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
    }


    public void JoinMatch()
    {
        //FindObjectOfType<MyNetworkManager>().JoinMatch(m_Match);
    }

}
