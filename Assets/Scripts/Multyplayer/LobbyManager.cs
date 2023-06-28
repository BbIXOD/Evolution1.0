using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField
        nickname,
        id;
    [SerializeField] private Image loadingScreen;
        
    
    private void Awake()
    {
        PhotonNetwork.GameVersion = "1.0";
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnJoinedRoom()
    {
        var nameString = nickname.text;
        nameString = nameString.Trim();

        if (nameString == "")
        {
            nameString = "Player";
        }
        PhotonNetwork.NickName = nameString;
        
        PhotonNetwork.LoadLevel("Game");
    }

    public override void OnConnectedToMaster()
    {
        Destroy(loadingScreen);
    }
    

    public void JoinRoom()
    {
        PhotonNetwork.JoinOrCreateRoom(id.text, null, null);
    }
}
