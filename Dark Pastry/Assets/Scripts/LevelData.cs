using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Collections;
using UnityEngine;


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

    public static void DataLoad()
    {
        _gameData = JsonUtility.FromJson<GameInformation>(Resources.Load<TextAsset>("data").text);
        foreach (var level in _gameData.levels)
        {
            if (level.number == 0)
                _levelData = level;
        }

        _hintList = _levelData.hints.ToList().GetEnumerator();
    }

    public static LevelInformation GetInfo()
    {
        return _levelData;
    }

    public static string AssignHint()
    {
        _hintList.MoveNext();
        return _hintList.Current;
    }
    
}
