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

    // Platform prefabs (located in Assets/Prefabs/)
    public GameObject platformTempleJungle;

    private List<Level> availableLevels = new List<Level>();
    private GameObject currentLevelParent;

    void Start()
    {
        // Populate available levels with their respective prefabs
        availableLevels.Add(new CastleLevel(groundShapeCastle));
        availableLevels.Add(new MountainLevel(groundShapeMountain));
        availableLevels.Add(new TempleJungleLevel(groundShapeTempleJungle, platformTempleJungle));
        availableLevels.Add(new OceanLevel(groundShapeOcean));
        LoadRandomLevel();
    }

    void LoadRandomLevel()
    {
        if (availableLevels.Count == 0) return;

        int randomIndex = Random.Range(0, availableLevels.Count);
        Level selectedLevel = availableLevels[randomIndex];

        // Create a parent object for this level's objects
        currentLevelParent = new GameObject("Level_" + selectedLevel.levelName);
        currentLevelParent.transform.position = Vector3.zero;

        // Instantiate background
        GameObject background = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity, currentLevelParent.transform);
        background.name = "Background_" + selectedLevel.levelName;

        // Initialize the level
        selectedLevel.InitializeLevel(background, currentLevelParent);

        // Set spawn points (future: update Fighter positions)
        Debug.Log("Loaded " + selectedLevel.levelName + " level!");
    }

    public Level GetCurrentLevel()
    {
        return availableLevels[Random.Range(0, availableLevels.Count)]; // Temporary, refine later
    }

    void OnDestroy()
    {
        if (currentLevelParent != null) Destroy(currentLevelParent);
    }
}