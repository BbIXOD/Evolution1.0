using Photon.Pun;
using UnityEngine;

public class BasicAttackScript : MonoBehaviour, IAttack
{
    private const string Bullet = "Bullet";

    private PlayerEvo _evo;

    public int Cooldown { get; } = 1000;

    private void Start()
    {
        transform.parent.gameObject.GetComponent<PlayerMovement>().attackManager.attacks.Add(this);
    }

    public void Attack()
    {
        var trans = transform;
        
        PhotonNetwork.Instantiate(Bullet, trans.position, trans.rotation);
        
    }
    
    public bool Enabled { get => true; }
}
