using System.Linq;
using System.Threading.Tasks;
using Photon.Pun;
using UnityEngine;
using Random = UnityEngine.Random;

public class FoodController : MonoBehaviour
{
    private int _foodCount;
    private int _desired;
    private const int Delta = 10;
    private const int FoodDelay = 5000;
    private const float MaxHeightMult = 1.5f;

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
        var position = transform.position;
        var locX = (int)position.x;
        var locZ = (int)position.z;
        
        var key = (locX / (int)ChunkManager.Size.x, locZ / (int)ChunkManager.Size.z);
        
        if (!ChunkManager.Loaded.ContainsKey(key))
        {
            return;
        }
        
        var place = Vector3.zero;
        
        place.x = Random.Range(locX, locX + ChunkManager.Size.x);
        place.z = Random.Range(locZ, locZ + ChunkManager.Size.z);
        
        var minY = ChunkManager.Loaded[key].GetChunkHeight(place);
        var maxY = ChunkManager.Size.y * MaxHeightMult;
        
        place.y = Random.Range(minY, maxY);
        
        var rot = Quaternion.Euler(0, 0, 0);
        
        var food = ChooseFood();

        var instantiated = PhotonNetwork.Instantiate(food.name, place, rot);
        instantiated.transform.parent = transform;

        _foodCount++;
    }

    private GameObject ChooseFood()
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

        return currentFood;
    }

}
