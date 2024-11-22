using UnityEngine;

public enum GameState
{
    Menu,
    PreGame,
    InGame,
    Paused,
    HighScore,
    GameOver
}

public class GameManagement : MonoBehaviour
{
    public static GameManagement Instance { get; private set; }
    [SerializeField] private BirdMovement birdMovement;
    [SerializeField] private PipeSpawner pipeSpawner;

    // State'i public yapýp dýþarýdan kontrol edilebilir hale getiriyoruz
    public GameState CurrentGameState { get; private set; } = GameState.PreGame;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ResetGameState();
        CurrentGameState = GameState.PreGame;
        UIManagement.Instance.ShowPreGamePanel();
    }

    private void ResetGameState()
    {
        Time.timeScale = 0f;
        ScoreManagement.Instance.ResetScore();
        birdMovement.BirdReset();
        pipeSpawner.ResetPipes();
    }

    public void StartGame()
    {
        CurrentGameState = GameState.InGame;
        UIManagement.Instance.ShowGamePanel();
        ResetGameState();
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        CurrentGameState = GameState.PreGame;
        UIManagement.Instance.ShowPreGamePanel();
        ResetGameState();
    }

    public void PauseGame()
    {
        CurrentGameState = GameState.Paused;
        UIManagement.Instance.ShowPausePanel();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        CurrentGameState = GameState.InGame;
        UIManagement.Instance.ShowGamePanel();
        Time.timeScale = 1f;
    }

    public void GameOver()
    {
        CurrentGameState = GameState.GameOver;
        UIManagement.Instance.ShowGameOverPanel();
        Time.timeScale = 0f;
    }

    public void ReturnToMenu()
    {
        CurrentGameState = GameState.Menu;
        UIManagement.Instance.ShowMenuPanel();
        ResetGameState();
    }

    // State kontrolü için yardýmcý metod
    public bool IsInGame()
    {
        return CurrentGameState == GameState.InGame;
    }
}