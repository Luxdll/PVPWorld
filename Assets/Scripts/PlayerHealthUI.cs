using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private Health playerHealth;

    private TMPro.TMP_Text healthText;
    private PlayerRespawn playerRespawn;
    private int lastDisplayedSecond = -1;

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

        playerRespawn = playerHealth.GetComponent<PlayerRespawn>();

        playerHealth.Changed += OnHealthChanged;
        playerHealth.Died += OnPlayerDied;

        OnHealthChanged(
            playerHealth.CurrentHealth,
            playerHealth.MaxHealth
        );
    }

    private void Update()
    {
        if (playerRespawn == null || !playerRespawn.IsRespawning)
            return;

        int seconds = Mathf.CeilToInt(playerRespawn.RemainingTime);

        if (seconds == lastDisplayedSecond)
            return;

        lastDisplayedSecond = seconds;
        healthText.text = $"RESPAWN: {seconds}";
    }

    private void OnHealthChanged(int current, int maximum)
    {
        lastDisplayedSecond = -1;
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