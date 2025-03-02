using UnityEngine;

public class MountainLevel : Level
{
    public MountainLevel() : base(
        Resources.Load<Sprite>("Sprites/bckg_01_mountain.png"), // Placeholder path
        new Vector2(-4f, 0f),
        new Vector2(4f, 0f),
        "Desert"
    )
    { }

    public override void InitializeLevel(GameObject backgroundObj)
    {
        backgroundObj.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
        // Future: Add mountain-specific power-ups (e.g., health pack)
        Debug.Log("Mountain Level initialized!");
    }
}