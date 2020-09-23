using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting }

    [System.Serializable]
    public class Wave
    {
        public int count;
        public float spawnRate;
        public int mowerCount;
        public int enemyIncreasedHP;
        public int enemyIncreasedDamage;
    }

    public Wave wave = new Wave();
    //public List<Wave> waves = new List<Wave>();

    private int currentWave = 1;

    public float timeBetweenWaves = 3f;

    private float waveCountdown;         // Count down to next wave
    private float searchCountdown = 1f;  // Count down for searching any alive enemy

    public Transform[] enemies = new Transform[2];

    //public delegate void CompleteGroup();
    //public static event CompleteGroup GroupCompleted;

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
                StartCoroutine(SpawnWave(wave));
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

        wave.mowerCount = 0;
        currentWave++;
        IncreaseDifficulty();

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
            _wave.spawnRate = Random.Range(1.5f, 5f);
            
            SpawnEnemy(enemies[RandomEnemyGenerator()]);

            yield return new WaitForSeconds(1f / _wave.spawnRate);
        }

        state = SpawnState.Waiting;

        yield break;
    }

    // Use this in SpawnWave
    void SpawnEnemy(Transform _enemy)
    {
        Instantiate(_enemy, transform.position, transform.rotation);
    }

    void IncreaseDifficulty()
    {
        wave.count++;
        wave.enemyIncreasedHP += 2;
        wave.enemyIncreasedDamage += 1;
    }

    int RandomEnemyGenerator()
    {
        if (wave.mowerCount == 2) { return 0; }

        int enemyIndex = 0;
        
        int randomRatio = Random.Range(0, 101);

        if (currentWave <= 2)
        {
            enemyIndex = 0;
        }
        else
        {           
            if((randomRatio < 85) && wave.mowerCount == 0)
            {
                enemyIndex = 1;
                wave.mowerCount++;
            }

            if((randomRatio < 15) && wave.mowerCount == 1)
            {
                enemyIndex = 1;
                wave.mowerCount++;
            }
            
        }
        return enemyIndex;
    }
}
