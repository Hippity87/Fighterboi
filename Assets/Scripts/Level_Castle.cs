using UnityEngine;

public class CastleLevel : Level
{
    public CastleLevel(GameObject groundPrefab) : base(
        Resources.Load<Sprite>("Sprites/bckg_03_castle"),
        new Vector2(-4f, 0f),
        new Vector2(4f, 0f),
        "Castle",
        groundPrefab
    )
    { }

    public override void InitializeLevel(GameObject backgroundObj, GameObject levelParent)
    {
        base.InitializeLevel(backgroundObj, levelParent);
        Debug.Log("Castle Level initialized!");
    }
}