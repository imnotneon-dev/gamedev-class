using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Can : MonoBehaviour
{
    public bool IsKnockedDown { get; private set; } = false;

    private Vector3 startLocalPosition;
    private Quaternion startLocalRotation;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        startLocalPosition = transform.localPosition;
        startLocalRotation = transform.localRotation;
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

        transform.localPosition = startLocalPosition;
        transform.localRotation = startLocalRotation;

        rb.Sleep();
    }
}