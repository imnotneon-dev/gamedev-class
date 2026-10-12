using UnityEngine;

public class KnockdownZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Can can = other.GetComponent<Can>();
        if (can != null && !can.IsKnockedDown)
        {
            can.MarkAsKnocked();
            GameManager.Instance?.CheckCanStatuses();
        }
    }
}