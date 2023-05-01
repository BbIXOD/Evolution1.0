using UnityEngine;

public class Water : MonoBehaviour
{
    private const float Scale = 10f;
    
    private void OnTriggerStay(Collider collider)
    {
        var pos = collider.transform.position;
        
        if (pos.y <= transform.position.y)
        {
            return;
        }
        
        pos.y = Mathf.Lerp(pos.y, transform.position.y, Scale);
        collider.transform.position = pos;
    }
}
