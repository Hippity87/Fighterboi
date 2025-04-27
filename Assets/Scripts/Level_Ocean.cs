using UnityEngine;

public class OceanLevel : Level
{
    public OceanLevel(GameObject groundPrefab) : base(
        Resources.Load<Sprite>("Sprites/bckg_02_ocean"),
        new Vector2(-4f, 0f),
        new Vector2(4f, 0f),
        "Ocean",
        groundPrefab
    )
    { }

    public override void InitializeLevel(GameObject backgroundObj, GameObject levelParent)
    {
        base.InitializeLevel(backgroundObj, levelParent);
        Debug.Log("Ocean Level initialized!");
    }
}