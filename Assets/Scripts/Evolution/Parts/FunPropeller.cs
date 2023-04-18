
public class FunPropeller : AbstractBodyPart
{
    
    public FunPropeller()
    {
        add = null;
        remove = null;
        destroy = null;
        Part = null;
    }

    public new void UpdateValue()
    {
        NeedValue += Getter.Moving();
    }
}
