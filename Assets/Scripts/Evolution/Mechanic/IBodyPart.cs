using UnityEngine;

public interface IBodyPart
{
    public IBodyPart[] Add { get; }
    public IBodyPart[] Remove { get; }
    public GameObject Part { get; }

    public float NeedValue { get; }

    public void UpdateValue(float condValue);
}
