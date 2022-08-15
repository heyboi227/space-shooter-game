using System.Collections;
using UnityEngine;

[System.Serializable]
public class SpawnableEnemy
{
    public GameObject enemyPrefab;
    [Range(0f, 100f)]
    public float chance = 100f;

    [HideInInspector]
    public double weight;
}

[System.Serializable]
public class SpawnablePowerup
{
    public GameObject powerupPrefab;
    [Range(0f, 100f)]
    public float chance = 100f;

    [HideInInspector]
    public double weight;
}

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private SpawnableEnemy[] enemyPrefab;
    [SerializeField]
    private SpawnablePowerup[] powerupPrefab;
    [SerializeField]
    private GameObject enemyContainer;
    [SerializeField]
    private GameObject powerupContainer;
    private bool spawnObjects = true;

    private double enemyAccumulatedWeight, powerupAccumulatedWeight;
    private readonly System.Random rand = new();

    void Start()
    {
        CalculateWeights();
    }

    public void StartSpawn()
    {
        StartCoroutine(SpawnEnemiesRoutine());
        StartCoroutine(SpawnPowerupsRoutine());
    }

    private void CalculateWeights()
    {
        enemyAccumulatedWeight = powerupAccumulatedWeight = 0;
        foreach (SpawnableEnemy enemy in enemyPrefab)
        {
            enemyAccumulatedWeight += enemy.chance;
            enemy.weight = enemyAccumulatedWeight;
        }
        foreach (SpawnablePowerup powerup in powerupPrefab)
        {
            powerupAccumulatedWeight += powerup.chance;
            powerup.weight = powerupAccumulatedWeight;
        }
    }

    private int GetRandomIndex()
    {
        double enemyRandom = rand.NextDouble() * enemyAccumulatedWeight;
        double powerupRandom = rand.NextDouble() * powerupAccumulatedWeight;
        for (int i = 0; i < enemyPrefab.Length; i++)
        {
            if (enemyPrefab[i].weight >= enemyRandom)
            {
                return i;
            }
        }
        for (int i = 0; i < powerupPrefab.Length; i++)
        {
            if (powerupPrefab[i].weight >= powerupRandom)
            {
                return i;
            }
        }
        return 0;
    }

    private IEnumerator SpawnEnemiesRoutine()
    {
        while (spawnObjects)
        {
            SpawnableEnemy randomEnemy = enemyPrefab[GetRandomIndex()];

            Vector3 spawnPosition = new(Random.Range(Config.leftLimit, Config.rightLimit), Config.upperLimit, transform.position.z);
            GameObject enemy = Instantiate(randomEnemy.enemyPrefab, spawnPosition, Quaternion.identity);
            enemy.transform.SetParent(enemyContainer.transform);

            yield return new WaitForSeconds(Random.Range(EnemyConfig.spawnRangeMin, EnemyConfig.spawnRangeMax));
        }
    }

    private IEnumerator SpawnPowerupsRoutine()
    {
        while (spawnObjects)
        {
            SpawnablePowerup randomPowerup = powerupPrefab[GetRandomIndex()];

            Vector3 spawnPosition = new(Random.Range(Config.leftLimit, Config.rightLimit), Config.upperLimit, transform.position.z);
            GameObject powerup = Instantiate(randomPowerup.powerupPrefab, spawnPosition, Quaternion.identity);
            powerup.transform.SetParent(powerupContainer.transform);

            yield return new WaitForSeconds(Random.Range(PowerupConfig.spawnRangeMin, PowerupConfig.spawnRangeMax));
        }
    }

    public void OnPlayerDeath()
    {
        spawnObjects = false;
    }
}
