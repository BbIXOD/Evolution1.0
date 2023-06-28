using System;
using UnityEngine;

public sealed class FunPropeller : AbstractBodyPart
{
    
    public FunPropeller()
    {
        add = Array.Empty<PartsEnum>();
        remove = Array.Empty<PartsEnum>();
        destroy = Array.Empty<PartsEnum>();
        Part = ToString();
        Index = PartsEnum.FunPropeller;
    }

    public override void UpdateValue()
    {
        NeedValue += Getter.Moving() * Time.fixedDeltaTime;
    }
}
