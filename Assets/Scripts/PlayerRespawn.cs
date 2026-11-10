using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(CharacterController))]
public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject healthBar;
    [SerializeField] private MatchManager matchManager;
    [SerializeField] private float respawnDelay = 10f;

    private Health health;
    private CharacterController controller;
    private PlayerMovement movement;
    private PlayerAim aiming;
    private PlayerShooting shooting;

    private Vector3 spawnPosition;
    private Quaternion spawnRotation;
    private bool isRespawning;
    private float respawnAt;

    public bool IsRespawning => isRespawning;

    public float RemainingTime => isRespawning
        ? Mathf.Max(0f, respawnAt - Time.time)
        : 0f;

    private void Awake()
    {
        health = GetComponent<Health>();
        controller = GetComponent<CharacterController>();
        movement = GetComponent<PlayerMovement>();
        aiming = GetComponent<PlayerAim>();
        shooting = GetComponent<PlayerShooting>();

        spawnPosition = transform.position;
        spawnRotation = transform.rotation;
    }

    private void Start()
    {
        if (visual == null || healthBar == null ||
            matchManager == null || movement == null ||
            aiming == null || shooting == null)
        {
            Debug.LogError(
                "PlayerRespawn: проверь ссылки и компоненты игрока.",
                this
            );

            enabled = false;
            return;
        }

        health.Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        if (isRespawning || matchManager.IsFinished)
            return;

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        isRespawning = true;
        respawnAt = Time.time + respawnDelay;

        SetPlayerActive(false);
        controller.enabled = false;

        while (Time.time < respawnAt && !matchManager.IsFinished)
        {
            yield return null;
        }

        if (matchManager.IsFinished)
        {
            isRespawning = false;
            yield break;
        }

        transform.SetPositionAndRotation(spawnPosition, spawnRotation);

        health.RestoreFullHealth();
        controller.enabled = true;
        SetPlayerActive(true);

        isRespawning = false;
    }

    private void SetPlayerActive(bool value)
    {
        movement.enabled = value;
        aiming.enabled = value;
        shooting.enabled = value;
        visual.SetActive(value);
        healthBar.SetActive(value);
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.Died -= OnPlayerDied;
        }
    }
}
