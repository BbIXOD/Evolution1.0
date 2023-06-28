using UnityEngine;
using static MyExtensions;


public class StateMachine : MonoBehaviour
{
    private IBehaviour[] _behaviors;
    private IBehaviour _current;

    private void Start()
    {
        _behaviors = GetComponents<IBehaviour>();

        _current = _behaviors.FindBest(beh => beh.Condition());
    }

    private void FixedUpdate()
    {
        var best = _behaviors.FindBest(beh => beh.Condition());

        if (best != _current)
        {
            _current.Exit();
            best.Enter();
            _current = best;
        }
        
        _current.DoBeh();
    }
}
