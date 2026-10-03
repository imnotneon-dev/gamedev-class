using System.Collections;
using UnityEngine;

public class SlowMotionTrigger : MonoBehaviour
{
    [SerializeField][Range(0.1f, 0.9f)] private float timeScale = 0.4f;
    [SerializeField] private float realTimeDuration = 4f;               

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

            StartCoroutine(TriggerSlowMotion());
        }
    }

    IEnumerator TriggerSlowMotion()
    {
        Time.timeScale = timeScale;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        yield return new WaitForSecondsRealtime(realTimeDuration);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;

        Destroy(gameObject);
    }

    private void OnDisable()
    {
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f;
    }
}