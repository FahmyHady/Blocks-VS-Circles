using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgradable : MonoBehaviour
{
    public Text myPriceText;
    [HideInInspector] public int currentLevel;
    public double upgradeCost;
    public double goldPerTap;
    public void OnEnable()
    {
        SaveLoadManager.AddAnUpgradable(this);
    }
    public void OnDisable()
    {
      //  EventManager.StopListening("Data Loaded", CalculateValues);
    }
    public void CalculateValues()
    {
        upgradeCost = UpgradeManager.CalculateUpgradeCost(currentLevel);
        goldPerTap = UpgradeManager.CalculateGoldPerTap(currentLevel);
        UpdatePriceUI(upgradeCost);

    }
    public virtual void PlayerPrestiged()
    {
    }
    public void Upgrade()
    {
        if (PlayerDataManager.GetVerticies() >= upgradeCost)
        {
            PlayerDataManager.AddVerticies(-upgradeCost);
            currentLevel++;
            CalculateValues();
            UpdatePriceUI(upgradeCost);
        }
        else
        {
            //play sound
        }
    }
    public void UpdatePriceUI(double price)
    {
        myPriceText.text = AbbrevationUtility.AbbreviateNumber(price);
    }
}
