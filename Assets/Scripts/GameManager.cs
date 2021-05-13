using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject[] spawners;
    public GameObject[] objects;
    public TMP_Text scoreText;
    public TMP_Text HPText;
    public TMP_Text TimerText;
    private int lastInstantiatePos;
    private bool lastInstantiatePosDoubled = false;
    private bool isGameOver = false;
    public GameOver gameOver;
    public LevelCompleted levelCompleted;
    public int score = 0;
    public int HP = 3;
    [Tooltip("Timer to end the game, 0 = Endless.")]
    public float Timer;
    private bool canCount = false;
    private bool timeBased;
    [SerializeField]
    private int difficulty = 1;
    [Tooltip("When to increase the Game difficulty? (In points)")]
    public int IncreaseDifficultyEvery = 10;
    [Tooltip("When to beat the game? (In points)")]
    public int CompleteLevelIn = 50;

    [Header("Reasons")]
    public string[] deathReasonsHP;
    public string[] deathReasonsTime;
    public string[] winningSentences;

    public AudioSource Audio;

    private void Start()
    {
        if (PlayerPrefs.GetInt("isMuted") == 1)
            Audio.mute = true;
        TimerText.text = Timer.ToString();
        if (Timer == 0)
        {
            timeBased = false;
            TimerText.gameObject.SetActive(false);
        }
        else if (Timer > 0)
            timeBased = true;
    }
    void Update()
    {
        if (!isGameOver && canCount == true)
        {
            if (timeBased == true && Timer > 0)
            {
                Timer -= Time.deltaTime;
                TimerText.text = Mathf.RoundToInt(Timer).ToString();
            }
            else if (Timer <= 0 && timeBased == true)
            {
                GameOver(deathReasonsTime[Random.Range(0, deathReasonsTime.Length)]);
            }
        }
    }

    public void addScore(int Add = 1)
    {
        score += Add;
        scoreText.text = score.ToString();
        if (score >= CompleteLevelIn && CompleteLevelIn != 0)
        {
            isGameOver = true;
            canCount = false;
            levelCompleted.Victory(score, winningSentences[Random.Range(0, winningSentences.Length)]);
            Debug.Log("Beated level 1");
        }
        CheckDifficulty();
    }
    private void CheckDifficulty()
    {
        if (score % IncreaseDifficultyEvery == 0)
        {
            difficulty++;
        }
    }
    public void DifficultyChangeAll(int Much)
    {
        difficulty = Much;
        for (int i = 0; i < 3; i++)
        {
            GameObject.FindWithTag("Container1").GetComponent<Rigidbody2D>().drag = Much;
            GameObject.FindWithTag("Container2").GetComponent<Rigidbody2D>().drag = Much;
            GameObject.FindWithTag("Container3").GetComponent<Rigidbody2D>().drag = Much;
            GameObject.FindWithTag("Bomb").GetComponent<Rigidbody2D>().drag = Much;
        }
    }
    public void HealthChange(int HPTemp = -1)
    {
        if (HPTemp < 0)
        {
            HP += HPTemp;
        }
        else if (HPTemp > 0)
        {
            HP -= HPTemp;
        }
        if (HP > 0)
        {
            HPText.text = HP.ToString();
        }
        if (HP <= 0)
        {
            GameOver(deathReasonsHP[Random.Range(0, deathReasonsHP.Length)]);
        }
    }
    public void AddHP(int Amount = 1)
    {
        if (Amount < 1)
            return;
        else 
        {
            HP += Amount;
            HPText.text = HP.ToString();
        }
    }

    public void GameOver(string reason)
    {
        canCount = false;
        isGameOver = true;
        timeBased = false;
        gameOver.GameOverInvoke(score, CompleteLevelIn, reason);
    }

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        if (timeBased == true && canCount == false)
            canCount = true;
        if (isGameOver)
            return;
        int rndSpawn = Random.Range(0, spawners.Length);
        if (lastInstantiatePos == rndSpawn && lastInstantiatePosDoubled == true)
        {
            Spawn();
            return;
        }
        else if (lastInstantiatePos == rndSpawn && lastInstantiatePosDoubled == false)
        {
            lastInstantiatePosDoubled = true;
        }
        else if (lastInstantiatePos != rndSpawn && lastInstantiatePosDoubled == true)
        {
            lastInstantiatePosDoubled = false;
        }
        int rndObj = Random.Range(0, objects.Length);

        GameObject obj = Instantiate(objects[rndObj], spawners[rndSpawn].transform);
        if (obj.name.Contains("Bomb"))
            obj.GetComponent<Bomb>().tile = rndSpawn;
        else if(obj.name.Contains("Object"))
            obj.GetComponent<Object>().tile = rndSpawn;

        if (difficulty > 1)
        {
            if (difficulty == 2)
                obj.GetComponent<Rigidbody2D>().drag = 8;
            else if (difficulty == 3)
                obj.GetComponent<Rigidbody2D>().drag = 6;
            else if (difficulty == 4)
                obj.GetComponent<Rigidbody2D>().drag = 4;
            else if (difficulty == 5)
                obj.GetComponent<Rigidbody2D>().drag = 2.5f;
            else if(difficulty == 6)
                obj.GetComponent<Rigidbody2D>().drag = 1;
            else if (difficulty >= 7)
                obj.GetComponent<Rigidbody2D>().drag = 0.5f;
        }
        lastInstantiatePos = rndSpawn;
    }
}
