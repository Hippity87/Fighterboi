using UnityEngine;
using System.Collections.Generic;

public class LevelLoader : MonoBehaviour
{
    public GameObject backgroundPrefab; // Assign a blank sprite GameObject in Inspector
    private List<Level> availableLevels = new List<Level>();
    private GameObject currentBackground;

    void Start()
    {
        // Populate available levels
        availableLevels.Add(new CastleLevel());
        availableLevels.Add(new MountainLevel());
        availableLevels.Add(new TempleJungleLevel());
        availableLevels.Add(new OceanLevel());
        // Randomly select and load a level
        LoadRandomLevel();
    }

    void LoadRandomLevel()
    {
        if (availableLevels.Count == 0) return;

        int randomIndex = Random.Range(0, availableLevels.Count);
        Level selectedLevel = availableLevels[randomIndex];

        // Instantiate background
        currentBackground = Instantiate(backgroundPrefab, Vector3.zero, Quaternion.identity);
        currentBackground.name = "Background_" + selectedLevel.levelName;

        // Initialize the level
        selectedLevel.InitializeLevel(currentBackground);

        // Set spawn points (future: update Fighter positions)
        Debug.Log("Loaded " + selectedLevel.levelName + " level!");
    }

    // Method to access current level (for future Fighter spawning)
    public Level GetCurrentLevel()
    {
        return availableLevels[Random.Range(0, availableLevels.Count)]; // Temporary, refine later
    }

    void OnDestroy()
    {
        if (currentBackground != null) Destroy(currentBackground);
    }
}