using UnityEngine;

public class OceanLevel : Level
{
    public OceanLevel() : base(
        Resources.Load<Sprite>("Sprites/bckg_02_ocean"), // Placeholder path
        new Vector2(-4f, 0f),
        new Vector2(4f, 0f),
        "Ocean"
    )
    { }

    public override void InitializeLevel(GameObject backgroundObj)
    {
        backgroundObj.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
        // Future: Add ocean-specific power-ups (e.g., health pack)
        Debug.Log("Ocean Level initialized!");
    }
}