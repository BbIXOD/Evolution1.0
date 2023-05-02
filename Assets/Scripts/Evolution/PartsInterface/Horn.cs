using System;

public class Horn : AbstractBodyPart
{
    private const float Multiplier = 100;
    
    public Horn()
    {
        add = Array.Empty<PartsEnum>();
        remove = Array.Empty<PartsEnum>();
        destroy = Array.Empty<PartsEnum>();
        Part = ToString();
        Index = PartsEnum.FunPropeller;
    }

    public override void UpdateValue() { }

    public void AddPoints()
    {
        NeedValue += Multiplier;
    }
}
