using UnityEngine;

public static class ChooseDir
{
    private const float DeltaY = 0.1f;
    private const float DeltaMagnitude = 0.3f;

    public static void SimpleMoveTo(Rigidbody movable, Vector3 target, float speed, float rotTime)
    {

        //todo: make escaping smarter
        
        var qBestRot = Quaternion.LookRotation(target);

        movable.rotation = Quaternion.Lerp(movable.rotation, qBestRot, rotTime);

        movable.velocity = speed * Time.fixedDeltaTime * movable.transform.forward;
    }

    public static void SimpleMove(Rigidbody movable, GameObject target, float speed, float rotTime)
    {
        var pos = movable.position;
        var dist = (target.transform.position - pos).magnitude;

        if (dist < DeltaMagnitude)
        {
            return;
        }
        
        var bestRot = DirYTo(pos, target);
        var qBestRot = Quaternion.LookRotation(bestRot);

        movable.rotation = Quaternion.Lerp(movable.rotation, qBestRot, rotTime);

        movable.velocity = speed * Time.fixedDeltaTime * movable.transform.forward;
    }
    
    private static Vector3 DirYTo(Vector3 pos, GameObject dest)
    {
        const int layerMask = 1;
        
        var dir = dest.transform.position - pos;
        var dist = dir.magnitude;

        var isHit = Physics.Raycast(pos, dir, out var hit, dist, layerMask, QueryTriggerInteraction.Ignore);

        if (!isHit || hit.collider.gameObject == dest)
        {
            return dir;
        }

        do
        {
            dir.y += DeltaY;
            isHit = Physics.Raycast(pos, dir, out hit, dist);

        }
        while (isHit && hit.collider.gameObject != dest);
        
        return dir;
    }
}
