using System.Threading.Tasks;
using UnityEngine;

public class HealthRegenMb : MonoBehaviour
{
    [SerializeField] private int regenPerTick;
    [SerializeField] private int tickLength;

    private IHealth _health;

    private void Start()
    {
        _health = GetComponentInParent<IHealth>();
        Regenerate();
    }

    private async void Regenerate()
    {
        if (_health == null)
        {
            return;
        }
        
        _health.Health += regenPerTick;
        await Task.Delay(tickLength);

        Regenerate();
    } 
}
