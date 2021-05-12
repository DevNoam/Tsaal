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
    private void Start()
    {
        version.text = $"V: {Application.version}";
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
}
