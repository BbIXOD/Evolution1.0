using System.Threading.Tasks;
using UnityEngine;

public class AbstractFood : MonoBehaviour
{
    private readonly int _evoInc = 10;
    private readonly int _healthInc = 10;
    private const int MoveTime = 200;
    private const float Speed = 2;

    private bool _consumed;
    private Transform _player;

    private void FixedUpdate()
    {
        if (_consumed)
        {
            var direction = _player.position - transform.position;
            transform.Translate(Time.fixedDeltaTime * Speed * direction);
        }
    }


    private void OnTriggerEnter(Collider col)
    {
        var state = col.gameObject.GetComponent<PlayerStateHandler>();
        if (_consumed || state == null)
        {
            return;
        }

        _consumed = true;
        
        transform.parent.GetComponent<FoodController>().EatenFood();

        _player = col.transform;
        Consume(state);
    }

    private async void Consume(PlayerStateHandler state)
    {
        await Task.Delay(MoveTime);
        state.playerEvo.EvoPoints += _evoInc;
        state.playerHealth.Health += _healthInc;
        Destroy(gameObject);
    }
}

