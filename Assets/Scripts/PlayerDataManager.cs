using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataManager : MonoBehaviour
{
    static double verticies;
    static public int prestigeLevel;
    public static void AddVerticies(double countToAdd)
    {
        verticies += countToAdd;
        EventManager.TriggerEvent("Verticies Updated", verticies);
    }
    public static double GetVerticies()
    {
        return verticies;
    }
}
