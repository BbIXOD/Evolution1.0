using Cinemachine;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class MultiplayerManager : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject canvas;
    private Vector3 _startPosition = new(15, ChunkManager.MaxHeight, 15);
    private const float Delta = 5;
    
    private void Awake()
    {
        var view = GetComponent<PhotonView>();
        
        
        _startPosition.x += Random.Range(-Delta, Delta);
        _startPosition.z += Random.Range(-Delta, Delta);

        player = PhotonNetwork.Instantiate(player.name, _startPosition, Quaternion.Euler(0, 0, 0));

        if (!view.IsMine)
        {
            return;
        }
        
        var cam = player.GetComponentInChildren<CinemachineVirtualCamera>();
        cam.Priority++;

        var targeting = player.AddComponent<Targeting>();

        canvas = Instantiate(canvas);
        targeting.Init(canvas.transform);

    }

    public void LeaveRoom()
    {
        PhotonNetwork.Destroy(player);
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel("LobbyScene");
    }
}
