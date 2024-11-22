using UnityEngine;
using UnityEngine.UI;
using TMPro; // Include TextMeshPro namespace
using System;

public class GameLogic : MonoBehaviour
{
    public Button rockButton;
    public Button paperButton;
    public Button scissorsButton;
    public Button shootButton;
    public Button replayButton;

    // Change from Unity's Text to TextMeshPro Text (TMP_Text)
    public TMP_Text playerChoiceText;
    public TMP_Text computerChoiceText;
    public TMP_Text gameResultText;

    // Add TextMeshPro Text elements for displaying scores
    public TMP_Text playerScoreText;
    public TMP_Text computerScoreText;

    private enum Choice { Rock, Paper, Scissors }
    private Choice playerChoice;
    private bool playerHasChosen = false;

    private void Start()
    {
        // Set up button listeners
        rockButton.onClick.AddListener(() => PlayerSelect(Choice.Rock));
        paperButton.onClick.AddListener(() => PlayerSelect(Choice.Paper));
        scissorsButton.onClick.AddListener(() => PlayerSelect(Choice.Scissors));
        shootButton.onClick.AddListener(Shoot);
        replayButton.onClick.AddListener(ResetGame);

        replayButton.interactable = false; // Disable Replay button initially

        // Initialize score UI
        UpdateScoreUI();
    }

    private void PlayerSelect(Choice choice)
    {
        if (playerHasChosen) return; // Prevent changing choice after Shoot

        playerChoice = choice;
        playerHasChosen = true;
        playerChoiceText.text = "Player: " + choice.ToString();
    }

    private void Shoot()
    {
        if (!playerHasChosen) return;

        // Generate a random choice for the computer
        Array choices = Enum.GetValues(typeof(Choice));
        Choice computerChoice = (Choice)choices.GetValue(UnityEngine.Random.Range(0, choices.Length));
        computerChoiceText.text = "Computer: " + computerChoice.ToString();

        // Determine the winner
        string result;
        if (playerChoice == computerChoice)
        {
            result = "It's a Tie!";
        }
        else if ((playerChoice == Choice.Rock && computerChoice == Choice.Scissors) ||
                 (playerChoice == Choice.Scissors && computerChoice == Choice.Paper) ||
                 (playerChoice == Choice.Paper && computerChoice == Choice.Rock))
        {
            result = "Player Wins!";
            ScoreManager.Instance.AddPlayerWin(); // Increment player score
        }
        else
        {
            result = "Computer Wins!";
            ScoreManager.Instance.AddComputerWin(); // Increment computer score
        }

        gameResultText.text = "Result: " + result;

        // Update the scores in the UI
        UpdateScoreUI();

        replayButton.interactable = true; // Enable Replay button
        DisableChoiceButtons(); // Disable choice buttons
    }

    private void DisableChoiceButtons()
    {
        rockButton.interactable = false;
        paperButton.interactable = false;
        scissorsButton.interactable = false;
    }

    private void ResetGame()
    {
        playerHasChosen = false;
        playerChoiceText.text = "Player: ";
        computerChoiceText.text = "Computer: ";
        gameResultText.text = " ";

        // Re-enable choice buttons and disable Replay button
        rockButton.interactable = true;
        paperButton.interactable = true;
        scissorsButton.interactable = true;
        replayButton.interactable = false;
    }

    // Method to update the score UI
    private void UpdateScoreUI()
    {
        playerScoreText.text = "Player Wins: " + ScoreManager.Instance.playerWins;
        computerScoreText.text = "Computer Wins: " + ScoreManager.Instance.computerWins;
    }
}
