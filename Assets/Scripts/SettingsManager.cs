using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using System.Text.RegularExpressions;


public class SettingsManager : MonoBehaviour
{
    private int isMuted = 0;
    [SerializeField]
    private Image Volume;
    [SerializeField]
    private Sprite[] VolumeSprites;
    public AudioManager AudioManager;
    public TMP_InputField playerNameText;
    public string playerName;
    //public TMP_Text availabilityInfo;
    //public GameObject availabilityInfoWarn;
    private string[] alef = new string[] { "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י", "כ", "ל", "מ", "נ", "ס", "ע", "פ", "צ", "ק", "ר", "ש", "ת" };


    void Start()
    {
/*#if UNITY_WEBGL
        try
        {
        playerNameText.interactable = false;
        playerNameText.text = "אפשרות זו אינה זמינה.";
        availabilityInfo.text = "אין אפשרות להעלות שם בגירסת הדפדפן, יש להוריד את המשחק מהחנות על מנת להשתמש באופצייה הזו.";
        availabilityInfoWarn.SetActive(false);
        }
        catch (System.Exception)
        {
            Debug.Log("");
        }
        #endif*/
        if (PlayerPrefs.GetInt("isMuted") == 1) //Is Muted
        {
            Volume.sprite = VolumeSprites[1];
            AudioManager.isMuted = true;
        }
        else //Not Muted
        {
            Volume.sprite = VolumeSprites[0];
            AudioManager.isMuted = false;
        }
        try
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            if (playerName.Length >= 1)
            {
                playerNameText.text = playerName;
                playerNameText.interactable = false;
            }
        }
        catch (System.Exception)
        {
            return;
        }
    }

    public void MuteUnMute()
    {
        if (PlayerPrefs.GetInt("isMuted") == 1) //If is muted
        {
            PlayerPrefs.SetInt("isMuted", 0);
            Volume.sprite = VolumeSprites[0]; //Don't mute
            AudioManager.isMuted = false;
            AudioManager.Play("AudioSettingOn");
        }
        else //Not muted
        {
            PlayerPrefs.SetInt("isMuted", 1); //Mute
            AudioManager.isMuted = true;
            Volume.sprite = VolumeSprites[1];
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        playerNameText.interactable = true;
        Start();
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
            playerNameText.text = playerNameText.text.Replace(" ", "");
    }
    public void UpdatePlayerName(string playerName)
    {
        playerNameText.text = playerNameText.text.Replace(" ", "");
        PlayerPrefs.SetString("PlayerName", playerName);
    }
}
