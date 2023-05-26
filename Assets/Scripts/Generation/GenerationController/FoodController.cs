using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodController : MonoBehaviour
{
    private int _foodCount;
    private int _desired;
    private const int Delta = 10;
    private const int FoodDelay = 5000;

    private const int HeavyFood = 3;

    private bool _generatorRunning;

    [SerializeField] private GameObject[] foodInstances;
    [SerializeField] private int[] foodFrequencies;


    private void Awake()
    {
        _desired = ChunkManager.FoodCount + Random.Range(-Delta, Delta);
        OnCreateFoodPlacing();
    }

    public void EatenFood()
    {
        _foodCount--;
        if (_generatorRunning)
        {
            return;
        }
        
        _generatorRunning = true;
        GenerateFood();
    }

    private async void GenerateFood()
    {
        await Task.Delay(FoodDelay);
        PlaceFood();
        if (_foodCount < _desired)
        {
            GenerateFood();
        }
        else
        {
            _generatorRunning = false;
        }
    }

    private void OnCreateFoodPlacing()
    {
        while (_foodCount < _desired)
        {
            PlaceFood();
        }
    }

    private void PlaceFood()
    {
        var place = RandomPlacer.GetPlace(transform.position, out var success, out var minY);

        if (!success)
        {
            return;
        }
        
        var food = ChooseFood(out var heavy);
        
        place.y = heavy ? minY : Random.Range(minY, ChunkManager.MaxHeight);
        
        var rot = Quaternion.Euler(0, 0, 0);

        Instantiate(food, place, rot, transform);

        _foodCount++;
    }

    private GameObject ChooseFood(out bool heavy)
    {
        var maxVal = foodFrequencies.Sum();

        var choose = Random.Range(0, maxVal) + 1;
        var currentVal = 0;
        var index = 0;
        GameObject currentFood;

        do
        {
            currentFood = foodInstances[index];
            currentVal += foodFrequencies[index];
            index++;
        }
        while (currentVal < choose);

        heavy = index == HeavyFood;
        return currentFood;
    }

}
