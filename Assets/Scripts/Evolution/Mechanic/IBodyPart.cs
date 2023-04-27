using System.Collections.Generic;
using UnityEngine;

public interface IBodyPart
{
    public IEnumerable<PartsEnum> Add { get; }
    public IEnumerable<PartsEnum> Remove { get; }
    public IEnumerable<PartsEnum> Destroy { get; }
    public string Part { get; }
    public PlayerStateGetter Getter { set; }

    public float NeedValue { get; set; }
    public PartsEnum Index { get; }
    public bool Updating { get; set; }
    public bool Active { get; set; }

    public void UpdateValue();
    public void ClearValue();
}
