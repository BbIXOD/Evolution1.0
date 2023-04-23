using System;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player;
    private Vector3 _startPosition = new Vector3(15, 16, 15);
    private const float Delta = 5;
    
    private void Awake()
    {
        _startPosition.x += Random.Range(-Delta, Delta);
        _startPosition.z += Random.Range(-Delta, Delta);

        PhotonNetwork.Instantiate(player.name, _startPosition, Quaternion.Euler(0, 0, 0));
    }

    public void LeaveRoom()
    {
        
    }
}
