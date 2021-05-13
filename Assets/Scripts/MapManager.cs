using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class MapManager : MonoBehaviour
{
    public GameObject[] LevelsSelector;
    public Color CurrentLevelColor;
    public Color BeatenLevelColor;
    public Animator Animation;
    public Animator Animation2;
    public AudioSource Audio;
    void Start()
    {
        if (PlayerPrefs.GetInt("isMuted") == 1)
            Audio.mute = true;
        PlayerPrefs.SetInt("Level1", 1);
        for (int i = 0; i < LevelsSelector.Length + 1; i++)
        {
            if (PlayerPrefs.GetInt("Level" + (i + 1)) == 1)
            {
                Debug.Log("Level" + (i + 1) + " Unlocked");
                LevelsSelector[i].GetComponent<Button>().interactable = true;
                LevelsSelector[i].transform.Find($"Arrows{i + 1}/ArrowsContainer").transform.gameObject.SetActive(true);

                var colors = LevelsSelector[i].GetComponent<Button>().colors;
                colors.normalColor = BeatenLevelColor;
                LevelsSelector[i].GetComponent<Button>().colors = colors;

                LevelsSelector[i].GetComponentInChildren<TMP_Text>().text = (i + 1).ToString();
                
            }
            else
            {
                var colors = LevelsSelector[i - 1].GetComponent<Button>().colors;
                colors.normalColor = CurrentLevelColor;
                LevelsSelector[i - 1].GetComponent<Button>().colors = colors;
                break;
            }
        }
    }

    public void MainMenu()
    {
        Animation.SetTrigger("MainMenu");
        StartCoroutine(MainMenuTrans());
    }
    private IEnumerator scene;
    private IEnumerator MainMenuTrans()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(0);
    }
    public void PlayLevel(int Level)
    {
        Animation2.SetTrigger("play");
        scene = GameTrans(Level);
        StartCoroutine(scene);
    }
    private IEnumerator GameTrans(int scene)
    {
        yield return new WaitForSeconds(0.7f);
        SceneManager.LoadScene(scene);
    }

}
