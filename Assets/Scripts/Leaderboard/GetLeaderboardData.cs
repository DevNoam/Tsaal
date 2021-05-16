using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.UI;

public class GetLeaderboardData : MonoBehaviour
{
    public string GetLeaderScript = "http://moominrewritten.000webhostapp.com/TsaalPHP/getData.php?";
    public GameObject ContentContainer;
    public GameObject PrefabContainer;
    private string[] alef = new string[] { "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י", "כ", "ל", "מ", "נ", "ס", "ע", "פ", "צ", "ק", "ר", "ש", "ת" };
    [Tooltip("You can request data sorted by typing (required): 'score' or 'time'")]
    public string getDataSortedas = "score";
    public TMP_Text YourPosition;
    public TMP_Text ScoreT;
    public TMP_Text TimeT;


    public void RequestLeaderboard(string LeaderboardID = "infinitelevel")
    {
        StartCoroutine(GetScores(LeaderboardID));
    }
    private IEnumerator GetScores(string table)
    {

        UnityWebRequest www = UnityWebRequest.Get(GetLeaderScript + $"table={table}&orderBy={getDataSortedas}");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            ErrorLoadingLeader();
        }
        else
        {
            if (www.downloadHandler.text == "No results")
                NoResults();
            else
                CreateLeaderView(www.downloadHandler.text);
        }
    }

    private void CreateLeaderView(string text)
    {
        DelLeader();
        int openID = PlayerPrefs.GetInt("openID");
        string playerNamePrefs = PlayerPrefs.GetString("PlayerName");
        string[] players = text.Split('\n');
        ContentContainer.transform.Find("LoadingData").gameObject.SetActive(false);

        for (int i = 1; i < players.Length; i++)
        {
            if (players[i].Length <= 1)
                return;
            string[] player = players[i].Split(' ');
            GameObject container = Instantiate(PrefabContainer, ContentContainer.transform);
            if (i == 1)
            {
                container.transform.GetComponent<Image>().color = new Color32(108, 253, 200, 255);
            }
            else if (i == 2)
            {
                container.GetComponent<Image>().color = new Color32(141, 255, 255, 255);
            }
            else if (i == 3)
            {
                container.GetComponent<Image>().color = new Color32(184, 224, 255, 255);
            }
            TMP_Text playerName = container.transform.Find("Name").GetComponent<TMP_Text>();
            playerName.text = player[0];
            container.transform.Find("Score").GetComponent<TMP_Text>().text = player[1];
            container.transform.Find("Time").GetComponent<TMP_Text>().text = player[2];
            if (player[3] == openID.ToString())
            {
                if (player[0] == playerNamePrefs)
                {
                    YourPosition.gameObject.SetActive(true);
                    YourPosition.text = $"{i} :הלבטב ךלש םוקימה";
                    container.GetComponent<Image>().color = new Color32(255, 229, 150, 255);
                }
            }
            for (int x = 0; x < alef.Length; x++)
            {
                if (playerName.text.Contains(alef[x]))
                {
                    playerName.isRightToLeftText = true;
                    break;
                }
            }
        }
    }

    public void LoadOther()
    {
        if (getDataSortedas == "score")
        {
            LoadByTime();
        }
        else if(getDataSortedas == "time")
        {
            LoadByScore();
        }
    }
    public void DelLeader()
    {
        foreach (Transform child in ContentContainer.transform)
        {
            if (child.transform.name != "LoadingData")
            {
                GameObject.Destroy(child.gameObject);
            }
        }
    }

    public void LoadByTime()
    {
        DelLeader();
        ScoreT.fontStyle = FontStyles.Underline;
        ScoreT.color = new Color32(255, 255, 255, 255);
        TimeT.fontStyle = FontStyles.Underline;
        TimeT.color = new Color32(255, 129, 129, 255);
        getDataSortedas = "time";
        RequestLeaderboard();

    }
    public void LoadByScore()
    {
        DelLeader();
        TimeT.fontStyle = FontStyles.Underline;
        TimeT.color = new Color32(255, 255, 255, 255);
        ScoreT.fontStyle = FontStyles.Underline;
        ScoreT.color = new Color32(255, 129, 129, 255);
        getDataSortedas = "score";
        RequestLeaderboard();
    }

    public void ErrorLoadingLeader()
    {
        ContentContainer.transform.Find("LoadingData").gameObject.SetActive(true);
        ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().text = "We've couldn't able to load the Leaderboard :/";
        ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().isRightToLeftText = false;
    }
    private void NoResults()
    {
        ContentContainer.transform.Find("LoadingData").gameObject.SetActive(true);
        ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().text = "הטבלה הזאת ריקה";
        ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().isRightToLeftText = true;
    }

    public void UploadingData(bool up)
    {
        ContentContainer.transform.Find("LoadingData").gameObject.SetActive(true);
        ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().isRightToLeftText = true;
        if (up == true)
            ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().text = "מעלה מידע..";
        else
        {
            ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().text = "טוען מידע..";
        }
    }

}
