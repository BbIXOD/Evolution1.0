using System.Collections.Generic;
using UnityEngine;

public interface IBodyPart
{
    public IEnumerable<IBodyPart> Add { get; }
    public IEnumerable<IBodyPart> Remove { get; }
    public IEnumerable<IBodyPart> Destroy { get; }
    public GameObject Part { get; }
    public PlayerStateGetter Getter { set; }

    public float NeedValue { get; }

    public void UpdateValue();
}
