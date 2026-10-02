using UnityEngine;
using UnityEngine.Events;

public class LaserMovementController : MonoBehaviour
{
    public UnityEvent OnLaserHit;

    [SerializeField] private float speed = 5f;
    [SerializeField] private float damage = 20f;

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerStatsManager>(out var playerStats))
            {
                playerStats.TakeDamage(damage);
            }

            OnLaserHit?.Invoke(); 
            Destroy(gameObject);
        }
    }
}