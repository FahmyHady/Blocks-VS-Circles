using UnityEngine;
using System.Collections.Generic;

public class SaveLoadManager : MonoBehaviour
{
    static List<Upgradable> upgradables = new List<Upgradable>();
    public static void AddAnUpgradable(Upgradable item)
    {
        if (!upgradables.Contains(item))
        {
            upgradables.Add(item);
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

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Save();
        }
    }

    void Save()
    {
        PlayerPrefs.SetString("VerticiesBalance", PlayerDataManager.GetVerticies().ToString());
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
            double verticies = 0;
            double.TryParse(PlayerPrefs.GetString("VerticiesBalance"), out verticies);
            PlayerDataManager.AddVerticies(verticies);
            for (int i = 0; i < upgradables.Count; i++)
            {
                upgradables[i].currentLevel = PlayerPrefs.GetInt("Upgradable" + i, 0);
                upgradables[i].CalculateValues();
            }
        }
        IdleGainManager.CalculateIdleGainPerSecond(upgradables);
        EventManager.TriggerEvent("Data Loaded");
    }
}
