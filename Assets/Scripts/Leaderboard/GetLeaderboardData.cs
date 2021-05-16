using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class GetLeaderboardData : MonoBehaviour
{
    public string GetLeaderScript = "http://localhost/TsaalDB/getData.php?";
    public GameObject ContentContainer;
    public GameObject PrefabContainer;
    private string[] alef = new string[] { "א", "ב", "ג", "ד", "ה", "ו", "ז", "ח", "ט", "י", "כ", "ל", "מ", "נ", "ס", "ע", "פ", "צ", "ק", "ר", "ש", "ת" };

    [ContextMenu("RequestLeader")]
    public void RequestLead()
    {
        StartCoroutine(GetScores("InfiniteLevel"));
    }


    public void RequestLeaderboard(string LeaderboardID)
    {
        StartCoroutine(GetScores(LeaderboardID));
    }
    private IEnumerator GetScores(string table)
    {
        UnityWebRequest www = UnityWebRequest.Get(GetLeaderScript + $"table={table}");
        www.downloadHandler = new DownloadHandlerBuffer();
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
            ErrorLoadingLeader();
        }
        else
        {
            if (www.downloadHandler.text == "No Results")
                NoResults();
            else
                CreateLeaderView(www.downloadHandler.text);
        }
    }

    private void CreateLeaderView(string text)
    {
        string[] players = text.Split('\n');
        Destroy(ContentContainer.transform.Find("LoadingData").gameObject);

        for (int i = 1; i < players.Length; i++)
        {
            if (players[i].Length <= 1)
                return;
            string[] player = players[i].Split(' ');
            GameObject container = Instantiate(PrefabContainer, ContentContainer.transform);
            container.transform.Find("Name").GetComponent<TMP_Text>().text = player[0];
            for (int x = 0; x < alef.Length; x++)
            {
                if (player[i].Contains(alef[x]))
                {
                    container.transform.Find("Name").GetComponent<TMP_Text>().isRightToLeftText = true;
                    break;
                }
            }
            container.transform.Find("Score").GetComponent<TMP_Text>().text = player[1];
            container.transform.Find("Time").GetComponent<TMP_Text>().text = player[2];
        }
    }

    public void ErrorLoadingLeader()
    {
        ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().text = "We've couldn't able to load the Leaderboard :/";
        ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().isRightToLeftText = false;
    }
    private void NoResults()
    {
        ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().text = "הקיר תאזה הלבטה";
        ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().isRightToLeftText = true;
    }

    public void UploadingData(bool up)
    {
        if (up == true)
            ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().text = "..דוקינ הלעמ";
        else
        {
            ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().text = "טוען מידע..";
            ContentContainer.transform.Find("LoadingData").gameObject.GetComponent<TMP_Text>().isRightToLeftText = true;
        }
    }

}
