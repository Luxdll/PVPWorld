using UnityEngine;

public class BaseHealthUI : MonoBehaviour
{
    [SerializeField] private Health baseHealth;
    [SerializeField] private RectTransform fill;

    private void Start()
    {
        if (baseHealth == null || fill == null)
        {
            Debug.LogError(
                "BaseHealthUI: назначь Base Health и Fill.",
                this
            );

            enabled = false;
            return;
        }

        baseHealth.Changed += UpdateFill;
        UpdateFill(baseHealth.CurrentHealth, baseHealth.MaxHealth);
    }

    private void UpdateFill(int current, int maximum)
    {
        float ratio = maximum > 0
            ? Mathf.Clamp01((float)current / maximum)
            : 0f;

        fill.localScale = new Vector3(ratio, 1f, 1f);
    }

    private void OnDestroy()
    {
        if (baseHealth != null)
            baseHealth.Changed -= UpdateFill;
    }
}