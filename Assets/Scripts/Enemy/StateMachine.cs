using UnityEngine;
using static MyExtensions.MyExtensions;


public class StateMachine : MonoBehaviour
{
    [SerializeField] private Object[] behaviors;
    private IBehaviour[] _behaviors;
    private IBehaviour _current;

    private void Awake()
    {
        _behaviors = (IBehaviour[])behaviors;
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
