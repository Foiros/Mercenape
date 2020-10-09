﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Created by Bao: Only script for spawning enemies
public class EnemySpawnerScript : MonoBehaviour
{
    public enum SpawnState { Spawning, Waiting, Counting }

    [System.Serializable]
    public class Group
    {
        public int shredCount;
        public int mowerCount;
        public int enemyIncreasedHP;
        public int enemyIncreasedDamage;
    }

    public Group group = new Group();
    //public List<Group> groups = new List<Group>();

    private int currentGroup = 0;
    private int currentWave = 1;

    public float timeBetweenGroups = 3f;

    private float groupCountdown;        // Count down to next group
    private float searchCountdown = 1f;  // Count down for searching any alive enemy

    public Transform[] enemies = new Transform[2];
    private List<Transform> spawnList = new List<Transform>();

    public SpawnState state = SpawnState.Counting;

    private PlayerCurrency playerCurrency;
    private GameMaster gm;

    private void Start()
    {
        playerCurrency = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerCurrency>();
        gm = GameObject.FindGameObjectWithTag("Player").GetComponent<GameMaster>();

        groupCountdown = timeBetweenGroups;

        IncreaseDifficulty();
    }

    private void Update()
    {
        // When player is fighting a group
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                // Finish group when player kill all enemy
                GroupCompleted();                
            }
            else
            {
                // If there's still enemy alive, wait for player to kill them all
                return;
            }
        }

        // Spawn group when count down is finished
        if (groupCountdown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                // Start spawning group
                StartCoroutine(SpawnWave());
            }
        }
        else
        {
            // Otherwise count down
            groupCountdown -= Time.deltaTime;
        }      
    }

    // Group completed and prepare new group
    void GroupCompleted()
    {
        // Prepare a new group
        state = SpawnState.Counting;
        groupCountdown = timeBetweenGroups;

        CheckWaveEnd();
        
        IncreaseDifficulty();

        Debug.Log("Going to group: " + currentGroup);
    }

    // When player get enough karma
    void CheckWaveEnd()
    {
        if (playerCurrency.playerKarma >= gm.lvMaxKarma)
        {
            print("Wave " + currentWave + " completed!!!!!");
            currentWave++;
            currentGroup = 0;   // Reset group
            groupCountdown = timeBetweenGroups * 2; // Wait a bit longer than normal
        }
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
    IEnumerator SpawnWave()
    {
        Debug.Log("Spawning group: " + currentGroup);
        state = SpawnState.Spawning;

        // Spawn
        for (int i = 0; i < spawnList.Count; i++)
        {          
            SpawnEnemy(spawnList[i]);

            yield return new WaitForSeconds(1f / RandomSpawnRate());
        }

        state = SpawnState.Waiting;

        yield break;
    }

    // Use this in SpawnWave
    void SpawnEnemy(Transform _enemy)
    {
        Instantiate(_enemy, transform.position, transform.rotation);
    }

    // Next group more difficult, spawn pattern
    void IncreaseDifficulty()
    {
        currentGroup++;

        if (currentGroup == 1) { group.shredCount = 3; group.mowerCount = 0; }

        if (currentGroup == 2) { group.shredCount = 4; group.mowerCount = 1; }
        
        if (currentGroup >= 3)
        {
            group.shredCount = Random.Range(4, 6 + 1);
            group.mowerCount = RandomMower();
        }

        MakeSpawnList();

        // Increase enemy HP and Damage
        group.enemyIncreasedHP += 2;
        group.enemyIncreasedDamage += 1;
    }

    // Make a list to spawn
    private void MakeSpawnList()
    {
        spawnList.Clear();

        for (int i = 0; i < group.shredCount; i ++)
        {
            // Add Shred
            spawnList.Add(enemies[0]);
        }

        for (int i = 0; i < group.mowerCount; i++)
        {
            // Add Mower
            spawnList.Add(enemies[1]);
        }
    }

    int RandomMower()
    {
        bool isOneMower = (Random.value > 0.85f);

        if (isOneMower)
        {
            return 1;
        }
        else
        {
            return 2;
        }       
    }

    private float RandomSpawnRate() { return Random.Range(0.2f, 0.5f); }
}
