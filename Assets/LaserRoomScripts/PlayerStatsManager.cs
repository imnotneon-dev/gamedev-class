using UnityEngine;
using UnityEngine.Events;

public class PlayerStatsManager : MonoBehaviour
{
    [SerializeField] private PlayerStats stats;
    [SerializeField] private float maxHealth = 100f;
    public UnityEvent<float, float> OnHealthChanged;
    public UnityEvent OnPlayerDied;

    private void Start()
    {
        ResetStats();
    }

    public void TakeDamage(float damage)
    {
        if (stats == null || stats.Health <= 0) 
            return;

        stats.Health = Mathf.Max(0f, stats.Health - damage);
        OnHealthChanged?.Invoke(stats.Health, maxHealth);

        if (stats.Health <= 0f)
        {
            OnPlayerDied?.Invoke();
        }
    }
    public void Heal(float amount)
    {
        if (stats == null) 
            return;

        stats.Health = Mathf.Min(maxHealth, stats.Health + amount);
        OnHealthChanged?.Invoke(stats.Health, maxHealth);
    }
    public void ResetStats()
    {
        if (stats != null)
        {
            stats.Health = maxHealth;
            OnHealthChanged?.Invoke(stats.Health, maxHealth);
        }
    }
}