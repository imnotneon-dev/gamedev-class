using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Can : MonoBehaviour
{
    public bool IsKnockedDown { get; private set; } = false;
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void Start()
    {
        rb.Sleep();
    }

    public void MarkAsKnocked()
    {
        IsKnockedDown = true;
    }

    public void ResetCan()
    {
        IsKnockedDown = false;

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        transform.position = startPosition;
        transform.rotation = startRotation;

        rb.Sleep();
    }
}