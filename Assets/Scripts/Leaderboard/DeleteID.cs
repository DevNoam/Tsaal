using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeleteID : MonoBehaviour
{
    private string encrpytUploadKey = "FuckHamas";
    public string delScript = "http://localhost/TsaalDB/delID.php?";
    public string[] tablesList = new string[] {"infinitelevel", "level1", "level2", "level3", "level5"};

    public void RequestDeleteID()
    {
        for (int i = 0; i < tablesList.Length; i++)
        {
            Debug.Log(tablesList[i].ToString());
            StartCoroutine(DeleteIDEnum(PlayerPrefs.GetString("PlayerName"), PlayerPrefs.GetInt("openID"), tablesList[i].ToString()));
        }
    }

    private IEnumerator DeleteIDEnum(string name, int openID ,string table)
    {
        UnityWebRequest www = UnityWebRequest.Get(delScript + $"table={table}&id={openID}&name={"\"" + name + "\""}&encryptKey={encrpytUploadKey}");
        yield return www.SendWebRequest();

        if (www.isNetworkError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
        }
    }
}
