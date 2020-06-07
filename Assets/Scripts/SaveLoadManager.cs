using UnityEngine;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    static List<Upgradable> upgradables = new List<Upgradable>();
    static Tapper theTapper;
    public static void AddAnUpgradable(Upgradable item)
    {
        if (!upgradables.Contains(item))
        {
            upgradables.Add(item);
        }
        if (item is Tapper)
        {
            theTapper = item as Tapper;
        }
    }
    private void OnEnable()
    {
        EventManager.StartListening("Gameplay Scene Loaded", Load);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Gameplay Scene Loaded", Load);
    }
#if UNITY_EDITOR
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Save();
        }
    }

#else
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            Save();
        }
    }
#endif
    void Save()
    {
        PlayerPrefs.SetString("VerticiesBalance", PlayerDataManager.GetVerticies().ToString());
        PlayerPrefs.SetInt("PrestigeLevel", PlayerDataManager.prestigeLevel);
        for (int i = 0; i < upgradables.Count; i++)
        {
            PlayerPrefs.SetInt("Upgradable" + i, upgradables[i].currentLevel); //first upgradable is always the tapper
        }
        DateTimeManager.INSTANCE.SaveTheDateNow();
        //Save Current Verticies and if applicable current world
    }
    void Load()
    {
        if (PlayerPrefs.HasKey("VerticiesBalance"))
        {
            PlayerDataManager.prestigeLevel = PlayerPrefs.GetInt("PrestigeLevel");
            double verticies = 0;
            double.TryParse(PlayerPrefs.GetString("VerticiesBalance"), out verticies);
            PlayerDataManager.AddVerticies(verticies);
            for (int i = 0; i < upgradables.Count; i++)
            {
                upgradables[i].currentLevel = PlayerPrefs.GetInt("Upgradable" + i, 0);
                upgradables[i].CalculateValues();
            }
        }
        else
        {
            theTapper.Init();
        }
        IdleGainManager.CalculateIdleGainPerSecond(upgradables);
        EventManager.TriggerEvent("Data Loaded");
    }
}
