using System;

public class BasicAttack : AbstractBodyPart
{
    public BasicAttack()
    {
        add = Array.Empty<PartsEnum>();
        remove = Array.Empty<PartsEnum>();
        destroy = Array.Empty<PartsEnum>();
        Part = ToString();
        Index = PartsEnum.BasicAttack;
    }
}
