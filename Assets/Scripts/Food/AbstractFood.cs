using System.Threading.Tasks;
using UnityEngine;

public class AbstractFood : MonoBehaviour
{
    private const int EvoInc = 10;
    private const int HealthInc = 10;
    private const int MoveTime = 200;
    private const float Speed = 6;

    private bool _consumed;
    protected Transform player;

    private void FixedUpdate()
    {
        if (!_consumed)
        {
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
        
        transform.parent.GetComponent<FoodController>().Destroyed();

        player = col.transform;
        Consume(state);
    }

    private async void Consume(IHealth state)
    {
        await Task.Delay(MoveTime);
        state.Health += HealthInc;

        var evo = state as PlayerStateHandler;
        if (evo != null)
        {
            evo.playerEvo.EvoPoints += EvoInc;
        }
        Destroy(gameObject);
    }
}

