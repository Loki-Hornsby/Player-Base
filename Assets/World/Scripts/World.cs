/// <summary>
/// Copyright 2022, Loki Alexander Button Hornsby (Loki Hornsby), All rights reserved.
/// Licensed under the BSD 3-Clause "New" or "Revised" License
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Temperature {
    public static float GetTemperature(Vector3 pos, float Time){
        
        return 0f;
    }
}

public static class Weather {
    public static float GetTemperature(Vector3 pos, float Time){
        
        return 0f;
    }
}

public static class Clock {
    const float offset = 10f;
    static float hours;
    static float minutes;

    public static float GetHours(){
        return hours;
    }

    public static float GetMinutes(){
        return minutes;
    }

    /// <summary>
    /// Add 1 second to the clock
    /// </summary>
    public static void Tick(float t){
        minutes += t * offset;

        if (minutes > 60f){
            minutes = 0f;
            hours++;
        }

        if (hours > 24f){
            hours = 0f;
        }
    }
}

public class World : MonoBehaviour {
    void Update(){
        //Clock.Tick(Time.deltaTime);
    }
}
