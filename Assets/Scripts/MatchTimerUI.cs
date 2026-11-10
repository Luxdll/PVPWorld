using UnityEngine;

[RequireComponent(typeof(TMPro.TextMeshProUGUI))]
public class MatchTimerUI : MonoBehaviour
{
    [SerializeField] private MatchManager matchManager;

    private TMPro.TMP_Text timerText;
    private float elapsedTime;
    private int lastDisplayedSecond = -1;

    private void Awake()
    {
        timerText = GetComponent<TMPro.TMP_Text>();
    }

    private void Start()
    {
        if (matchManager == null)
        {
            timerText.text = "--:--";
            Debug.LogError(
                "MatchTimerUI: назначь MatchManager.",
                this
            );

            enabled = false;
            return;
        }

        UpdateText();
    }

    private void Update()
    {
        if (matchManager.IsFinished)
            return;

        elapsedTime += Time.deltaTime;
        UpdateText();
    }

    private void UpdateText()
    {
        int totalSeconds = Mathf.FloorToInt(elapsedTime);

        if (totalSeconds == lastDisplayedSecond)
            return;

        lastDisplayedSecond = totalSeconds;

        int minutes = totalSeconds / 60;
        int seconds = totalSeconds % 60;

        timerText.text = $"{minutes:00}:{seconds:00}";
    }
}