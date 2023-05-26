using System.Threading.Tasks;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    private const int Delay = 1000;
    
    private void Start()
    {
        Scan();
    }

    private async void Scan()
    {
        var ray = new Ray(transform.position, Vector3.down);
        var isHit = Physics.Raycast(ray, ChunkManager.MaxHeight);

        if (isHit)
        {
            await Task.Delay(Delay);
            Scan();
        }
        
        Destroy(gameObject);
    }
}
