using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeControl : MonoBehaviour
{
    public GameObject[] cooker;
    public SpawnPeople spawnPeople;
    public OrderingNumber[] dishPrice;
    public DesktopMovement player;

    public List<int> cookerUpgrade;
    public List<int> customerUpgrade;
    public List<int> incomeUpgrade;
    public List<int> speedUpgrade;

    public List<int> cookerUpgradeInUse = new();
    public List<int> customerUpgradeInUse = new();
    public List<int> incomeUpgradeInUse = new();
    public List<int> speedUpgradeInUse = new();  

    public Text cookerUpgradeText;
    public Text customerUpgradeText;
    public Text incomeUpgradeText;
    public Text speedUpgradeText;

    public GameObject[] starMeter;

    public DataManager data;

    public void TransferData()
    {
        cookerUpgradeInUse = data.gameModel.models[0].cookerUpgrade;
        customerUpgradeInUse = data.gameModel.models[0].customerUpgrade;
        incomeUpgradeInUse = data.gameModel.models[0].incomeUpgrade;
        speedUpgradeInUse = data.gameModel.models[0].speedUpgrade;
        if (cookerUpgradeInUse.Count < 4)
        {
            for (int i = 0; i < cookerUpgradeInUse.Count; i++)
            {
                cooker[i].GetComponent<CookedFood>().cookedTime -= 4 - cookerUpgradeInUse.Count;
            }
        }
        if (customerUpgradeInUse.Count < 4)
        {
            spawnPeople.CustAmount += 2 * (4 - customerUpgradeInUse.Count);
        }
        if (incomeUpgradeInUse.Count < 4)
        {
            for (int i = 0; i < dishPrice.Length; i++)
            {
                dishPrice[i].price += 50 * (4 - incomeUpgradeInUse.Count);
            }
        }
        if (speedUpgradeInUse.Count < 4)
        {
            player.speed += 0.2f * (4 - speedUpgradeInUse.Count);
        }
    }

    private void Start()
    {
        if (cookerUpgradeInUse.Count == 0) cookerUpgradeText.text = "MAX";
        else cookerUpgradeText.text = cookerUpgradeInUse[0].ToString() + "$";
        if (customerUpgradeInUse.Count == 0) customerUpgradeText.text = "MAX";
        else customerUpgradeText.text = customerUpgradeInUse[0].ToString() + "$";
        if (incomeUpgradeInUse.Count == 0) incomeUpgradeText.text = "MAX";
        else incomeUpgradeText.text = incomeUpgradeInUse[0].ToString() + "$";
        if (speedUpgradeInUse.Count == 0) speedUpgradeText.text = "MAX";
        else speedUpgradeText.text = speedUpgradeInUse[0].ToString() + "$";
    }

    public void UpgradeCooker()
    {
        if (MoneyControl.money < cookerUpgradeInUse[0])
            return;
        MoneyControl.money -= cookerUpgradeInUse[0];
        for (int i = 0; i < cooker.Length; i++)
        {
            cooker[i].GetComponent<CookedFood>().cookedTime -= 1;
        }
        cookerUpgradeInUse.Remove(cookerUpgradeInUse[0]);
        if (cookerUpgradeInUse.Count > 0)
            cookerUpgradeText.text = cookerUpgradeInUse[0].ToString() + "$";
        else cookerUpgradeText.text = "MAX";
    }

    public void UpgradeCustomer()
    {
        if (MoneyControl.money < customerUpgradeInUse[0])
            return;
        MoneyControl.money -= customerUpgradeInUse[0];
        spawnPeople.CustAmount += 2;
        customerUpgradeInUse.Remove(customerUpgradeInUse[0]);
        if (customerUpgradeInUse.Count > 0)
            customerUpgradeText.text = customerUpgradeInUse[0].ToString() + "$";
        else cookerUpgradeText.text = "MAX";
        spawnPeople.SpawnCustomer();
    }

    public void UpgradeIncome()
    {
        if (MoneyControl.money < incomeUpgradeInUse[0])
            return;
        for (int i = 0; i < dishPrice.Length; i++)
        {
            dishPrice[i].price += 50;
        }
        MoneyControl.money -= incomeUpgradeInUse[0];
        incomeUpgradeInUse.Remove(incomeUpgradeInUse[0]);
        if (incomeUpgradeInUse.Count > 0)
            incomeUpgradeText.text = incomeUpgradeInUse[0].ToString() + "$";
        else incomeUpgradeText.text = "MAX";
    }

    public void UpgradeSpeed()
    {
        if (MoneyControl.money < speedUpgradeInUse[0])
            return;
        MoneyControl.money -= speedUpgradeInUse[0];
        player.speed += 0.2f;
        speedUpgradeInUse.Remove(speedUpgradeInUse[0]);
        if (speedUpgradeInUse.Count > 0)
            speedUpgradeText.text = speedUpgradeInUse[0].ToString() + "$";
        else speedUpgradeText.text = "MAX";
    }

    public void Close()
    {
        player.moneyText.color = Color.white;
        player.upgradeIsOn = false;
        Cursor.lockState = CursorLockMode.Locked;
        data.SaveData();
        for (int i = 0; i < starMeter.Length; i++)
            starMeter[i].SetActive(true);
        gameObject.SetActive(false);
    }
}
