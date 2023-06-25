using System;
using System.Threading.Tasks;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private const int Delay = 1000;
    [NonSerialized]public EnemySpawnController controller;

    [SerializeField] private GameObject loot;
    
    private bool _onQuit;
    private bool _dead;
    
    private void Start()
    {
        GetComponent<Rigidbody>().angularDrag = Mathf.Infinity;
        Scan();
    }

    private async void Scan()
    {
        if (_dead)
        {
            return;
        }
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

    private void OnApplicationQuit()
    {
        _onQuit = true;
    }

    private void OnDestroy()
    {
        if (_onQuit)
        {
            return;
        }
        
        _dead = true;
        
        Instantiate(loot, transform.position, Quaternion.Euler(0, 0, 0));

        if (controller == null)
        {
            return;
        }
        
        controller.Destroyed();
    }
}
