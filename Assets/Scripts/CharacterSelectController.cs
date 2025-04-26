using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterSelectController : MonoBehaviour
{
    private Canvas characterSelectCanvas;
    private bool player1Confirmed = false;
    private bool player2Confirmed = false;
    private string[] characters = { "Fighter1", "Fighter2", "Fighter3", "Fighter4" }; // Example characters

    void Start()
    {
        // Find CharacterSelectCanvas, including inactive objects
        Canvas[] canvases = FindObjectsByType<Canvas>(FindObjectsSortMode.None);
        characterSelectCanvas = null;
        foreach (Canvas canvas in canvases)
        {
            if (canvas.name == "CharacterSelectCanvas")
            {
                characterSelectCanvas = canvas;
                break;
            }
        }

        if (characterSelectCanvas == null)
        {
            Debug.LogError("CharacterSelectCanvas not found in CharacterSelect scene!");
            return;
        }

        characterSelectCanvas.gameObject.SetActive(true);
        Debug.Log("CharacterSelectCanvas activated");

        // Assign button events for character selection
        Button[] buttons = characterSelectCanvas.GetComponentsInChildren<Button>(true);
        Debug.Log($"Found {buttons.Length} buttons under CharacterSelectCanvas");
        foreach (Button button in buttons)
        {
            Debug.Log($"Processing button: {button.name}");
            if (button.name.StartsWith("P1_") && !button.name.EndsWith("ConfirmButton"))
            {
                string charName = button.name.Replace("P1_", "");
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => SelectCharacter(1, charName));
                Debug.Log($"Assigned P1 character button: {button.name} to select {charName}");
            }
            else if (button.name.StartsWith("P2_") && !button.name.EndsWith("ConfirmButton"))
            {
                string charName = button.name.Replace("P2_", "");
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => SelectCharacter(2, charName));
                Debug.Log($"Assigned P2 character button: {button.name} to select {charName}");
            }
            else if (button.name == "P1_ConfirmButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ConfirmSelection(1));
                Debug.Log("Assigned P1_ConfirmButton to ConfirmSelection(1)");
            }
            else if (button.name == "P2_ConfirmButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ConfirmSelection(2));
                Debug.Log("Assigned P2_ConfirmButton to ConfirmSelection(2)");
            }
        }
    }

    void SelectCharacter(int player, string character)
    {
        if (player == 1)
        {
            GameState.Player1Character = character;
            Debug.Log($"Player 1 selected: {character}");
        }
        else
        {
            GameState.Player2Character = character;
            Debug.Log($"Player 2 selected: {character}");
        }
    }

    void ConfirmSelection(int player)
    {
        if (player == 1)
        {
            player1Confirmed = true;
            Debug.Log("Player 1 confirmed selection");
        }
        else
        {
            player2Confirmed = true;
            Debug.Log("Player 2 confirmed selection");
        }

        Debug.Log($"Confirmation state: player1Confirmed={player1Confirmed}, player2Confirmed={player2Confirmed}");
        if (player1Confirmed && player2Confirmed)
        {
            Debug.Log("Both players confirmed, transitioning to LoadingScreen");
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}