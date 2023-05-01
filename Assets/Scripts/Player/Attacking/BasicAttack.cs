using Photon.Pun;
using UnityEngine;

public class BasicAttack : MonoBehaviour, IAttack
{
    private const string Bullet = "Bullet";

    private PlayerEvo _evo;

    public int Cooldown { get; } = 1000;

    private void Awake()
    {
        GetComponent<PlayerMovement>().attackManager.attacks.Add(this);
    }

    public void Attack()
    {
        var trans = transform;
        
        PhotonNetwork.Instantiate(Bullet, trans.position, trans.rotation);
        
    }
    
    public bool Enabled { get => true; }
}
