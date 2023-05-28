using System;
using System.Threading.Tasks;
using UnityEngine;

public class Despawn : MonoBehaviour
{
    private const int Delay = 1000;
    [NonSerialized]public EnemySpawnController controller;

    [SerializeField] private GameObject loot;
    
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
            return;
        }
        
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        controller.Destroyed();
        Instantiate(loot, transform.position, Quaternion.Euler(0, 0, 0));
    }
}
