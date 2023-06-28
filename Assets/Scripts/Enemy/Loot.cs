using Extensions = MyExtensions;
using UnityEngine;
using Random = UnityEngine.Random;

public class Loot : MonoBehaviour
{
    [SerializeField] private GameObject[] loot;
    [SerializeField] private int[] frequency;
    [SerializeField] private int[] amount;

    private const float SpawnPosDelta = 1;

    private bool _onQuit;
    
    private Collider _myCollider;

    private void Awake()
    {
        _myCollider = GetComponent<Collider>();
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
        
        var count = Extensions.Choose(amount);
        var item = loot[Extensions.Choose(frequency)];
        
        var spawnPos = transform.position;
        const int vectorLength = 3;
        
        for (var i = 0; i < count; i++)
        {
            for (var j = 0; j < vectorLength; j++)
            {
                spawnPos[j] += Random.Range(-SpawnPosDelta, SpawnPosDelta);
            }

            var curLoot = Instantiate(item, spawnPos, Quaternion.Euler(0,0,0));
            
            Physics.IgnoreCollision(_myCollider, curLoot.GetComponent<Collider>());
        }
    }
}
