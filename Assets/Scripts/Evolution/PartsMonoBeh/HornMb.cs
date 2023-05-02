using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class HornMb : MonoBehaviour
{
    private GameObject _target;
    private IHealth _health;

    private const int Cooldown = 100;
    private const int Damage = 15;
    
    private readonly CancellationTokenSource _source = new CancellationTokenSource();
    private CancellationToken _token;

    private void Awake()
    {
        _token = _source.Token;
    }

    private void OnCollisionEnter(Collision collision)
    {
        var health = collision.gameObject.GetComponent<IHealth>();

        if (health == null || _target != null)
        {
            return;
        }
        _health = health;
        _target = collision.gameObject;
        DoDamage();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject != _target)
        {
            return;
        }
        _target = null;
        _health = null;
        
        _source.Cancel();
    }

    private async void DoDamage()
    {
        _health.Health -= Damage;
        await Task.Delay(Cooldown, _token);
        DoDamage();
    }
}
