using UnityEngine;
using UnityEngine.Events;

public class FloorTrigger : MonoBehaviour
{
    [SerializeField] private float healAmount = 10f;
    public UnityEvent OnFloorTriggered;

    private bool hasBeenSteppedOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenSteppedOn) 
            return;

        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            hasBeenSteppedOn = true;

            PlayerStatsManager statsManager = other.GetComponentInParent<PlayerStatsManager>();
            if (statsManager != null)
            {
                statsManager.Heal(healAmount);
            }

            OnFloorTriggered?.Invoke();
            gameObject.SetActive(false);
        }
    }
}