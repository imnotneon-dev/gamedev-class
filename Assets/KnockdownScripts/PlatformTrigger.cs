using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private bool isUnlimited = false;
    [SerializeField] private int maxBalls = 3;
    [SerializeField] private float timeLimit = 10f;
    [SerializeField] private Transform canTower;
    [SerializeField] private MovingPlatform movingPlatform;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[Platform] Player entered: {gameObject.name}");

            if (movingPlatform != null)
            {
                movingPlatform.StartMoving();
            }

            GameManager.Instance?.StartGame(isUnlimited, maxBalls, timeLimit, this, canTower);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[Platform] Player exited: {gameObject.name}");

            if (movingPlatform != null)
            {
                movingPlatform.StopAndReset();
            }

            GameManager.Instance?.ResetGameFromPlatform(this);
        }
    }
}