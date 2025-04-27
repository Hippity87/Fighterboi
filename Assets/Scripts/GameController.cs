using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    [SerializeField] private Canvas pauseMenuCanvas;
    [SerializeField] private Canvas optionsCanvas;
    [SerializeField] private Canvas gameCanvas;
    private bool isPaused = false;
    private bool gameStarted = false;
    private float countdownTimer = 4f;
    private TextMeshProUGUI countdownText;

    private Slider p1HealthBar;
    private Slider p2HealthBar;
    private Slider p1HealthBackdrop;
    private Slider p2HealthBackdrop;
    private TextMeshProUGUI p1NameText;
    private TextMeshProUGUI p2NameText;
    private float p1Health = 100f;
    private float p2Health = 100f;
    private float backdropLerpSpeed = 2f;

    void Start()
    {
        Debug.Log("GameController Start() called");
        Time.timeScale = 0;
        Debug.Log($"Time.timeScale set to {Time.timeScale}");
        isPaused = false;

        if (pauseMenuCanvas == null)
        {
            Debug.LogError("PauseMenuCanvas not assigned in GameController Inspector! Continuing without pause menu.");
        }
        else
        {
            pauseMenuCanvas.gameObject.SetActive(isPaused);
            Button[] pauseButtons = pauseMenuCanvas.GetComponentsInChildren<Button>(true);
            foreach (Button button in pauseButtons)
            {
                if (button.name == "ResumeButton")
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(ResumeGame);
                }
                else if (button.name == "OptionsButton")
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(ShowOptions);
                }
                else if (button.name == "ExitToMainMenuButton")
                {
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(ExitToMainMenu);
                }
            }
        }

        if (optionsCanvas == null)
        {
            Debug.LogError("OptionsCanvas not assigned in GameController Inspector!");
        }
        else
        {
            optionsCanvas.gameObject.SetActive(false);
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
        }

        if (gameCanvas == null)
        {
            Debug.LogError("GameCanvas not assigned in GameController Inspector!");
            return; // Exit early to avoid further issues
        }

        // Revert to the original countdownText assignment logic
        countdownText = gameCanvas.GetComponentInChildren<TextMeshProUGUI>(true);
        if (countdownText != null)
        {
            Debug.Log($"First TextMeshProUGUI found: {countdownText.name}");
            if (countdownText.name != "CountdownText")
            {
                Debug.Log($"First TextMeshProUGUI is not CountdownText, setting to null");
                countdownText = null;
            }
        }
        else
        {
            Debug.LogWarning("No TextMeshProUGUI components found under GameCanvas on first attempt");
        }

        // Loop through all TextMeshProUGUI components to find CountdownText
        TextMeshProUGUI[] texts = gameCanvas.GetComponentsInChildren<TextMeshProUGUI>(true);
        Debug.Log($"Found {texts.Length} TextMeshProUGUI components under GameCanvas");
        foreach (TextMeshProUGUI text in texts)
        {
            Debug.Log($"TextMeshProUGUI component: {text.name}");
            if (text.name == "CountdownText") countdownText = text;
            else if (text.name == "P1NameText") p1NameText = text;
            else if (text.name == "P2NameText") p2NameText = text;
        }

        // Find all Slider components under gameCanvas
        Slider[] sliders = gameCanvas.GetComponentsInChildren<Slider>(true);
        Debug.Log($"Found {sliders.Length} Slider components under GameCanvas");
        foreach (Slider slider in sliders)
        {
            Debug.Log($"Slider component: {slider.name}");
            if (slider.name == "P1HealthBar") p1HealthBar = slider;
            else if (slider.name == "P2HealthBar") p2HealthBar = slider;
            else if (slider.name == "P1HealthBackdrop") p1HealthBackdrop = slider;
            else if (slider.name == "P2HealthBackdrop") p2HealthBackdrop = slider;
        }

        if (countdownText == null)
        {
            Debug.LogError("CountdownText not found under GameCanvas! Please ensure a TextMeshProUGUI object named 'CountdownText' exists under GameCanvas.");
        }
        else
        {
            Debug.Log($"CountdownText found, initial text: {countdownText.text}");
        }

        if (p1HealthBar == null || p2HealthBar == null || p1HealthBackdrop == null || p2HealthBackdrop == null || p1NameText == null || p2NameText == null)
        {
            Debug.LogError("One or more health bar UI elements not found under GameCanvas!");
        }
        else
        {
            p1HealthBar.maxValue = 100;
            p2HealthBar.maxValue = 100;
            p1HealthBackdrop.maxValue = 100;
            p2HealthBackdrop.maxValue = 100;
            p1HealthBar.value = p1Health;
            p2HealthBar.value = p2Health;
            p1HealthBackdrop.value = p1Health;
            p2HealthBackdrop.value = p2Health;
            p1NameText.text = GameState.Player1Character ?? "Player 1";
            p2NameText.text = GameState.Player2Character ?? "Player 2";
        }

        GameObject[] fighters = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log($"Found {fighters.Length} fighters with tag 'Player'");
        foreach (GameObject fighter in fighters)
        {
            if (fighter.scene == SceneManager.GetActiveScene())
            {
                fighter.SetActive(true);
                Debug.Log($"Activated fighter: {fighter.name}");
            }
        }

        GameState.ApplySettings();
    }

    void Update()
    {
        if (!gameStarted)
        {
            Debug.Log($"CountdownTimer: {countdownTimer}, Time.unscaledDeltaTime: {Time.unscaledDeltaTime}");
            countdownTimer -= Time.unscaledDeltaTime;
            if (countdownText != null)
            {
                if (countdownTimer > 3) countdownText.text = "3";
                else if (countdownTimer > 2) countdownText.text = "2";
                else if (countdownTimer > 1) countdownText.text = "1";
                else if (countdownTimer > 0) countdownText.text = "Fight!";
                else
                {
                    countdownText.gameObject.SetActive(false);
                    gameStarted = true;
                    Time.timeScale = 1;
                    Debug.Log("Countdown finished, game started, Time.timeScale set to 1");
                }
            }
            else
            {
                Debug.LogWarning("countdownText is null, skipping countdown update");
                // Fallback: Start the game if countdownText is missing
                if (countdownTimer <= 0)
                {
                    gameStarted = true;
                    Time.timeScale = 1;
                    Debug.Log("CountdownText is missing, but timer expired, starting game anyway");
                }
            }
        }

        if (gameStarted)
        {
            if (p1HealthBackdrop != null && p2HealthBackdrop != null)
            {
                p1HealthBackdrop.value = Mathf.Lerp(p1HealthBackdrop.value, p1Health, Time.deltaTime * backdropLerpSpeed);
                p2HealthBackdrop.value = Mathf.Lerp(p2HealthBackdrop.value, p2Health, Time.deltaTime * backdropLerpSpeed);
            }

            if (Input.GetKeyDown(KeyCode.Q)) TakeDamage(1, 10f);
            if (Input.GetKeyDown(KeyCode.P)) TakeDamage(2, 10f);

            if (Input.GetKeyDown(KeyCode.F1) && (optionsCanvas == null || !optionsCanvas.gameObject.activeSelf))
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        if (!gameStarted) return;

        isPaused = !isPaused;
        if (pauseMenuCanvas != null)
        {
            pauseMenuCanvas.gameObject.SetActive(isPaused);
        }
        Time.timeScale = isPaused ? 0 : 1;
        Debug.Log("TogglePause: isPaused = " + isPaused + ", Canvas active = " + (pauseMenuCanvas != null ? pauseMenuCanvas.gameObject.activeSelf.ToString() : "null"));
    }

    public void ResumeGame()
    {
        TogglePause();
    }

    public void ShowOptions()
    {
        if (pauseMenuCanvas != null) pauseMenuCanvas.gameObject.SetActive(false);
        if (optionsCanvas != null)
        {
            optionsCanvas.gameObject.SetActive(true);

            Slider[] sliders = optionsCanvas.GetComponentsInChildren<Slider>(true);
            foreach (Slider slider in sliders)
            {
                if (slider.name == "MasterVolumeSlider") slider.value = GameState.MasterVolume;
                else if (slider.name == "MusicVolumeSlider") slider.value = GameState.MusicVolume;
                else if (slider.name == "SfxVolumeSlider") slider.value = GameState.SfxVolume;
                else if (slider.name == "BrightnessSlider") slider.value = GameState.Brightness;
            }
        }
    }

    public void ApplyOptions()
    {
        if (optionsCanvas != null)
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
    }

    public void ResetOptions()
    {
        GameState.ResetSettings();
        if (optionsCanvas != null)
        {
            Slider[] sliders = optionsCanvas.GetComponentsInChildren<Slider>(true);
            foreach (Slider slider in sliders)
            {
                if (slider.name == "MasterVolumeSlider") slider.value = GameState.MasterVolume;
                else if (slider.name == "MusicVolumeSlider") slider.value = GameState.MusicVolume;
                else if (slider.name == "SfxVolumeSlider") slider.value = GameState.SfxVolume;
                else if (slider.name == "BrightnessSlider") slider.value = GameState.Brightness;
            }
        }
    }

    public void HideOptions()
    {
        if (optionsCanvas != null)
        {
            optionsCanvas.gameObject.SetActive(false);
            if (pauseMenuCanvas != null) pauseMenuCanvas.gameObject.SetActive(isPaused);
            if (!isPaused) Time.timeScale = 1;
        }
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    public void TakeDamage(int player, float damage)
    {
        if (player == 1)
        {
            p1Health = Mathf.Max(0, p1Health - damage);
            if (p1HealthBar != null) p1HealthBar.value = p1Health;
        }
        else
        {
            p2Health = Mathf.Max(0, p2Health - damage);
            if (p2HealthBar != null) p2HealthBar.value = p2Health;
        }
    }

    public float GetPlayerHealth(int player)
    {
        return player == 1 ? p1Health : p2Health;
    }
}