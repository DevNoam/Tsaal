using UnityEngine;
using UnityEngine.UI;
public class SettingsManager : MonoBehaviour
{
    private int isMuted = 0;
    [SerializeField]
    private Image Volume;
    [SerializeField]
    private Sprite[] VolumeSprites;
    public AudioSource Audio;

    void Start()
    {
        if (PlayerPrefs.GetInt("isMuted") == 1) //Is Muted
        {
            Volume.sprite = VolumeSprites[1];
            Audio.mute = true;
        }
        else //Not Muted
        {
            Volume.sprite = VolumeSprites[0];
        }
    }

    public void MuteUnMute()
    {
        if (PlayerPrefs.GetInt("isMuted") == 1) //If is muted
        {
            PlayerPrefs.SetInt("isMuted", 0);
            Volume.sprite = VolumeSprites[0]; //Don't mute
            Audio.mute = false;
        }
        else //Not muted
        {
            PlayerPrefs.SetInt("isMuted", 1); //Mute
            Audio.mute = true;
            Volume.sprite = VolumeSprites[1];
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();
        Start();
    }
}
