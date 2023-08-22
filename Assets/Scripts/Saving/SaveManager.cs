using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Ebac.Core.Singleton;
using System;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private SaveSetup _saveSetup;
    private string _path = Application.streamingAssetsPath + "/save.txt";
    public int lastLevel;

    public Action<SaveSetup> FileLoaded;

    public SaveSetup setup
    {
        get { return _saveSetup; }
    }

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Invoke(nameof(Load), .1f);
    }

    private void CreateNewSave()
    {
        _saveSetup = new SaveSetup();
        _saveSetup.lastLevel = 0;
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

    public void SaveLastLevelButton()
    {
        SaveLastLevel(lastLevel);
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

    public void SaveCheckpoint(int key)
    {
        _saveSetup.checkpoint = key;
    }
    #endregion

    [NaughtyAttributes.Button]
    private void Load()
    {
        string fileLoaded = "";

        if (File.Exists(_path))
        {
            fileLoaded = File.ReadAllText(_path);
            _saveSetup = JsonUtility.FromJson<SaveSetup>(fileLoaded);
            lastLevel = _saveSetup.lastLevel;
        }
        else
        {
            CreateNewSave();
            Save();
        }
        FileLoaded.Invoke(_saveSetup);
    }
}

public class SaveSetup
{
    public int lastLevel;
    public string playerName;
    public int coins;
    public int lifepacks;
    public int checkpoint = -1;
}
