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
#if UNITY_WEBGL
        if (Screen.fullScreen == Screen.fullScreen)
        {
            quit.SetActive(true);
        }
        else
        {
            quit.SetActive(false);
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
        if (Screen.fullScreen == Screen.fullScreen)
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
    public void HREF(string url)
    {
        Application.OpenURL(url);
    }
}
