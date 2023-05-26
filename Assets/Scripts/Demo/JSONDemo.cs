using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class JSONDemo : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerLevel;
    public TextMeshProUGUI playerGold;
    public ListPlayer listPlayer;
    private string json;
    private string dataPath;

    private void Awake()
    {
        dataPath = Application.persistentDataPath + "/PlayerData.json";
        Debug.Log(dataPath);
    }

    void Start()
    {
        //PlayerData playerData = new PlayerData
        //{
        //    Name = "Cat",
        //    Level = 1,
        //    Gold = 100
        //};
        //json = JsonUtility.ToJson(playerData);
        json = JsonUtility.ToJson(listPlayer);
        Debug.Log(json);
    }

    public void LoadPlayerData()
    {
        string loadJSON = File.ReadAllText(dataPath);
        if(loadJSON != null)
        {
            //PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(loadJSON);
            //playerName.text = "Player Name: " + loadedPlayerData.Name;
            //playerLevel.text = "Player Level: " + loadedPlayerData.Level;
            //playerGold.text = "Player Gold: " + loadedPlayerData.Gold;
            ListPlayer loadedPlayerData = JsonUtility.FromJson<ListPlayer>(loadJSON);
            playerName.text = "Player Name: " + loadedPlayerData.playerDatas[2].Name;
            playerLevel.text = "Player Level: " + loadedPlayerData.playerDatas[2].Level;
            playerGold.text = "Player Gold: " + loadedPlayerData.playerDatas[2].Gold;
        }
    }

    public void SavePlayerData()
    {
        File.WriteAllText(dataPath, json);
    }

    public void UpdatePlayerData()
    {
        PlayerData newData = new PlayerData
        {
            Name = "Dog",
            Level = 2,
            Gold = 1000
        };
        playerName.text = "Player Name: " + newData.Name;
        playerLevel.text = "Player Level: " + newData.Level;
        playerGold.text = "Player Gold: " + newData.Gold;
        json = JsonUtility.ToJson(newData);
        SavePlayerData();
    }
}
