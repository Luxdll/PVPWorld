using System.Collections;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] private GameObject minionPrefab;
    [SerializeField, Min(1)] private int unitsPerWave = 3;
    [SerializeField, Min(0.1f)] private float waveInterval = 10f;
    [SerializeField, Min(0f)] private float initialDelay = 2f;
    [SerializeField, Min(1.1f)] private float spacing = 1.5f;

    private IEnumerator Start()
    {
        if (minionPrefab == null)
        {
            Debug.LogError(
                "WaveSpawner: назначь префаб бойца.",
                this
            );

            yield break;
        }

        yield return new WaitForSeconds(initialDelay);

        while (true)
        {
            SpawnWave();
            yield return new WaitForSeconds(waveInterval);
        }
    }

    private void SpawnWave()
    {
        for (int i = 0; i < unitsPerWave; i++)
        {
            float offset = (i - (unitsPerWave - 1) * 0.5f) * spacing;

            Vector3 position =
                transform.position + Vector3.forward * offset;

            Instantiate(minionPrefab, position, Quaternion.identity);
        }
    }
}