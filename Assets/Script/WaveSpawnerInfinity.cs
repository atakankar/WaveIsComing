using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WaveSpawnerInfinity : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;
    private int waveIndex = 0;
    public Text waveCountDownText;

    void Update ()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        
        countdown -= Time.deltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);
        waveCountDownText.text = string.Format("{0:00:00}",countdown);
    }

    IEnumerator SpawnWave()
    {
        timeBetweenWaves = timeBetweenWaves + 1f;
        waveIndex++;
        PlayerStats.Rounds++;
        Debug.Log("Wave Is Coming!");
        for(int i = 0; i <waveIndex;i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
        }
        
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}
