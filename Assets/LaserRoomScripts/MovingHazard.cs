using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingHazard : MonoBehaviour
{
    [SerializeField] private Vector3 moveOffset = new Vector3(0f, 0f, 6f);
    [SerializeField] private float speed = 2f;

    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstacleLayer;

    [SerializeField] private float damageAmount = 20f;
    [SerializeField] private float damageInterval = 1f;

    private Vector3 startPosition;
    private float nextDamageTime = 0f;

    private void Start()
    {
        startPosition = transform.position;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
        }
    }

    private void Update()
    {
        float pingPong = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = startPosition + (moveOffset * pingPong);

        DetectPlayer();
    }

    private void DetectPlayer()
    {
        bool isPlayerInRange = Physics.CheckSphere(transform.position, detectionRadius, playerLayer);

        if (isPlayerInRange)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
            if (hits.Length > 0)
            {
                Transform playerTransform = hits[0].transform;

                Vector3 origin = transform.position + Vector3.up * 0.5f;
                Vector3 target = playerTransform.position + Vector3.up * 1.0f;

                Vector3 directionToPlayer = (target - origin).normalized;
                float distanceToPlayer = Vector3.Distance(origin, target);

                if (!Physics.Raycast(origin, directionToPlayer, out RaycastHit hitInfo, distanceToPlayer, obstacleLayer))
                {

                    if (Time.time >= nextDamageTime)
                    {
                        nextDamageTime = Time.time + damageInterval;

                        PlayerStatsManager stats = playerTransform.GetComponentInParent<PlayerStatsManager>();
                        if (stats != null)
                        {
                            stats.TakeDamage(damageAmount);
                            Debug.Log($"<color=red>[HAZARD ALERT]</color> Hit player! Dealt {damageAmount} damage.");
                        }
                        else
                        {
                            Debug.LogWarning("[HAZARD] Player detected, but no PlayerStatsManager found on player object!");
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}