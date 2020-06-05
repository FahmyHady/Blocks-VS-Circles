using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Upgradable : MonoBehaviour
{
    [HideInInspector] public int currentLevel;
    protected double upgradeCost;
    protected double goldPerTap;
    public void OnEnable()
    {
        EventManager.StartListening("Data Loaded", CalculateValues);
        SaveLoadManager.AddAnUpgradable(this);
    }
    public void OnDisable()
    {
        EventManager.StopListening("Data Loaded", CalculateValues);
    }
    void CalculateValues()
    {
        upgradeCost = UpgradeManager.CalculateUpgradeCost(currentLevel);
        goldPerTap = UpgradeManager.CalculateGoldPerTap(currentLevel);
    }
    public void Upgrade()
    {
        if (PlayerDataManager.GetVerticies() >= upgradeCost)
        {
            PlayerDataManager.AddVerticies(-upgradeCost);
            currentLevel++;
            CalculateValues();
        }
        else
        {
            //play sound
        }
    }
}
