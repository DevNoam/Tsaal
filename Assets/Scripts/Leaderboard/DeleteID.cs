using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeleteID : MonoBehaviour
{
    private string encrpytUploadKey = "FuckHamas";
    public string delScript = "http://moominrewritten.000webhostapp.com/TsaalPHP/delID.php?";
    public string tablesList = "infinitelevel";

    public void RequestDeleteID()
    {
        StartCoroutine(DeleteIDEnum(PlayerPrefs.GetString("PlayerName"), PlayerPrefs.GetInt("openID"), tablesList));
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
