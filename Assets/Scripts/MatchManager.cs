using UnityEngine;

public class MatchManager : MonoBehaviour
{
    [SerializeField] private Health survivorsBase;
    [SerializeField] private Health infectedBase;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private TMPro.TMP_Text resultText;

    private bool isFinished;

    public bool IsFinished => isFinished;

    private void Start()
    {
        if (survivorsBase == null || infectedBase == null ||
            resultPanel == null || resultText == null)
        {
            if (resultPanel != null)
                resultPanel.SetActive(true);

            if (resultText != null)
            {
                resultText.gameObject.SetActive(true);
                resultText.text = "MATCH SETUP ERROR";
            }

            Debug.LogError(
                "MatchManager: назначь обе базы, панель и текст.",
                this
            );

            enabled = false;
            return;
        }

        Time.timeScale = 1f;
        resultPanel.SetActive(false);

        survivorsBase.Died += OnSurvivorsBaseDestroyed;
        infectedBase.Died += OnInfectedBaseDestroyed;
    }

    private void OnSurvivorsBaseDestroyed()
    {
        EndMatch(false);
    }

    private void OnInfectedBaseDestroyed()
    {
        EndMatch(true);
    }

    private void EndMatch(bool survivorsWon)
    {
        if (isFinished)
            return;

        isFinished = true;

        resultText.text = survivorsWon
            ? "VICTORY\nSurvivors win"
            : "DEFEAT\nInfected win";

        resultText.gameObject.SetActive(true);
        resultPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    private void OnDestroy()
    {
        if (survivorsBase != null)
            survivorsBase.Died -= OnSurvivorsBaseDestroyed;

        if (infectedBase != null)
            infectedBase.Died -= OnInfectedBaseDestroyed;

        if (isFinished)
            Time.timeScale = 1f;
    }
}