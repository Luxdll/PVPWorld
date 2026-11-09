using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    private int currentHealth;

    public int CurrentHealth => currentHealth;
    public int MaxHealth => maxHealth;

    public event System.Action<int, int> Changed;
    public event System.Action Died;

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0 || currentHealth <= 0)
            return;

        currentHealth = Mathf.Max(0, currentHealth - damage);
        Changed?.Invoke(currentHealth, maxHealth);

        if (currentHealth == 0)
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}