using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;

public class IdleGainManager : MonoBehaviour
{
    static double totalGainPerSecond;
    double gainSinceLastSession;
    private void OnEnable()
    {
        EventManager.StartListening("Data Loaded", IntializeIdleGain);
    }
    private void OnDisable()
    {
        EventManager.StopListening("Data Loaded", IntializeIdleGain);
    }

    private void IntializeIdleGain()
    {
        TimeSpan timeSinceLastSession = DateTimeManager.INSTANCE.GetElapsedTime();
        gainSinceLastSession = timeSinceLastSession.TotalSeconds * totalGainPerSecond;
        if (timeSinceLastSession.TotalMinutes < 1 || gainSinceLastSession == 0)
        {
            EventManager.TriggerEvent("No Idle Gain");
            return;
        }
        EventManager.TriggerEvent("Idle Gain Verticies", AbbrevationUtility.AbbreviateNumber(gainSinceLastSession));
        EventManager.TriggerEvent("Idle Gain Time", timeSinceLastSession.Days + " Days " + timeSinceLastSession.Hours + " Hours " + timeSinceLastSession.Minutes + " Minutes");
    }
    public void CollectIdleVerticies()
    {
        //playParticle
        PlayerDataManager.AddVerticies(gainSinceLastSession);
    }
    public static void CalculateIdleGainPerSecond(List<Upgradable> castToCirlces)
    {
        totalGainPerSecond = 0;
        for (int i = 0; i < castToCirlces.Count; i++)
        {
            if (castToCirlces[i] is Circle)
            {
                totalGainPerSecond += castToCirlces[i].goldPerTap;
            }
        }
    }
}
