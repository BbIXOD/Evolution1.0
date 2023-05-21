
using UnityEngine;

public class PeaceBehaviour : BaseBehaviour
{
    public override void Awake()
    {
        base.Awake();
        
        searchRadius = 50;
        searchTag = "Food";
        searchedTrigger = QueryTriggerInteraction.Collide;
    }

    public override int Condition()
    {
        return 1;
    }
}
