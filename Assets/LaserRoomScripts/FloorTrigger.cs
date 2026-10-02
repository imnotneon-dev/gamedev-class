using UnityEngine;
using UnityEngine.Events;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] private float healAmount = 10f;
    public UnityEvent OnFloorTriggered;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.TryGetComponent<PlayerStatsManager>(out var statsManager))
            {
                statsManager.Heal(healAmount);
                Debug.Log("Healed 10 HP!");
            }

            OnFloorTriggered?.Invoke();

            gameObject.SetActive(false);
        }
    }
}