using UnityEngine;

public class FunPropellerMb : MonoBehaviour
{
    private void Start()
    {
        transform.parent.GetComponent<PlayerMovement>().speed *= 2;
    }
}
