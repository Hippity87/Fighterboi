using UnityEngine;
using System.Collections.Generic;

public abstract class Level
{
    public Sprite backgroundSprite; // Background image for this level
    public Vector2 player1SpawnPoint; // Spawn point for Player 1
    public Vector2 player2SpawnPoint; // Spawn point for Player 2
    public string levelName; // Name for identification
    public GameObject groundPrefab; // Prefab for the ground (can be a Sprite Shape for curved terrain)
    public List<LevelObject> additionalObjects; // Additional objects like platforms, obstacles, or power-ups

    public Level(Sprite background, Vector2 p1Spawn, Vector2 p2Spawn, string name, GameObject ground = null, List<LevelObject> objects = null)
    {
        backgroundSprite = background;
        player1SpawnPoint = p1Spawn;
        player2SpawnPoint = p2Spawn;
        levelName = name;
        groundPrefab = ground; // Passed in from LevelLoader, assigned in Inspector
        additionalObjects = objects ?? new List<LevelObject>(); // Initialize empty list if null
    }

    // Virtual method for level initialization, can be overridden by derived classes
    public virtual void InitializeLevel(GameObject backgroundObj, GameObject levelParent)
    {
        // Set the background sprite
        if (backgroundSprite != null)
        {
            backgroundObj.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
            Debug.Log($"Background sprite set for level {levelName}");
        }
        else
        {
            Debug.LogWarning($"Background sprite is null for level {levelName}");
        }

        // Instantiate the ground prefab (e.g., a Sprite Shape with a PolygonCollider2D)
        if (groundPrefab != null)
        {
            GameObject ground = Object.Instantiate(groundPrefab, new Vector3(0, -2, 0), Quaternion.identity, levelParent.transform);
            ground.name = "GroundShape_" + levelName;
            ground.tag = "Ground"; // Ensure the tag is set for collision detection
            Debug.Log($"Ground instantiated for level {levelName} at position {ground.transform.position} with tag {ground.tag}");

            // Verify the collider
            PolygonCollider2D collider = ground.GetComponent<PolygonCollider2D>();
            if (collider == null)
            {
                Debug.LogWarning($"GroundShape_{levelName} does not have a PolygonCollider2D!");
            }
            else
            {
                Debug.Log($"GroundShape_{levelName} has a PolygonCollider2D with {collider.points.Length} points");
            }
        }
        else
        {
            Debug.LogWarning($"Ground prefab not assigned for level {levelName}. Fighters may not have a surface to land on.");
        }

        // Instantiate additional objects (e.g., platforms)
        foreach (var obj in additionalObjects)
        {
            GameObject instantiatedObj = Object.Instantiate(obj.prefab, obj.position, Quaternion.identity, levelParent.transform);
            instantiatedObj.name = obj.name;
            instantiatedObj.tag = obj.tag; // Set the tag (e.g., "Ground" for platforms)
            Debug.Log($"Instantiated additional object {obj.name} for level {levelName} with tag {obj.tag}");
        }
    }
}

public class LevelObject
{
    public GameObject prefab; // Prefab for the object (e.g., platform, obstacle)
    public Vector3 position; // Position in the scene
    public string name; // Name for identification in the Hierarchy
    public string tag; // Tag for collision detection (e.g., "Ground")

    public LevelObject(GameObject prefab, Vector3 position, string name, string tag = "Untagged")
    {
        this.prefab = prefab;
        this.position = position;
        this.name = name;
        this.tag = tag;
    }
}