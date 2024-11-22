using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManagement : MonoBehaviour
{
    public static ScoreManagement Instance { get; private set; }

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private Transform highScoreContainer; // High score panel i�indeki content
    [SerializeField] private GameObject highScoreEntryPrefab; // Tek bir high score giri�i i�in prefab

    private int currentScore = 0;
    private List<(string name, int score)> highScores = new List<(string name, int score)>();
    private const int MAX_HIGH_SCORES = 5;
    private const string HIGH_SCORES_KEY = "HighScores";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadHighScores();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Debug.Log("ScoreManagement started");
        if (highScoreContainer == null)
            Debug.LogError("highScoreContainer is not assigned!");
        if (highScoreEntryPrefab == null)
            Debug.LogError("highScoreEntryPrefab is not assigned!");
    }

    public void IncreaseScore()
    {
        currentScore++;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = $"{currentScore}";
        }
    }

    public void ResetScore()
    {
        currentScore = 0;
        UpdateScoreDisplay();
    }

    public void SaveHighScore(string playerName)
    {
        // E�er isim bo�sa veya sadece bo�luk karakterlerinden olu�uyorsa "Dude" olarak kaydet
        if (string.IsNullOrWhiteSpace(playerName))
        {
            playerName = "Dude";
        }

        // Mevcut en d���k skoru bul
        int lowestScore = 0;
        if (highScores.Count >= MAX_HIGH_SCORES)
        {
            lowestScore = highScores[highScores.Count - 1].score;
        }

        // Debug.Log ekleyelim sorunu g�rmek i�in
        Debug.Log($"Attempting to save score - Name: {playerName}, Score: {currentScore}");

        // Sadece mevcut skor s�f�rdan b�y�kse VE
        // ya liste hen�z dolmam��sa YA DA mevcut skor en d���k skordan b�y�kse kaydet
        if (currentScore > 0 && (highScores.Count < MAX_HIGH_SCORES || currentScore > lowestScore))
        {
            highScores.Add((playerName, currentScore));
            highScores.Sort((a, b) => b.score.CompareTo(a.score)); // B�y�kten k����e s�rala

            // E�er 5'ten fazla kay�t varsa, son kayd� sil
            if (highScores.Count > MAX_HIGH_SCORES)
            {
                highScores.RemoveAt(MAX_HIGH_SCORES);
            }

            SaveHighScoresToPrefs();
            UpdateHighScoreDisplay();

            // Debug.Log ekleyelim kay�t sonras�n� g�rmek i�in
            Debug.Log($"Score saved successfully - Total high scores: {highScores.Count}");
        }
        else
        {
            Debug.Log($"Score not saved - Current score: {currentScore}, Lowest score: {lowestScore}, High scores count: {highScores.Count}");
        }
    }

    private void SaveHighScoresToPrefs()
    {
        string highScoreString = "";
        foreach (var score in highScores)
        {
            highScoreString += $"{score.name},{score.score}|";
        }
        PlayerPrefs.SetString(HIGH_SCORES_KEY, highScoreString);
        PlayerPrefs.Save();
    }

    private void LoadHighScores()
    {
        highScores.Clear();
        string highScoreString = PlayerPrefs.GetString(HIGH_SCORES_KEY, "");

        if (!string.IsNullOrEmpty(highScoreString))
        {
            string[] scores = highScoreString.Split('|');
            foreach (string score in scores)
            {
                if (!string.IsNullOrEmpty(score))
                {
                    string[] parts = score.Split(',');
                    if (parts.Length == 2)
                    {
                        highScores.Add((parts[0], int.Parse(parts[1])));
                    }
                }
            }
        }
    }

    public void UpdateHighScoreDisplay()
    {
        // �nce mevcut t�m high score giri�lerini temizle
        foreach (Transform child in highScoreContainer)
        {
            Destroy(child.gameObject);
        }

        // Yeni high score giri�lerini olu�tur
        for (int i = 0; i < highScores.Count; i++)
        {
            GameObject entry = Instantiate(highScoreEntryPrefab, highScoreContainer);
            TMP_Text entryText = entry.GetComponent<TMP_Text>();
            if (entryText != null)
            {
                entryText.text = $"{i + 1}. {highScores[i].name} - {highScores[i].score}";
            }
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }
}