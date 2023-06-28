using System;
using UnityEngine;

public sealed class BasicRegen : AbstractBodyPart
{
    private const float Multiplier = 100;
    
    public BasicRegen()
    {
        add = Array.Empty<PartsEnum>();
        remove = Array.Empty<PartsEnum>();
        destroy = Array.Empty<PartsEnum>();
        Part = ToString();
        Index = PartsEnum.BasicRegen;
    }

    public override void UpdateValue()
    {
        NeedValue += Getter.HowHurt() * Time.fixedDeltaTime;
    }
}
