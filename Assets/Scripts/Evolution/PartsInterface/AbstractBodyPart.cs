using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractBodyPart : IBodyPart
{
    protected PartsEnum[] add;
    protected PartsEnum[] remove;
    protected PartsEnum[] destroy;
    
    public IEnumerable<PartsEnum> Add { get => add; }
    public IEnumerable<PartsEnum> Remove { get => remove; }
    public IEnumerable<PartsEnum> Destroy { get => destroy; }
    public string Part { get; protected set; }
    public float NeedValue { get; set; }
    public PartsEnum Index { get; protected set; }
    public bool Updating { get; set; }
    public bool Active { get; set; }
    public PlayerStateGetter Getter { protected get; set; }

    public virtual void UpdateValue()
    {
        NeedValue += Time.fixedDeltaTime;
    }

    public void ClearValue()
    {
        NeedValue = 0;
    }
}
