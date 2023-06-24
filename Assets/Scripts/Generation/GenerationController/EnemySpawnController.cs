using UnityEngine;

public class EnemySpawnController : BaseController
{
    private void Awake()
    {
        min = 0;
        max = 1;
        spawnRate = 7500;
        upTo = 1;
    }

    protected override GameObject Place()
    {
        var enemy = base.Place();

        if (enemy == null)
        {
            return null;
        }
        
        enemy.GetComponent<SpawnManager>().controller = this;
        
        return enemy;
    }
}
