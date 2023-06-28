using Photon.Pun;
using TMPro;
using UnityEngine;

public class Nickname : MonoBehaviourPunCallbacks
{
    private PhotonView _view;
    private TextMeshProUGUI _tmp;

    private Transform _whereToLook;
    private void Start()
    {
        _whereToLook = Camera.main!.transform;
        _view = GetComponent<PhotonView>();
        _tmp = GetComponent<TextMeshProUGUI>();
        _tmp.text = _view.Owner.NickName;
    }

    private void Update()
    {
        transform.LookAt(_whereToLook);
    }
}