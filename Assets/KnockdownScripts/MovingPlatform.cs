using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float zDistance = 3f;
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float startDelay = 1.5f;

    private Vector3 startPos;
    private Rigidbody rb;
    private bool isMoving = false;
    private float moveTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        startPos = transform.position;
    }

    private void FixedUpdate()
    {
        if (!isMoving)
            return;

        moveTimer += Time.fixedDeltaTime * speed;

        float zOffset = Mathf.Sin(moveTimer) * zDistance;
        Vector3 targetPos = startPos + new Vector3(0f, 0f, zOffset);

        rb.MovePosition(targetPos);
    }

    public void StartMoving()
    {
        StopAllCoroutines();
        StartCoroutine(StartMovementDelay());
    }

    private IEnumerator StartMovementDelay()
    {
        isMoving = false;

        yield return new WaitForSeconds(startDelay);

        isMoving = true;
    }

    public void StopAndReset()
    {
        StopAllCoroutines();
        isMoving = false;
        moveTimer = 0f;

        if (rb != null)
        {
            rb.MovePosition(startPos);
        }
        transform.position = startPos;
    }
}