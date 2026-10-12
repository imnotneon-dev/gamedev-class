using UnityEngine;
using UnityEngine.InputSystem;

public class BallThrower : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Transform throwPoint;
    [SerializeField] private float throwForce = 18f;

    private void Update()
    {
        if (GameManager.Instance == null) 
            return;

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (GameManager.Instance.CanThrowBall())
            {
                ThrowBall();
            }
        }
    }

    private void ThrowBall()
    {
        if (ballPrefab == null || throwPoint == null) 
            return;

        GameObject ball = Instantiate(ballPrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody rb = ball.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.AddForce(throwPoint.forward * throwForce, ForceMode.Impulse);
        }

        GameManager.Instance.OnBallThrown();

        Destroy(ball, 2f);
    }
}