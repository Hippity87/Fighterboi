using UnityEngine;

public static class GameState
{
    public static string Player1Character { get; set; }
    public static string Player2Character { get; set; }

    // Options settings
    public static float MasterVolume { get; set; } = 1f;
    public static float MusicVolume { get; set; } = 1f;
    public static float SfxVolume { get; set; } = 1f;
    public static float Brightness { get; set; } = 1f;

    // Apply settings to the game
    public static void ApplySettings()
    {
        // Apply master volume (affects all audio)
        AudioListener.volume = MasterVolume;

        // Apply brightness (simulated by adjusting a global light or post-processing)
        // Note: This requires a Light2D or post-processing setup; for now, we'll log it
        Debug.Log($"Brightness set to: {Brightness}");

        // Apply music and SFX volumes (requires AudioSource setup in scenes)
        Debug.Log($"Music Volume: {MusicVolume}, SFX Volume: {SfxVolume}");
    }

    // Reset settings to default
    public static void ResetSettings()
    {
        MasterVolume = 1f;
        MusicVolume = 1f;
        SfxVolume = 1f;
        Brightness = 1f;
        ApplySettings();
    }
}