using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DateTimeManager : MonoBehaviour
{
    public static DateTimeManager INSTANCE;
    private DateTime _currentDate;

    #region MonoBehavior Callbacks
    private void Awake()
    {
        if (INSTANCE != null)
            Destroy(gameObject);
        else
            INSTANCE = this;
    }
    #endregion

    public TimeSpan GetElapsedTime()
    {
        DateTime oldDate;
        TimeSpan returnValue;

        _currentDate = DateTime.Now;

        if (!long.TryParse(PlayerPrefs.GetString("LastPlayTime"), out long temp))
            returnValue = TimeSpan.MinValue;

        else
        {
            oldDate = DateTime.FromBinary(temp);
            returnValue = _currentDate.Subtract(oldDate);
        }

        return returnValue;
    }

    [ContextMenu("Start Counting")]
    public void SaveTheDateNow()
    {
        string saveString = DateTime.Now.ToBinary().ToString();
        PlayerPrefs.SetString("LastPlayTime", saveString);
    }

    [ContextMenu("Clear PlayerPRefs Key")]
    private void ClearKey()
    {
        PlayerPrefs.DeleteKey("LastPlayTime");
    }
}
