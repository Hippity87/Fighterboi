using UnityEngine;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    public GameObject backgroundPrefab; // Assign a blank sprite GameObject in Inspector

    // Ground prefabs for each level (located in Assets/Prefabs/)
    public GameObject groundShapeCastle;
    public GameObject groundShapeMountain;
    public GameObject groundShapeTempleJungle;
    public GameObject groundShapeOcean;

    // Platform prefabs for each level (located in Assets/Prefabs/)
    public GameObject platformCastle; // Can be null if no platforms
    public GameObject platformMountain; // Can be null if no platforms
    public GameObject platformTempleJungle;
    public GameObject platformOcean; // Can be null if no platforms

    // Power-up prefabs for each level (located in Assets/Prefabs/)
    public GameObject powerUpCastle; // Can be null if no power-ups
    public GameObject powerUpMountain; // Can be null if no power-ups
    public GameObject powerUpTempleJungle; // Can be null if no power-ups
    public GameObject powerUpOcean; // Can be null if no power-ups

    private List<Level> availableLevels = new List<Level>();
    private GameObject currentLevelParent;
    private Level currentLevel; // Store the current level for access

    void Start()
    {
        // Populate available levels with their respective prefabs
        availableLevels.Add(new CastleLevel(
            groundShapeCastle,
            platformCastle != null ? new List<GameObject> { platformCastle } : null,
            powerUpCastle != null ? new List<GameObject> { powerUpCastle } : null
        ));
        availableLevels.Add(new MountainLevel(
            groundShapeMountain,
            platformMountain != null ? new List<GameObject> { platformMountain } : null,
            powerUpMountain != null ? new List<GameObject> { powerUpMountain } : null
        ));
        availableLevels.Add(new TempleJungleLevel(
            groundShapeTempleJungle,
            platformTempleJungle != null ? new List<GameObject> { platformTempleJungle } : null,
            powerUpTempleJungle != null ? new List<GameObject> { powerUpTempleJungle } : null
        ));
        availableLevels.Add(new OceanLevel(
            groundShapeOcean,
            platformOcean != null ? new List<GameObject> { platformOcean } : null,
            powerUpOcean != null ? new List<GameObject> { powerUpOcean } : null
        ));
        LoadRandomLevel();
    }

    void LoadRandomLevel()
    {
        if (availableLevels.Count == 0) return;

        int randomIndex = Random.Range(0, availableLevels.Count);
        currentLevel = availableLevels[randomIndex];

        // Create a parent object for this level's objects
        currentLevelParent = new GameObject("Level_" + currentLevel.levelName);
        currentLevelParent.transform.position = Vector3.zero;

        // Instantiate background
        GameObject background = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity, currentLevelParent.transform);
        background.name = "Background_" + currentLevel.levelName;

        // Initialize the level
        currentLevel.InitializeLevel(background, currentLevelParent);

        // Set fighter positions based on spawn points
        GameObject fighter1 = GameObject.Find("fighter_1");
        GameObject fighter2 = GameObject.Find("fighter_1 (1)");
        if (fighter1 != null)
        {
            fighter1.transform.position = new Vector3(currentLevel.player1SpawnPoint.x, currentLevel.player1SpawnPoint.y, 0);
            Debug.Log($"Positioned fighter_1 at {fighter1.transform.position}");
        }
        else
        {
            Debug.LogWarning("fighter_1 not found in the scene!");
        }
        if (fighter2 != null)
        {
            fighter2.transform.position = new Vector3(currentLevel.player2SpawnPoint.x, currentLevel.player2SpawnPoint.y, 0);
            Debug.Log($"Positioned fighter_1 (1) at {fighter2.transform.position}");
        }
        else
        {
            Debug.LogWarning("fighter_1 (1) not found in the scene!");
        }

        Debug.Log("Loaded " + currentLevel.levelName + " level!");
    }

    public Level GetCurrentLevel()
    {
        return currentLevel; // Return the current level instead of a random one
    }

    void OnDestroy()
    {
        if (currentLevelParent != null) Destroy(currentLevelParent);
    }
}