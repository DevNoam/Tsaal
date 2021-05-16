using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private TMP_Text version;
    public Animator movingSceneAnimation;
    private IEnumerator coroutine;
    public GameObject quit;
    private void Start()
    {
        version.text = $"V: {Application.version}";
#if UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS
        if (Screen.fullScreen == Screen.fullScreen)
        {
            quit.SetActive(true);
        }
        else
        {
#if UNITY_WEBGL
            Screen.fullScreen = Screen.fullScreen;
#endif
            quit.SetActive(true);
        }
#endif
        }

    public void SwitchScenes(int scene)
    {
        movingSceneAnimation.SetBool("play", true);
        coroutine = Scene(scene);
        StartCoroutine(coroutine);
    }

    private IEnumerator Scene(int Scene)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(Scene);
    }

    public void quitfullscreen()
    {
#if UNITY_WEBGL
        Screen.fullScreen = !Screen.fullScreen;
#endif
#if UNITY_ANDROID || UNITY_IOS
        Application.Quit();
#endif
    }
}
