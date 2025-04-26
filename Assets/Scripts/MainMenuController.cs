using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    private Canvas mainMenuCanvas;
    private Canvas optionsCanvas; // Options menu canvas

    void Start()
    {
        mainMenuCanvas = GameObject.Find("MainMenuCanvas")?.GetComponent<Canvas>();
        if (mainMenuCanvas == null)
        {
            Debug.LogError("MainMenuCanvas not found in MainMenu scene!");
            return;
        }

        optionsCanvas = GameObject.Find("OptionsCanvas")?.GetComponent<Canvas>();
        if (optionsCanvas == null)
        {
            Debug.LogError("OptionsCanvas not found in MainMenu scene!");
            return;
        }

        mainMenuCanvas.gameObject.SetActive(true);
        optionsCanvas.gameObject.SetActive(false);

        // Assign MainMenuCanvas button events
        Button[] mainMenuButtons = mainMenuCanvas.GetComponentsInChildren<Button>(true);
        foreach (Button button in mainMenuButtons)
        {
            if (button.name == "StartButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(StartGame);
            }
            else if (button.name == "OptionsButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(ShowOptions);
            }
            else if (button.name == "ExitButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(ExitGame);
            }
        }

        // Assign OptionsCanvas button events
        Button[] optionsButtons = optionsCanvas.GetComponentsInChildren<Button>(true);
        foreach (Button button in optionsButtons)
        {
            if (button.name == "ApplyButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(ApplyOptions);
            }
            else if (button.name == "ResetButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(ResetOptions);
            }
            else if (button.name == "BackButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(HideOptions);
            }
        }

        // Disable fighters in MainMenu scene
        GameObject[] fighters = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject fighter in fighters)
        {
            if (fighter.scene == SceneManager.GetActiveScene())
            {
                fighter.SetActive(false);
            }
        }

        // Apply initial settings
        GameState.ApplySettings();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void ShowOptions()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        optionsCanvas.gameObject.SetActive(true);

        // Update sliders with current settings
        Slider[] sliders = optionsCanvas.GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            if (slider.name == "MasterVolumeSlider") slider.value = GameState.MasterVolume;
            else if (slider.name == "MusicVolumeSlider") slider.value = GameState.MusicVolume;
            else if (slider.name == "SfxVolumeSlider") slider.value = GameState.SfxVolume;
            else if (slider.name == "BrightnessSlider") slider.value = GameState.Brightness;
        }
    }

    public void ApplyOptions()
    {
        Slider[] sliders = optionsCanvas.GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            if (slider.name == "MasterVolumeSlider") GameState.MasterVolume = slider.value;
            else if (slider.name == "MusicVolumeSlider") GameState.MusicVolume = slider.value;
            else if (slider.name == "SfxVolumeSlider") GameState.SfxVolume = slider.value;
            else if (slider.name == "BrightnessSlider") GameState.Brightness = slider.value;
        }
        GameState.ApplySettings();
        HideOptions();
    }

    public void ResetOptions()
    {
        GameState.ResetSettings();
        Slider[] sliders = optionsCanvas.GetComponentsInChildren<Slider>(true);
        foreach (Slider slider in sliders)
        {
            if (slider.name == "MasterVolumeSlider") slider.value = GameState.MasterVolume;
            else if (slider.name == "MusicVolumeSlider") slider.value = GameState.MusicVolume;
            else if (slider.name == "SfxVolumeSlider") slider.value = GameState.SfxVolume;
            else if (slider.name == "BrightnessSlider") slider.value = GameState.Brightness;
        }
    }

    public void HideOptions()
    {
        optionsCanvas.gameObject.SetActive(false);
        mainMenuCanvas.gameObject.SetActive(true);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}