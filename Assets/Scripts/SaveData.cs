using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public SaveUserData saveUserData;

    private void Awake()
    {
        LoadUserData();
        DontDestroyOnLoad(this);
    }

    [ContextMenu("To Json Data")]
    public void SaveUserData()
    {
        string jsonData = JsonUtility.ToJson(saveUserData, true);
        string path = Path.Combine(Application.dataPath, "UserData.json");
        File.WriteAllText(path, jsonData);
        Debug.Log("데이터 저장했음");
    }

    [ContextMenu("From Json Data")]
    public void LoadUserData()
    {
        string path = Path.Combine(Application.dataPath, "UserData.json");
        if (!File.Exists(path))
        {
            Debug.Log("No File");
            saveUserData = new SaveUserData();
            return;
        }
        string jsonData = File.ReadAllText(path);

        saveUserData = JsonUtility.FromJson<SaveUserData>(jsonData);
        Debug.Log("데이터 불러왔음");
    }

    private void OnApplicationQuit()
    {
        SaveUserData();
    }

}

[System.Serializable]
public class SaveUserData
{
    public List<UserData> user = new List<UserData>();
}

[System.Serializable]
public class UserData
{
    public string id;
    public string password;
    public string name;
    public int balance;
}
