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
        characterSelectCanvas = GameObject.Find("CharacterSelectCanvas")?.GetComponent<Canvas>();
        if (characterSelectCanvas == null)
        {
            Debug.LogError("CharacterSelectCanvas not found in CharacterSelect scene!");
            return;
        }

        characterSelectCanvas.gameObject.SetActive(true);

        // Assign button events for character selection
        Button[] buttons = characterSelectCanvas.GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            if (button.name.StartsWith("P1_"))
            {
                string charName = button.name.Replace("P1_", "");
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => SelectCharacter(1, charName));
            }
            else if (button.name.StartsWith("P2_"))
            {
                string charName = button.name.Replace("P2_", "");
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => SelectCharacter(2, charName));
            }
            else if (button.name == "P1_ConfirmButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ConfirmSelection(1));
            }
            else if (button.name == "P2_ConfirmButton")
            {
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() => ConfirmSelection(2));
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

        if (player1Confirmed && player2Confirmed)
        {
            SceneManager.LoadScene("LoadingScreen");
        }
    }
}