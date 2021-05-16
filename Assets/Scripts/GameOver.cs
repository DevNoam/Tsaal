using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;
    private int Score = 0;
    private int HighScore = 0;
    private int time = 0;
    private bool isGameOver = false;
    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;
    public TMP_Text reasonToDeath;
    public bool infinite = false;
    public GetLeaderboardData Leaderboard;
    public UploadData uploadManager;
    public GameObject registerReminder;
    public string LeaderboardID;
    private bool isUploadingData = false;
    public TMP_InputField playerNameText;
    private string[] alef = new string[] { "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י", "כ", "ל", "מ", "נ", "ס", "ע", "פ", "צ", "ק", "ר", "ש", "ת" };


    public void GameOverInvoke(int Score, int requiredForBeaten, int time, string reason)
    {
        if (isGameOver == false)
            isGameOver = true;
        else if (isGameOver == true)
            return;

        if (infinite == true)
        {
            this.Score = Score;
            this.time = time;
            if (PlayerPrefs.HasKey("HighScore"))
            {
                HighScore = PlayerPrefs.GetInt("HighScore");
                if (Score >= HighScore)
                {
                    if (!PlayerPrefs.HasKey("PlayerName"))
                    {
                        registerReminder.SetActive(true);
                    }
                    PlayerPrefs.SetInt("HighScore", Score);
                    HighScore = Score;
                    Upload();
                }
            }
            else
            {
                if (PlayerPrefs.HasKey("PlayerName"))
                {
                    Upload();
                }
                PlayerPrefs.SetInt("HighScore", Score);
                HighScore = Score;
            }

            HighScoreText.text = $"/ {PlayerPrefs.GetInt("HighScore")}";
            if (Score >= PlayerPrefs.GetInt("HighScore"))
                reason = "!שדח גשיה";

            if (isUploadingData == false)
                Leaderboard.RequestLeaderboard(LeaderboardID);
        }
        else
            HighScoreText.text = $"/ {requiredForBeaten}";
        //Applying data:
        ScoreText.text = Score.ToString();

        if(reason.Length >= 1)
            reasonToDeath.text = reason;
        Panel.SetActive(true);
    }
    private void Upload()
    {
        isUploadingData = true;
        //UPLOAD HIGHSCORE TO LEADERBOARD
        string name = PlayerPrefs.GetString("PlayerName");
        if (name.Length >= 2)
        {
            uploadManager.Upload(name, Score, time, LeaderboardID);
        }

    }
    public void editPlayerName()
    {
        for (int i = 0; i < alef.Length; i++)
        {
            if (playerNameText.textComponent.text.Contains(alef[i]))
            {
                playerNameText.textComponent.GetComponent<TMP_Text>().isRightToLeftText = true;
                break;
            }
            else
                playerNameText.textComponent.GetComponent<TMP_Text>().isRightToLeftText = false;
        }
    }
    public void UpdatePlayerName(string playerName)
    {
        PlayerPrefs.SetString("PlayerName", playerName);
        Upload();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
