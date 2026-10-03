using UnityEngine;

public class PlayerRespawnController : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;

    private CharacterController controller;
    private PlayerStatsManager statsManager;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        statsManager = GetComponent<PlayerStatsManager>();
    }

    public void RespawnPlayer()
    {
        if (statsManager != null)
        {
            statsManager.ResetStats();
        }

        if (spawnPoint != null)
        {
            if (controller != null)
            {
                controller.enabled = false;
            }

            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;

            if (controller != null)
            {
                controller.enabled = true;
            }
        }
    }
}