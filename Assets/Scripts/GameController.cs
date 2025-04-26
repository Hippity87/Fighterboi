using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour
{
    private Canvas pauseMenuCanvas;
    private Canvas optionsCanvas;
    private bool isPaused = false;
    private bool gameStarted = false;
    private float countdownTimer = 4f; // 3, 2, 1, Fight! (1 second each)
    private TextMeshProUGUI countdownText;

    // Health bar UI elements
    private Slider p1HealthBar;
    private Slider p2HealthBar;
    private Slider p1HealthBackdrop;
    private Slider p2HealthBackdrop;
    private TextMeshProUGUI p1NameText;
    private TextMeshProUGUI p2NameText;
    private float p1Health = 100f;
    private float p2Health = 100f;
    private float backdropLerpSpeed = 2f; // Speed of backdrop animation

    void Start()
    {
        // Initialize time scale regardless of canvas
        Time.timeScale = 0; // Start frozen until countdown finishes
        isPaused = false;

        // Find UI elements
        pauseMenuCanvas = GameObject.Find("PauseMenuCanvas")?.GetComponent<Canvas>();
        if (pauseMenuCanvas == null)
        {
            Debug.LogError("PauseMenuCanvas not found in Game scene! Continuing without pause menu.");
        }
        else
        {
            pauseMenuCanvas.gameObject.SetActive(isPaused);
            // Assign PauseMenuCanvas button events
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

        optionsCanvas = GameObject.Find("OptionsCanvas")?.GetComponent<Canvas>();
        if (optionsCanvas == null)
        {
            Debug.LogError("OptionsCanvas not found in Game scene!");
        }
        else
        {
            optionsCanvas.gameObject.SetActive(false);
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
        }

        // Countdown timer
        countdownText = GameObject.Find("CountdownText")?.GetComponent<TextMeshProUGUI>();
        if (countdownText == null)
        {
            Debug.LogError("CountdownText not found in Game scene!");
        }

        // Health bars
        p1HealthBar = GameObject.Find("P1HealthBar")?.GetComponent<Slider>();
        p2HealthBar = GameObject.Find("P2HealthBar")?.GetComponent<Slider>();
        p1HealthBackdrop = GameObject.Find("P1HealthBackdrop")?.GetComponent<Slider>();
        p2HealthBackdrop = GameObject.Find("P2HealthBackdrop")?.GetComponent<Slider>();
        p1NameText = GameObject.Find("P1NameText")?.GetComponent<TextMeshProUGUI>();
        p2NameText = GameObject.Find("P2NameText")?.GetComponent<TextMeshProUGUI>();

        if (p1HealthBar == null || p2HealthBar == null || p1HealthBackdrop == null || p2HealthBackdrop == null || p1NameText == null || p2NameText == null)
        {
            Debug.LogError("One or more health bar UI elements not found in Game scene!");
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

        // Enable fighters
        GameObject[] fighters = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject fighter in fighters)
        {
            if (fighter.scene == SceneManager.GetActiveScene())
            {
                fighter.SetActive(true);
            }
        }

        // Apply settings
        GameState.ApplySettings();
    }

    void Update()
    {
        if (!gameStarted)
        {
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
                }
            }
        }

        if (gameStarted)
        {
            // Update health bar backdrops
            if (p1HealthBackdrop != null && p2HealthBackdrop != null)
            {
                p1HealthBackdrop.value = Mathf.Lerp(p1HealthBackdrop.value, p1Health, Time.deltaTime * backdropLerpSpeed);
                p2HealthBackdrop.value = Mathf.Lerp(p2HealthBackdrop.value, p2Health, Time.deltaTime * backdropLerpSpeed);
            }

            // Example: Simulate damage for testing (remove in actual game)
            if (Input.GetKeyDown(KeyCode.Q)) TakeDamage(1, 10f); // Damage P1
            if (Input.GetKeyDown(KeyCode.P)) TakeDamage(2, 10f); // Damage P2

            if (Input.GetKeyDown(KeyCode.F1) && !optionsCanvas.gameObject.activeSelf)
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
        if (pauseMenuCanvas != null) pauseMenuCanvas.gameObject.SetActive(isPaused);
        if (!isPaused) Time.timeScale = 1;
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
            if (p1Health <= 0) EndGame(2);
        }
        else
        {
            p2Health = Mathf.Max(0, p2Health - damage);
            if (p2HealthBar != null) p2HealthBar.value = p2Health;
            if (p2Health <= 0) EndGame(1);
        }
    }

    private void EndGame(int winner)
    {
        Debug.Log($"Player {winner} wins!");
        Time.timeScale = 0;
        // Optionally show a win screen or transition back to MainMenu
        SceneManager.LoadScene("MainMenu");
    }
}