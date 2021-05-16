using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class UploadData : MonoBehaviour
{
    private string encrpytUploadKey = "FuckHamas";
    public string addScoreScript = "http://localhost/TsaalDB/uploadScore.php?"; //be sure to add a ? to your url
    public string editScoreScript = "http://localhost/TsaalDB/editID.php?";
    public GetLeaderboardData leaderboardGet;
    private int openID = 0;
    private void Start()
    {
        Debug.Log("ID: " + PlayerPrefs.GetInt("openID"));
        leaderboardGet = gameObject.transform.GetComponent<GetLeaderboardData>();
    }


    public void Upload(string name, int score, int time, string table)
    {
        if (PlayerPrefs.HasKey("openID") && PlayerPrefs.GetInt("openID") >= 1)
        {
            openID = PlayerPrefs.GetInt("openID");
            StartCoroutine(EditID(openID, name, score, time, table));
        }
        else
        {
            StartCoroutine(UploadEnum(name, score, time, table));
        }
    }

    private IEnumerator EditID(int ID, string name, int score, int time, string table)
    {
        leaderboardGet.UploadingData(true);
        UnityWebRequest www = UnityWebRequest.Get(editScoreScript + $"table={table}&id={openID}&name={"\"" + name + "\""}&score={score}&time={time}&encryptKey={encrpytUploadKey}");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
        leaderboardGet.UploadingData(false);
        leaderboardGet.RequestLeaderboard(table);
    }


    private IEnumerator UploadEnum(string name, int score, int time, string table)
    {
        leaderboardGet.UploadingData(true);
        UnityWebRequest www = UnityWebRequest.Get(addScoreScript + $"table={table}&name={"\"" + name + "\""}&score={score}&time={time}&encryptKey={encrpytUploadKey}");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            string[] download = www.downloadHandler.text.Split('\n');
            for (int i = 0; i < download.Length; i++)
            {
                Debug.Log(download[i]);
            }
            PlayerPrefs.SetInt("openID", int.Parse(download[1]));
        }
        leaderboardGet.UploadingData(false);
        leaderboardGet.RequestLeaderboard(table);
    }
}