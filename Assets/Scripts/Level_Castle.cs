using UnityEngine;

public class CastleLevel : Level
{
    public CastleLevel() : base(
        Resources.Load<Sprite>("Sprites/bckg_03_castle.png"), // Placeholder path
        new Vector2(-4f, 0f),
        new Vector2(4f, 0f),
        "Desert"
    )
    { }

    public override void InitializeLevel(GameObject backgroundObj)
    {
        backgroundObj.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
        // Future: Add castle-specific power-ups (e.g., health pack)
        Debug.Log("Castle Level initialized!");
    }
}