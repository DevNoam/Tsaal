using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCompleted : MonoBehaviour
{
    [SerializeField]
    GameObject Panel;
    private int Score = 0;
    private int HighScore = 0;

    public TMP_Text ScoreText;
    public TMP_Text HighScoreText;
    public TMP_Text reasonToDeath;
    [Tooltip("Which level to unlock after completing the level? = 1/ 2/ 3/ 4/ 5")]
    public int unlockLevel;
    // Start is called before the first frame update
    public void Victory(int Score, string reason)
    {
        PlayerPrefs.SetInt($"Level{unlockLevel}", 1);
        ScoreText.text = Score.ToString();
        HighScoreText.text = $"/ {Score}";
        if (reason.Length >= 1)
            reasonToDeath.text = reason;
        Panel.SetActive(true);
    }

    public void Continue()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
