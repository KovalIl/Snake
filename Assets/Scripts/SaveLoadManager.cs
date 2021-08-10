using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public enum SaveType
{
    Money,
    Score,
    CurrentSnake,
    Buy
}

public class SaveLoadManager : Singleton<SaveLoadManager>
{
    private Save _currentSave;
    string filePath;

    private void Awake()
    {
        base.Awake();
        filePath = Application.persistentDataPath + "/save.snakesave";
    }

    public Save CurrentSave
    {
        get
        {
            if (_currentSave == null)
            {
                _currentSave = LoadGame();
            }
            return _currentSave;
        }
    }

    private Save LoadGame()
    {
        Debug.Log(filePath);
        if (!File.Exists(filePath))
        {
            Debug.Log("!File.Exists");
            return new Save();    
        }

        Debug.Log("File.Exists");
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Open);
        Save save = (Save)binaryFormatter.Deserialize(fileStream);
        fileStream.Close();
        return save;
    }

    public void SaveGame(int value, SaveType saveType)
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fileStream = new FileStream(filePath, FileMode.Create);
        if(saveType == SaveType.Money)
        {
            CurrentSave.Money = value;
        }
        else if(saveType == SaveType.CurrentSnake)
        {
            CurrentSave.CurrentSnakeIndex = value;
        }
        else if(saveType == SaveType.Buy)
        {
            CurrentSave.SnakeBought.Add(value);
        }
        else
        {
            CurrentSave.BestScore = value;
        }
        binaryFormatter.Serialize(fileStream, CurrentSave);
        fileStream.Close();
    }
}

[System.Serializable]
public class Save
{
    public int Money;
    public int BestScore;
    public int CurrentSnakeIndex;
    public List<int> SnakeBought = new List<int>();
}