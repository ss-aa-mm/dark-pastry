using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


[Serializable]
public struct LevelInformation
{
    public int number;
    public bool globeEnabled;
    public string[] ingredients;
    public string[] hints;
}

[Serializable]
public struct GameInformation
{
    public LevelInformation[] levels;
}

public class LevelData : MonoBehaviour
{
    private static GameInformation _gameData;
    private static LevelInformation _levelData;
    private static List<string>.Enumerator _hintList;
    private static int _currentLevel;

    public static void DataLoad()
    {
        _gameData = JsonUtility.FromJson<GameInformation>(Resources.Load<TextAsset>("data").text);
        foreach (var level in _gameData.levels)
        {
            if (level.number == _currentLevel)
                _levelData = level;
        }

        _hintList = _levelData.hints.ToList().GetEnumerator();
    }

    public static LevelInformation GetInfo()
    {
        return _levelData;
    }

    public static void LevelCompleted()
    {
        _currentLevel++;
        SceneManager.LoadScene("Level" + _currentLevel);
    }

    public static string AssignHint()
    {
        _hintList.MoveNext();
        return _hintList.Current;
    }
    
}
