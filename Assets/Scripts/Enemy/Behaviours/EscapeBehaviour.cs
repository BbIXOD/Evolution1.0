using System;
using System.Linq;
using UnityEngine;

public class EscapeBehaviour: BaseBehaviour
{
    protected Transform playerContainer;
    
    public override void Awake()
    {
        base.Awake();
        
        searchRadius = 15;
        searchTag = "Player";
        searchedTrigger = QueryTriggerInteraction.Ignore;
    }

    public override void DoBeh()
    {
        if (playerContainer == null)
        {
            Search();
            playerContainer = target.transform;
        }

        ChooseDir.SimpleMoveTo(rb, transform.position - playerContainer.position, data.speed, data.rotTime);
    }

    public override int Condition()
    {
        Physics.OverlapSphereNonAlloc(rb.position, searchRadius, buffer);

        return Convert.ToInt16(buffer.Any(col => col != null && col.CompareTag(searchTag))) * 2;
    }
}
