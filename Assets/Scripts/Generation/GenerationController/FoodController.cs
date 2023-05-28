using UnityEngine;

public class FoodController : BaseController
{
    private const int Delta = 10;
    private const string Heavy = "Grass";
    
    private void Awake()
    {
        min = ChunkManager.FoodCount - Delta;
        max = ChunkManager.FoodCount - Delta;
        spawnRate = 5000;
    }

    protected override GameObject Place()
    {
        var food = Choose();

        var place = RandomPlacer.GetPlace(transform.position, out var success, upTo: upTo,
            heavy: food.name == Heavy);
        

        if (!success)
        {
            return null;
        }

        var rot = Quaternion.Euler(0, 0, 0);
        
        count++;

        return Instantiate(food, place, rot, transform);
    }
}
