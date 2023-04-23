using System;
using Photon.Pun;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private TMP_InputField
        nickname,
        id;
        
    
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
            nameString = Convert.ToString(Random.Range(0, 10));
        }
        PhotonNetwork.NickName = nameString;
        
        PhotonNetwork.LoadLevel("Game");
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom(id.text);
    }

    public void JoinRoom()
    {
        PhotonNetwork.JoinRoom(id.text);
    }
}
