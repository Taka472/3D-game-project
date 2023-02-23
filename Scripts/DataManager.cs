using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using UnityEngine;
using AssemblyCSharp;

public class DataManager : MonoBehaviour
{
    public PlayerModel model;
    public InGameModel gameModel;
    public static DataManager instance;
    public UpgradeControl upgradeControl;
    public AchievementControl achievementControl;

    public static bool startNew = true;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        model = new(1);
        gameModel = new(1);
        LoadData();
        if (startNew)
            CreateNew();
    }

    public void CreateDemoData()
    {
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "Data.txt"), PlayerModel.GetJsonFromModel(model, true));
        List<int> uncompleteDemo = new();
        for (int i = 0; i < 3; i++)
            uncompleteDemo.Add(i + 1);
        achievementControl.uncompleteAchievement = uncompleteDemo;
    }

    public void CreateNew()
    {
        File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "InGameData.txt"), InGameModel.GetJsonFromModel(gameModel, true));
        gameModel.models[0].money = 0;
        gameModel.models[0].starMeter = 3;
        gameModel.models[0].cookerUpgrade = upgradeControl.cookerUpgrade;
        gameModel.models[0].customerUpgrade = upgradeControl.customerUpgrade;
        gameModel.models[0].incomeUpgrade = upgradeControl.incomeUpgrade;
        gameModel.models[0].speedUpgrade = upgradeControl.speedUpgrade;
        StarControl.currentMeter = 3;
        MoneyControl.money = 0;
        upgradeControl.TransferData();
        SaveData();
    }

    public void LoadData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Data.txt");
        string data = File.ReadAllText(path);
        string inGamePath = Path.Combine(Application.streamingAssetsPath, "InGameData.txt");
        string inGameData = File.ReadAllText(inGamePath);
        PlayerModel.Model[] models = PlayerModel.GetModelFromJson(data);
        InGameModel.Model[] inGameModels = InGameModel.GetModelFromJson(inGameData);
        model.models[0] = models[0];
        gameModel.models[0] = inGameModels[0];
        upgradeControl.TransferData();
    }

    public void SaveData()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Data.txt");
        string InGamePath = Path.Combine(Application.streamingAssetsPath, "InGameData.txt");
        model.models[0].uncomplete = achievementControl.uncompleteAchievement;
        model.models[0].complete = achievementControl.completeAchievement;
        gameModel.models[0].cookerUpgrade = upgradeControl.cookerUpgradeInUse;
        gameModel.models[0].customerUpgrade = upgradeControl.customerUpgradeInUse;
        gameModel.models[0].incomeUpgrade = upgradeControl.incomeUpgradeInUse;
        gameModel.models[0].speedUpgrade = upgradeControl.speedUpgradeInUse;
        gameModel.models[0].starMeter = StarControl.currentMeter;
        gameModel.models[0].money = MoneyControl.money;
        string data = PlayerModel.GetJsonFromModel(model, true);
        string InGameData = InGameModel.GetJsonFromModel(gameModel, true);
        File.WriteAllText(path, data);
        File.WriteAllText(InGamePath, InGameData);
    }
}
