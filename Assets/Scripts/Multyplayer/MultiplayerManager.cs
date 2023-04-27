using Cinemachine;
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

        player = PhotonNetwork.Instantiate(player.name, _startPosition, Quaternion.Euler(0, 0, 0));
        var cam = player.GetComponentInChildren<CinemachineVirtualCamera>();
        cam.Priority++;
    }

    public void LeaveRoom()
    {
        Debug.LogWarning("LeaveRoom");
        PhotonNetwork.Destroy(player);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("LobbyScene");
    }
}
