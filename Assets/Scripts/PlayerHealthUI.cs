using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    private TMPro.TMP_Text healthText;

    private void Awake()
    {
        healthText = GetComponent<TMPro.TMP_Text>();
    }

    private void Start()
    {
        if (playerHealth == null)
        {
            healthText.text = "HP: unavailable";

            Debug.LogError(
                "PlayerHealthUI: назначь здоровье игрока.",
                this
            );

            enabled = false;
            return;
        }

        playerHealth.Changed += OnHealthChanged;
        playerHealth.Died += OnPlayerDied;

        OnHealthChanged(
            playerHealth.CurrentHealth,
            playerHealth.MaxHealth
        );
    }

    private void OnHealthChanged(int current, int maximum)
    {
        healthText.text = $"HP: {current} / {maximum}";
    }

    private void OnPlayerDied()
    {
        healthText.text = "PLAYER DOWN";
    }

    private void OnDestroy()
    {
        if (playerHealth == null)
            return;

        playerHealth.Changed -= OnHealthChanged;
        playerHealth.Died -= OnPlayerDied;
    }
}