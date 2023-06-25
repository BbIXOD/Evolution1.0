using System.Linq;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseController : MonoBehaviour
{
    protected int count;
    private int _desired;
    protected int min, max;
    protected int spawnRate;
    protected int upTo = 0;

    private bool _generatorRunning;

    [SerializeField] protected GameObject[] spawnInstances;
    [SerializeField] protected int[] spawnFrequencies;

    private void Start()
    {
        _desired = Random.Range(min, max + 1);
        OnCreatePlacing();
    }

    public void Destroyed()
    {
        count--;
        if (count >= _desired || _generatorRunning)
        {
            return;
        }
        
        _generatorRunning = true;
        Generate();
    }

    private async void Generate()
    {
        
        await Task.Delay(spawnRate);

        if (this == null)
        {
            return;
        }
        
        Place();
        if (count < _desired)
        {
            Generate();
        }
        else
        {
            _generatorRunning = false;
        }
    }

    private void OnCreatePlacing()
    {
        while (count < _desired)
        {
            Place();
        }
    }

    protected virtual GameObject Place()
    {
        var food = Choose();

        var place = RandomPlacer.GetPlace(transform.position, out var success, upTo: upTo);
        

        if (!success)
        {
            return null;
        }

        var rot = Quaternion.Euler(0, 0, 0);
        
        count++;

        return Instantiate(food, place, rot, transform);
    }

    protected GameObject Choose()
    {
        var maxVal = spawnFrequencies.Sum();

        var choose = Random.Range(0, maxVal) + 1;
        var currentVal = 0;
        var index = 0;

        do
        {
            currentVal += spawnFrequencies[index];
            index++;
        }
        while (currentVal < choose);
        
        
        return spawnInstances[--index];
    }
}
