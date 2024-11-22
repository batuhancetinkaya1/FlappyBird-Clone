using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagement : MonoBehaviour
{
    public static UIManagement Instance { get; private set; }

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject preGamePanel;

    [SerializeField] private TMP_InputField gameOverNameInput;
    [SerializeField] private Button restartButton;    // Yeni eklenen
    [SerializeField] private Button returnMenuButton; // Yeni eklenen

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


        Debug.Log("UIManagement started");
        if (gameOverNameInput == null)
            Debug.LogError("gameOverNameInput is not assigned!");
        if (restartButton == null)
            Debug.LogError("restartButton is not assigned!");
        if (returnMenuButton == null)
            Debug.LogError("returnMenuButton is not assigned!");
        // Input field eventlerini dinle
        if (gameOverNameInput != null)
        {
            gameOverNameInput.onSubmit.AddListener(OnNameSubmitted);
            gameOverNameInput.onValueChanged.AddListener(OnNameInputChanged);
        }

        // Buton listenerlarý ekle
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartButtonPressed);
        }

        if (returnMenuButton != null)
        {
            returnMenuButton.onClick.AddListener(OnMainMenuButtonPressed);
        }
    }

    public void OnNameSubmitted(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            SaveScoreAndProceed();
        }
    }

    private void OnNameInputChanged(string value)
    {
        // Butonlarýn her zaman aktif olmasýný saðla
        if (restartButton != null) restartButton.interactable = true;
        if (returnMenuButton != null) returnMenuButton.interactable = true;
    }

    private void SaveScoreAndProceed()
    {
        string playerName = gameOverNameInput.text;
        ScoreManagement.Instance.SaveHighScore(playerName); // Direkt gönder
    }

    // Diðer panel metodlarý ayný kalýyor...

    public void ShowGameOverPanel()
    {
        HideAllPanels();
        gameOverPanel.SetActive(true);

        if (gameOverNameInput != null)
        {
            gameOverNameInput.text = ""; // Input field'ý temizle
            gameOverNameInput.ActivateInputField(); // Input field'ý aktif et
            Debug.Log("GameOver panel shown and input field cleared");
        }
        else
        {
            Debug.LogError("gameOverNameInput is null in ShowGameOverPanel");
        }
    }

    public void OnRestartButtonPressed()
    {
        SaveScoreAndProceed();
        GameManagement.Instance.RestartGame();
    }

    public void OnMainMenuButtonPressed()
    {
        SaveScoreAndProceed();
        GameManagement.Instance.ReturnToMenu();
    }

    public void OnPlayButtonPressed()
    {
        ShowPreGamePanel();
    }

    public void OnHighScoreButtonPressed()
    {
        ScoreManagement.Instance.UpdateHighScoreDisplay();
        ShowHighScorePanel();
    }

    public void OnStartGameButtonPressed()
    {
        GameManagement.Instance.StartGame();
    }

    public void OnPauseButtonPressed()
    {
        GameManagement.Instance.PauseGame();
    }

    public void OnResumeButtonPressed()
    {
        GameManagement.Instance.ResumeGame();
    }

    public void OnGameOverConfirmButtonPressed()
    {
        string playerName = gameOverNameInput.text;
        ScoreManagement.Instance.SaveHighScore(playerName);
        GameManagement.Instance.ReturnToMenu();
    }

    public void ShowMenuPanel()
    {
        HideAllPanels();
        menuPanel.SetActive(true);
    }

    public void ShowPreGamePanel()
    {
        HideAllPanels();
        preGamePanel.SetActive(true);
    }

    public void ShowGamePanel()
    {
        HideAllPanels();
        gamePanel.SetActive(true);
    }

    public void ShowHighScorePanel()
    {
        HideAllPanels();
        highScorePanel.SetActive(true);
        ScoreManagement.Instance.UpdateHighScoreDisplay();
    }

    public void ShowPausePanel()
    {
        HideAllPanels();
        pausePanel.SetActive(true);
    }

    private void HideAllPanels()
    {
        menuPanel.SetActive(false);
        gamePanel.SetActive(false);
        highScorePanel.SetActive(false);
        pausePanel.SetActive(false);
        gameOverPanel.SetActive(false);
        preGamePanel.SetActive(false);
    }
}