using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBodyPart : IBodyPart
{
    protected IBodyPart[] add;
    protected IBodyPart[] remove;
    protected IBodyPart[] destroy;
    
    public IEnumerable<IBodyPart> Add { get => add; }
    public IEnumerable<IBodyPart> Remove { get => remove; }
    public IEnumerable<IBodyPart> Destroy { get => destroy; }
    public GameObject Part { get; protected set; }
    public float NeedValue { get; protected set; }
    public PlayerStateGetter Getter { protected get; set; }


    public void UpdateValue()
    {
        NeedValue += Time.fixedDeltaTime;
    }
}
