using UnityEngine;

public class HiddenObject : MonoBehaviour
{
    private Renderer objRenderer;
    private float displayTimer = 0f;

    [SerializeField] private float lingerTime = 0.25f;

    private void Awake()
    {
        objRenderer = GetComponent<Renderer>();
        if (objRenderer != null)
        {
            objRenderer.enabled = false; 
        }
    }

    private void Update()
    {
        if (displayTimer > 0f)
        {
            displayTimer -= Time.deltaTime;

            if (displayTimer <= 0f && objRenderer != null)
            {
                objRenderer.enabled = false;
            }
        }
    }

    public void Reveal()
    {
        if (objRenderer != null)
        {
            objRenderer.enabled = true;
        }

        displayTimer = lingerTime;
    }
}