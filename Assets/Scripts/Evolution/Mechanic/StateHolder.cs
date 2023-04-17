public class StateHolder
{
    private float _value;
    private IBodyPart[] _subscribers;

    public StateHolder(IBodyPart[] subscribers)
    {
        _subscribers = subscribers;
    }
}
