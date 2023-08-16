using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Ebac.Core.Singleton;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";
    public int lastLevel;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 2;
        _saveSetup.playerName = "Mateus";
    }

    #region SAVE
    private void Save()
    {
        string setupToJson = JsonUtility.ToJson(_saveSetup);
        SaveFile(setupToJson);
    }

    private void SaveItems()
    {
        _saveSetup.coins = ItemManager.Instance.GetItemByType(ItemType.COIN).soInt.value;
        _saveSetup.lifepacks = ItemManager.Instance.GetItemByType(ItemType.LIFE_PACK).soInt.value;
    }

    private void SaveName(string name)
    {
        _saveSetup.playerName = name;
        Save();
    }

    public void SaveLastLevel(int level)
    {
        _saveSetup.lastLevel = level;
        SaveItems();
        Save();
    }

    private void SaveFile(string json)
    {
        File.WriteAllText(_path, json);
    }
    #endregion

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path)) fileLoaded = File.ReadAllText(_path);

        _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
        lastLevel = _saveSetup.lastLevel;
    }
}

public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public int coins;
    public int lifepacks;
}
