using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class Timer
{
    public Action CallBack;
    public float MaxValue;
    public float CurrentValue;

    public Timer(Action callBack, float maxValue)
    {
        CallBack = callBack;
        MaxValue = maxValue;
        CurrentValue = maxValue;
        Utils.Singleton.Timers.Add(this);
    }

    public void Update(float timeDelta)
    {
        CurrentValue -= timeDelta;

        if (CurrentValue < 0)
        {
            CurrentValue += MaxValue;
            CallBack();
        }
    }
}

public class Utils : MonoBehaviour
{
    public static Utils Singleton;

    public List<Timer> Timers = new List<Timer>();


    public static bool RandomBool()
    {
        var myBool = (UnityEngine.Random.value < 0.5);
        return myBool;
    }

    public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
    }

    public static T RandomEnumValue<T>()
    {
        var v = Enum.GetValues(typeof(T));
        return (T)v.GetValue(UnityEngine.Random.Range(0, v.Length));
    }

    void Awake()
    {
        Singleton = this;
    }

    void Update()
    {
        foreach (var timer in Timers)
        {
            timer.Update(Time.deltaTime);
        }
    }


}




