using UnityEngine;
using UnityEngine.InputSystem;

public class FlashlightMechanic : MonoBehaviour
{
    [SerializeField] private Light flashlightSpotlight;
    [SerializeField] private float scanRange = 25f;

    [SerializeField] private LayerMask hiddenObjectLayer;
    [SerializeField] private LayerMask obstacleLayer;

    private bool isFlashlightOn = false;

    private void Start()
    {
        if (flashlightSpotlight != null)
        {
            flashlightSpotlight.enabled = isFlashlightOn;
        }
    }

    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.fKey.wasPressedThisFrame)
        {
            isFlashlightOn = !isFlashlightOn;
            if (flashlightSpotlight != null)
            {
                flashlightSpotlight.enabled = isFlashlightOn;
            }
        }

        if (isFlashlightOn && flashlightSpotlight != null)
        {
            ScanForHiddenObjects();
        }
    }

    private void ScanForHiddenObjects()
    {
        Collider[] candidates = Physics.OverlapSphere(transform.position, scanRange, hiddenObjectLayer);

        foreach (Collider col in candidates)
        {
            Vector3 origin = transform.position;

            Vector3 targetPoint = col.ClosestPoint(origin);
            Vector3 dirToObject = (targetPoint - origin).normalized;
            float distToObject = Vector3.Distance(origin, targetPoint);

            if (distToObject < 0.2f)
            {
                HiddenObject selfObj = col.GetComponent<HiddenObject>();
                if (selfObj != null) selfObj.Reveal();
                continue;
            }

            float angleToObject = Vector3.Angle(transform.forward, dirToObject);
            float halfConeAngle = flashlightSpotlight.spotAngle / 2f;

            if (angleToObject <= halfConeAngle)
            {
                float checkDist = Mathf.Max(0.1f, distToObject - 0.1f);

                if (!Physics.Raycast(origin, dirToObject, checkDist, obstacleLayer))
                {
                    Debug.DrawLine(origin, targetPoint, Color.green);

                    HiddenObject hiddenObj = col.GetComponent<HiddenObject>();
                    if (hiddenObj != null)
                    {
                        hiddenObj.Reveal();
                    }
                }
                else
                {
                    Debug.DrawLine(origin, targetPoint, Color.red);
                }
            }
        }
    }
}