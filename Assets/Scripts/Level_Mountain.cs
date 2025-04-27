using UnityEngine;

public class MountainLevel : Level
{
    public MountainLevel(GameObject groundPrefab) : base(
        Resources.Load<Sprite>("Sprites/bckg_01_mountain"),
        new Vector2(-4f, 0f),
        new Vector2(4f, 0f),
        "Mountain",
        groundPrefab
    )
    { }

    public override void InitializeLevel(GameObject backgroundObj, GameObject levelParent)
    {
        base.InitializeLevel(backgroundObj, levelParent);
        Debug.Log("Mountain Level initialized!");
    }
}