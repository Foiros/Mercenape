using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting}

    [System.Serializable]
    public class Wave
    {
        public int count;
        public float spawnRate;
    }

    public List<Wave> waves = new List<Wave>();

    private int currentWave = 1;

    public float timeBetweenWaves = 3f;

    private float waveCountdown;         // Count down to next wave
    private float searchCountdown = 1f;  // Count down for searching any alive enemy

    public Transform[] enemies = new Transform[2];
    int randomEnemy;

    private SpawnState state = SpawnState.Counting;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;        
    }

    private void Update()
    {
        // When player is fighting a wave
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                WaveCompleted();                
            }
            else
            {
                // If there's still enemy alive, let player kill them all
                return;
            }
        }

        // Spawn wave when count down is finished
        if (waveCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                // Start spawning wave
                StartCoroutine(SpawnWave(waves[0]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    // Wave completed and prepare new wave
    void WaveCompleted()
    {
        // Begin a new wave
        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;
      
        currentWave++;
        waves[0].count++;

        Debug.Log("Wave completed! Going to wave: " + currentWave);
    }

    // Check if enemies are still alive
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
              
        return true;
    }

    // Spawn enemies one by one with rate
    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning wave: " + currentWave);
        state = SpawnState.Spawning;

        // Spawn
        for (int i = 0; i < _wave.count; i++)
        {
            randomEnemy = Random.Range(0, 2);
            _wave.spawnRate = Random.Range(1.5f, 3.5f);

            SpawnEnemy(enemies[randomEnemy]);

            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    // Use this in SpawnWave
    void SpawnEnemy(Transform _enemy)
    {
        Instantiate(_enemy, transform.position, transform.rotation);
        Debug.Log("Spawning Enemy: " + _enemy.name);
    }
}
