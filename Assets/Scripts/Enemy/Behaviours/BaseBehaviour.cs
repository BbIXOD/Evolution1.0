using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour, IBehaviour
{
    protected Rigidbody rb;
    protected readonly EnemyData data = new();

    protected float searchRadius;
    private const int BufferSize = 100;
    protected string searchTag;
    protected QueryTriggerInteraction searchedTrigger;

    protected readonly Collider[] buffer = new Collider[BufferSize];
    protected GameObject target;

    public virtual void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public virtual void Enter()
    {
        Search();
    }

    public virtual void DoBeh()
    {
        if (target == null)
        {
            Search();
            
            if (target == null)
            {
                return;
            }
        }

        ChooseDir.SimpleMove(rb, target, data.speed, data.rotTime);
    }

    public virtual void Exit() { }

    public virtual int Condition()
    {
        return 0; 
    }

    protected void Search()
    {
        for (var i = 0; i < buffer.Length; i++)
        {
            buffer[i] = null;
        }
        
        var count = Physics.OverlapSphereNonAlloc(rb.position, searchRadius, buffer, 1, searchedTrigger);
        var dist = Mathf.Infinity;

        for (var i = 0; i < count; i++)
        {
            var col = buffer[i];
            
            if (!col.gameObject.CompareTag(searchTag))
            {
                continue;
            }

            var magnitude = (col.transform.position - rb.position).magnitude;

            if (magnitude >= dist)
            {
                continue;
            }

            dist = magnitude;
            target = col.gameObject;
        }
    }
}
