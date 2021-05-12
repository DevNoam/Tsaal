using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;
    private int Score = 0;
    private int HighScore = 0;

    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;
    public TMP_Text reasonToDeath;
    public bool infinite = false;

    public void GameOverInvoke(int Score, int requiredForBeaten, string reason)
    {
        if (infinite == true)
        {
            //Reciving data:
            this.Score = Score;
            if (PlayerPrefs.HasKey("HighScore"))
            {
                //if(googleServices......)
                HighScore = PlayerPrefs.GetInt("HighScore");
                if (Score > HighScore)
                {
                    PlayerPrefs.SetInt("HighScore", Score);
                    HighScore = Score;
                }
            }//Else if Google Or PlayerPrefs Exist
            else
            {
                //Save the HighScore
                PlayerPrefs.SetInt("HighScore", Score);
                HighScore = Score;
                //if(Connected to google....)
            }
        }
        
        //Applying data:
        ScoreText.text = Score.ToString();
        if (infinite == true)
        {
            HighScoreText.text = $"/ {PlayerPrefs.GetInt("HighScore")}";
            if (Score >= PlayerPrefs.GetInt("HighScore"))
                reason = "!שדח גשיה";
        }
        else
            HighScoreText.text = $"/ {requiredForBeaten}";
        
        if(reason.Length >= 1)
            reasonToDeath.text = reason;
        Panel.SetActive(true);
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
