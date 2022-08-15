using UnityEngine;

public static class Config
{
    public static float upperLimit = 7f;
    public static float leftLimit = -9f;
    public static float rightLimit = 9f;
    public static float lowerLimit = -5f;
}
public static class PlayerConfig
{
    // Position
    public static float startXPosition = 0f;
    public static float startYPosition = 0f;
    public static float startZPosition = 0f;

    // Variables
    public static float speed = 7f;
    public static int lives = 3;

    // Playground limits
    public static float upperLimit = 4f;
    public static float lowerLimit = -4f;
    public static float leftLimit = -11.3f;
    public static float rightLimit = 11.3f;
}

public static class LaserConfig
{
    public static float speed = 8f;
    public static float fireRate = 0.2f;
    public static Vector3 offsetSpawn = new(0f, 0.8f, 0f);
    public static float distanceLimit = 11f;
}

public static class EnemyConfig
{
    public static float spawnRangeMin = 1f;
    public static float spawnRangeMax = 3f;
}

public static class PowerupConfig
{
    public static float speed = 3f;
    public static float spawnRangeMin = 5f;
    public static float spawnRangeMax = 10f;
    public static float timeLimit = 10f;
}

public static class StartingUFOConfig
{
    public static float rotateSpeed = 70f;
}