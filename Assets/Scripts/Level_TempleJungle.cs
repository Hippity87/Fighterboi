using UnityEngine;

public class TempleJungleLevel : Level
{
    public TempleJungleLevel() : base(
        Resources.Load<Sprite>("Sprites/bckg_04_jungletemple.png"), // Placeholder path
        new Vector2(-5f, 0f),
        new Vector2(5f, 0f),
        "Forest"
    )
    { }

    public override void InitializeLevel(GameObject backgroundObj)
    {
        // Set background sprite
        backgroundObj.GetComponent<SpriteRenderer>().sprite = backgroundSprite;
        // Future: Add jungle-specific power-ups (e.g., speed boost)
        Debug.Log("Temple Jungle Level initialized!");
    }
}