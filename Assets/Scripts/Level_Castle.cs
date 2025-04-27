using UnityEngine;
using System.Collections.Generic;

public class CastleLevel : Level
{
    public CastleLevel(GameObject groundPrefab, List<GameObject> platformPrefabs, List<GameObject> powerUpPrefabs) : base(
        Resources.Load<Sprite>("Sprites/bckg_03_castle"),
        new Vector2(-4f, 0f),
        new Vector2(4f, 0f),
        "Castle",
        groundPrefab,
        CreatePlatformObjects(platformPrefabs),
        CreatePowerUpObjects(powerUpPrefabs)
    )
    { }

    private static List<LevelObject> CreatePlatformObjects(List<GameObject> platformPrefabs)
    {
        if (platformPrefabs == null || platformPrefabs.Count == 0) return null;
        List<LevelObject> platforms = new List<LevelObject>();
        for (int i = 0; i < platformPrefabs.Count; i++)
        {
            if (platformPrefabs[i] != null)
            {
                platforms.Add(new LevelObject(
                    platformPrefabs[i],
                    new Vector3(2f, 0f, 0f), // Default position, adjust as needed
                    $"Platform_Castle_{i + 1}",
                    "Ground"
                ));
            }
        }
        return platforms;
    }

    private static List<LevelObject> CreatePowerUpObjects(List<GameObject> powerUpPrefabs)
    {
        if (powerUpPrefabs == null || powerUpPrefabs.Count == 0) return null;
        List<LevelObject> powerUps = new List<LevelObject>();
        for (int i = 0; i < powerUpPrefabs.Count; i++)
        {
            if (powerUpPrefabs[i] != null)
            {
                powerUps.Add(new LevelObject(
                    powerUpPrefabs[i],
                    new Vector3(0f, 1f, 0f), // Default position above ground, adjust as needed
                    $"PowerUp_Castle_{i + 1}",
                    "PowerUp"
                ));
            }
        }
        return powerUps;
    }

    public override void InitializeLevel(GameObject backgroundObj, GameObject levelParent)
    {
        base.InitializeLevel(backgroundObj, levelParent);
        Debug.Log("Castle Level initialized!");
    }
}