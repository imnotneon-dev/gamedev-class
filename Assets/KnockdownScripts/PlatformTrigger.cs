using UnityEngine;

public class PlatformTrigger : MonoBehaviour
{
    [SerializeField] private bool isUnlimited = false;
    [SerializeField] private int maxBalls = 3;
    [SerializeField] private float timeLimit = 10f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[Platform] Player entered: {gameObject.name}");
            GameManager.Instance.StartGame(isUnlimited, maxBalls, timeLimit, this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log($"[Platform] Player exited: {gameObject.name}");
            GameManager.Instance?.ResetGameFromPlatform(this);
        }
    }
}