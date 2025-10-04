using System;
using System.Collections.Generic;

using UnityEngine;

public class ObserverManager : MonoBehaviour
{

    // void Awake()
    // {
    //     Listeners.Clear();

    // }

    public static Dictionary<string, List<Delegate>> Listeners = new Dictionary<string, List<Delegate>>();
    public static void AddListener(string name, Action callback) // ko có tham số truyền vào 
    {
        if (!Listeners.ContainsKey(name))
        {
            Listeners.Add(name, new List<Delegate>());
            Listeners[name].Add(callback);
        }
        else if (Listeners.ContainsKey(name))
        {
            Listeners[name].Add(callback);
        }
    }
    public static void AddListener<T>(string name, Action<T> callback) // có tham số truyền vào 
    {
        if (!Listeners.ContainsKey(name))
        {
            Listeners.Add(name, new List<Delegate>());
            Listeners[name].Add(callback);
        }
        else if (Listeners.ContainsKey(name))
        {
            Listeners[name].Add(callback);
        }
    }
    public static void RemoveListener(string name, Action callback)
    {
        if (!Listeners.ContainsKey(name))
            return;
        if (Listeners.ContainsKey(name))
        {
            Listeners[name].Remove(callback);
        }
        if (Listeners[name].Count == 0)
        {
            Listeners.Remove(name);

        }


    }
    public static void RemoveListener<T>(string name, Action<T> callback)
    {
        if (!Listeners.ContainsKey(name))
            return;
        if (Listeners.ContainsKey(name))
        {
            Listeners[name].Remove(callback);
        }
        if (Listeners[name].Count == 0)
        {
            Listeners.Remove(name);

        }


    }
    public static void Notify<T>(string name, T param)
    {
        if (!Listeners.ContainsKey(name)) return;

        foreach (var listener in Listeners[name])
        {
            if (listener is Action<T> action)
            {
                try
                {
                    action.Invoke(param);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Observer problem: {e} - {listener}");
                }
            }
        }
    }
    public static void Notify(string name)
    {
        if (!Listeners.ContainsKey(name))
        {
            return;
        }
        for (int i = 0; i < Listeners[name].Count; i++)
        {
            if (Listeners[name][i] is Action action)
            {
                try
                {
                    action?.Invoke();

                }
                catch (Exception e)
                {

                    Debug.LogError("Observer problem " + e + Listeners[name][i]);
                }
            }
        }
    }

}
public enum ObserverEvent
{
    ChangeDropStorage
}
