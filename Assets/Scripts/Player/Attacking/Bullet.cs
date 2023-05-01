using UnityEngine;

public class Bullet : MonoBehaviour
{
    private const float Speed = 100;
    private const int Damage = 20;
    private float _distance = 70;

    private void FixedUpdate()
    {
        var move = Speed * Time.fixedDeltaTime * Vector3.forward;
        _distance -= move.z;
        
        transform.Translate(move);

        if (_distance <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<IHealth>();
        if (health != null)
        {
            other.GetComponent<IHealth>().Health -= Damage;
        }
        
        Destroy(gameObject);
    }
}
