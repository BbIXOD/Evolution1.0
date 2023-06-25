using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class AbstractFood : MonoBehaviour
{
    private const int EvoInc = 10;
    private const int HealthInc = 10;
    private const int MoveTime = 200;
    private const int LastingTime = 100000;
    private const float Speed = 6;
    

    private bool _consumed;
    private bool _loot;
    protected Transform player;

    private async void Start()
    {

        _loot = transform.parent == null;
        
        if (!_loot)
        {
            return;
        }

        
        await Task.Delay(LastingTime);

        if (this == null)
        {
            return;
        }
        
        Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!_consumed)
        {
            return;
        }

        if (player == null)
        {
            _consumed = false;
            return;
        }
        
        var direction = player.position - transform.position;
        direction = direction.normalized;
        transform.Translate(Time.fixedDeltaTime * Speed * direction);
    }


    private void OnTriggerEnter(Collider col)
    {
        var state = col.gameObject.GetComponent<IHealth>();
        
        if (_consumed || state == null)
        {
            return;
        }

        _consumed = true;

        player = col.transform;
        Consume(state);

        if (_loot)
        {
            return;
        }
        transform.parent.GetComponent<FoodController>().Destroyed();
    }

    private async void Consume(IHealth state)
    {
        await Task.Delay(MoveTime);

        if (this == null)
        {
            return;
        }
        
        state.Health += HealthInc;

        var evo = state as PlayerStateHandler;
        if (evo != null)
        {
            evo.playerEvo.EvoPoints += EvoInc;
        }
        Destroy(gameObject);
    }
}

