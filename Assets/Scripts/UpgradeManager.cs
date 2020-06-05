using System;
using System.Collections;
using System.Collections.Generic;
using Unity.RemoteConfig;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static Func<int, double> CalculateUpgradeCost;
    public static Func<int, double> CalculateGoldPerTap;
    float XgoldPerTap;
    float YgoldPerTap;
    float XupgradeCost;
    float YupgradeCost;
    void SetEquations()
    {
        CalculateGoldPerTap = (upgradeLevel) => XgoldPerTap * upgradeLevel * YgoldPerTap;
        CalculateUpgradeCost = (upgradeLevel) => XupgradeCost * (Mathf.Pow(YupgradeCost, upgradeLevel));
    }
    void SetDefaultValues()
    {
        XgoldPerTap = 5;
        YgoldPerTap = 2.1f;
        XupgradeCost = 5;
        YupgradeCost = 1.08f;
    }
    public struct userAttributes
    {
    }

    public struct appAttributes
    {
    }
    void Awake()
    {
        // Add a listener to apply settings when successfully retrieved:
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        // Set the user’s unique ID:
        ConfigManager.SetCustomUserID(SystemInfo.deviceUniqueIdentifier);
        // Set the environment ID:
        ConfigManager.SetEnvironmentID("42c96ffb-c11f-4553-a538-a9ef49573d22");
        // Fetch configuration setting from the remote service:
        ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());
    }

    void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        if (configResponse.status == ConfigRequestStatus.Failed)
        {
            SetDefaultValues();
        }
        else
        {
            // Conditionally update settings, depending on the response's origin:
            switch (configResponse.requestOrigin)
            {
                case ConfigOrigin.Default:
                    Debug.Log("No settings loaded this session; using default values.");
                    break;
                case ConfigOrigin.Cached:
                    Debug.Log("No settings loaded this session; using cached values from a previous session.");
                    break;
                case ConfigOrigin.Remote:
                    Debug.Log("New settings loaded this session; update values accordingly.");
                    XgoldPerTap = ConfigManager.appConfig.GetFloat("X - goldPerTap");
                    YgoldPerTap = ConfigManager.appConfig.GetFloat("Y - goldPerTap");
                    XupgradeCost = ConfigManager.appConfig.GetFloat("X - upgradeCost");
                    YupgradeCost = ConfigManager.appConfig.GetFloat("Y - upgradeCost");
                    break;
            }
        }
        SetEquations();
        EventManager.TriggerEvent("Finished Fetching Equations");
    }

}
