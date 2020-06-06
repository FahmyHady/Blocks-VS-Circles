using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using System.Linq;

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent<bool>> eventDictionaryBool;
    private Dictionary<string, UnityEvent<double>> eventDictionaryDouble;
    private Dictionary<string, UnityEvent<string>> eventDictionaryString;
    private Dictionary<string, UnityEvent> eventDictionaryNull;


    private static EventManager eventManager;

    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionaryBool == null)
        {
            eventDictionaryBool = new Dictionary<string, UnityEvent<bool>>();
        }
        if (eventDictionaryDouble == null)
        {
            eventDictionaryDouble = new Dictionary<string, UnityEvent<double>>();
        }   
        if (eventDictionaryString == null)
        {
            eventDictionaryString = new Dictionary<string, UnityEvent<string>>();
        }
        if (eventDictionaryNull == null)
        {
            eventDictionaryNull = new Dictionary<string, UnityEvent>();
        }
    }


    #region EventBool
    [System.Serializable]
    public class BoolEvent : UnityEvent<bool>
    {

    }

    public static void StartListening(string eventName, UnityAction<bool> listener)
    {
        UnityEvent<bool> thisEvent = null;
        if (instance.eventDictionaryBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new BoolEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryBool.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<bool> listener)
    {
        if (eventManager == null) return;
        UnityEvent<bool> thisEvent = null;
        if (instance.eventDictionaryBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, bool toggle)
    {
        UnityEvent<bool> thisEvent = null;
        if (instance.eventDictionaryBool.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion  
    #region EventDouble
    [System.Serializable]
    public class DoubleEvent : UnityEvent<double>
    {

    }

    public static void StartListening(string eventName, UnityAction<double> listener)
    {
        UnityEvent<double> thisEvent = null;
        if (instance.eventDictionaryDouble.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new DoubleEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryDouble.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<double> listener)
    {
        if (eventManager == null) return;
        UnityEvent<double> thisEvent = null;
        if (instance.eventDictionaryDouble.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, double toggle)
    {
        UnityEvent<double> thisEvent = null;
        if (instance.eventDictionaryDouble.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion  
    #region EventString
    [System.Serializable]
    public class StringEvent : UnityEvent<string>
    {

    }

    public static void StartListening(string eventName, UnityAction<string> listener)
    {
        UnityEvent<string> thisEvent = null;
        if (instance.eventDictionaryString.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new StringEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryString.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction<string> listener)
    {
        if (eventManager == null) return;
        UnityEvent<string> thisEvent = null;
        if (instance.eventDictionaryString.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName, string toggle)
    {
        UnityEvent<string> thisEvent = null;
        if (instance.eventDictionaryString.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(toggle);
        }
    }
    #endregion

    #region EventNone
    public static void StartListening(string eventName, UnityAction listener)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionaryNull.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionaryNull.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, UnityAction listener)
    {
        if (eventManager == null) return;
        UnityEvent thisEvent = null;
        if (instance.eventDictionaryNull.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    public static void TriggerEvent(string eventName)
    {
        UnityEvent thisEvent = null;
        if (instance.eventDictionaryNull.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke();
        }
 
    }
    #endregion
}