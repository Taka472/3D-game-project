using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementControl : MonoBehaviour
{
    public List<int> uncompleteAchievement = new();
    public List<int> completeAchievement = new();

    public int customerServe;
    public GameObject achievementList;

    private void Start()
    {
        uncompleteAchievement.Clear();
        completeAchievement.Clear();
        uncompleteAchievement = DataManager.instance.model.models[0].uncomplete;
        completeAchievement = DataManager.instance.model.models[0].complete;
    }

    private void Update()
    {
        if (customerServe == 5 && !completeAchievement.Contains(3))
            Complete(1);
        if (MoneyControl.money >= 1000 && !completeAchievement.Contains(1))
            Complete(2);
    }

    public void Complete(int completeID)
    {
        achievementList.transform.GetChild(completeID - 1).GetComponent<AchievementChecked>().Checking();
        completeAchievement.Add(completeID);
        uncompleteAchievement.Remove(completeID);
        DataManager.instance.SaveData();
    }
}
