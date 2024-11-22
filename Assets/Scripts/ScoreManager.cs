using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    public int playerWins = 0;
    public int computerWins = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void AddPlayerWin()
    {
        playerWins++;
    }

    public void AddComputerWin()
    {
        computerWins++;
    }
}
