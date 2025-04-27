using UnityEngine;
using System.Collections.Generic;

public class TempleJungleLevel : Level
{
    public TempleJungleLevel(GameObject groundPrefab, GameObject platformPrefab) : base(
        Resources.Load<Sprite>("Sprites/bckg_04_jungletemple"),
        new Vector2(-5f, 0f),
        new Vector2(5f, 0f),
        "TempleJungle",
        groundPrefab,
        new List<LevelObject>
        {
            new LevelObject(
                platformPrefab,
                new Vector3(2f, 0f, 0f),
                "Platform_TempleJungle",
                "Ground"
            )
        }
    )
    { }

    public override void InitializeLevel(GameObject backgroundObj, GameObject levelParent)
    {
        base.InitializeLevel(backgroundObj, levelParent);
        Debug.Log("Temple Jungle Level initialized!");
    }
}