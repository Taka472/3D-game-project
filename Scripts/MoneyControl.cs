using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyControl : MonoBehaviour
{
    public Text moneyText;
    public static int money;

    private void Start()
    {
        money = DataManager.instance.gameModel.models[0].money;
    }

    private void Update()
    {
        moneyText.text = money.ToString();
    }
}
