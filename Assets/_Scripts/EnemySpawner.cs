using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Random = System.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy[] _enemyPrefabs;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private float respawnRate = 6f;
    [SerializeField] private float initialSpawnDelay;
    [SerializeField] private int totalEnemiesToSpawn;
    [SerializeField] private int numberToSpawnEachTime = 1;

    private float spawnTimer;
    private int totalNumberSpawned;

    private void OnEnable()
    {
        spawnTimer = respawnRate - initialSpawnDelay;
    }

    private void Update()
    {
        spawnTimer += Time.deltaTime;
        
        if (ShouldSpawn())
        {
            Spawn();
        }
    }
    
    private bool ShouldSpawn()
    {
        if (totalEnemiesToSpawn > 0 && totalNumberSpawned >= totalEnemiesToSpawn) return false;
        
        return spawnTimer >= respawnRate;
    }
    
    private void Spawn()
    {
        spawnTimer = 0;

        var availableSpawnPoints = _spawnPoints.ToList();

        for (int i = 0; i < numberToSpawnEachTime; i++)
        {
            if (totalEnemiesToSpawn > 0 && totalNumberSpawned >= totalEnemiesToSpawn)
                break;

            Enemy prefab = ChooseRandomEnemyPrefab();
            if (prefab != null)
            {
                Transform spawnPoint = ChooseRandomSpawnPoint(availableSpawnPoints);
                if (availableSpawnPoints.Contains(spawnPoint))
                {
                    availableSpawnPoints.Remove(spawnPoint);
                }
                var enemy = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
                totalNumberSpawned++;
            }
        }
    }

    private Enemy ChooseRandomEnemyPrefab()
    {
        if (_enemyPrefabs.Length == 0)
        {
            return null;
        }

        if (_enemyPrefabs.Length == 1)
        {
            return _enemyPrefabs[0];
        }

        int index = UnityEngine.Random.Range(0, _enemyPrefabs.Length);
        return _enemyPrefabs[index];
    }

    private Transform ChooseRandomSpawnPoint(List<Transform> availableSpawnPoints)
    {
        if (availableSpawnPoints.Count == 0)
        {
            return transform;
        }

        if (availableSpawnPoints.Count == 1)
        {
            return availableSpawnPoints[0];
        }

        int index = UnityEngine.Random.Range(0, availableSpawnPoints.Count);
        return availableSpawnPoints[index];
    }
    
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawCube(transform.position, Vector3.one);

        foreach (var spawnPoint in _spawnPoints)
        {
            Gizmos.DrawSphere(spawnPoint.position, 0.5f);   
        }
    }
#endif
    
    //InvokeRepeating on Start
    /*private void InstantiateEnemies()  
    {
        var randomSpawn = new Vector3(UnityEngine.Random.Range(-10f, 10f),
            0f, UnityEngine.Random.Range(0, 10f));
        
        if (totalEnemiesSpawned <= 4)
        {
            Instantiate(_enemyPrefabs, randomSpawn, Quaternion.identity);
            totalEnemiesSpawned++;
        }
    }*/
}
