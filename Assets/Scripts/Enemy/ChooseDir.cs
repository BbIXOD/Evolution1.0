using UnityEngine;

public class ChooseDir
{
    private const float DeltaY = 0.1f;

    public void SimpleMove(Rigidbody movable, GameObject target, float speed)
    {
        
    }
    
    public Vector3 DirYTo(Vector3 pos, GameObject dest)
    {
        const int layerMask = 1;
        
        var dir = dest.transform.position - pos;
        var dist = dir.magnitude;

        Physics.Raycast(pos, dir, out var hit, dist, layerMask, QueryTriggerInteraction.Ignore);

        if (hit.collider.gameObject == dest)
        {
            return dir;
        }
        
        bool isHit;

        do
        {
            dir.y += DeltaY;
            isHit = Physics.Raycast(pos, dir, dist);

        }
        while (isHit);
        
        return dir;
    }
}
