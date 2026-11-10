using UnityEngine;

public class WorldHealthBar : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Image fillImage;
    [SerializeField]
    private Vector3 offset =
        new Vector3(0f, 2.5f, 0f);

    private Health health;
    private Camera viewCamera;

    private void Awake()
    {
        health = GetComponentInParent<Health>();
        viewCamera = Camera.main;
    }

    private void Start()
    {
        if (health == null || fillImage == null)
        {
            Debug.LogError(
                "WorldHealthBar: нужен родитель с Health и ссылка Fill Image.",
                this
            );

            enabled = false;
            return;
        }

        TeamMember teamMember = health.GetComponentInParent<TeamMember>();

        fillImage.color =
            teamMember != null && teamMember.Team == TeamId.Infected
                ? new Color32(234, 92, 99, 255)
                : new Color32(72, 219, 138, 255);

        health.Changed += UpdateFill;
        UpdateFill(health.CurrentHealth, health.MaxHealth);
    }

    private void LateUpdate()
    {
        if (health == null)
            return;

        transform.position = health.transform.position + offset;

        if (viewCamera != null)
        {
            transform.rotation = viewCamera.transform.rotation;
        }
    }

    private void UpdateFill(int current, int maximum)
    {
        float ratio = maximum > 0
            ? Mathf.Clamp01((float)current / maximum)
            : 0f;

        fillImage.rectTransform.localScale =
            new Vector3(ratio, 1f, 1f);
    }

    private void OnDestroy()
    {
        if (health != null)
        {
            health.Changed -= UpdateFill;
        }
    }
}