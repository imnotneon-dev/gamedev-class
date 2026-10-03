using System.Collections;
using UnityEngine;

public class SpeedBoostTrigger : MonoBehaviour
{
    [SerializeField] private float boostMultiplier = 1.8f;
    [SerializeField] private float duration = 4f;
    private bool hasBeenSteppedOn = false;

    private void OnTriggerEnter(Collider other)
    {
        if (hasBeenSteppedOn)
            return;

        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            hasBeenSteppedOn = true;

            if (TryGetComponent<Collider>(out var col))
                col.enabled = false;
            if (TryGetComponent<MeshRenderer>(out var rend))
                rend.enabled = false;

            var playerObject = other.transform.root.gameObject;
            StartCoroutine(ApplySpeedBoost(playerObject));
        }
    }

    private IEnumerator ApplySpeedBoost(GameObject player)
    {
        var controller = player.GetComponent<StarterAssets.ThirdPersonController>();

        if (controller != null)
        {
            controller.MoveSpeed *= boostMultiplier;
            controller.SprintSpeed *= boostMultiplier;

            yield return new WaitForSeconds(duration);

            controller.MoveSpeed /= boostMultiplier;
            controller.SprintSpeed /= boostMultiplier;
        }

        Destroy(gameObject);
    }
}