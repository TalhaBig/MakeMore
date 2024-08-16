using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class TimeSaveDelta : MonoBehaviour{
    //Singleton
    public static TimeSaveDelta Instance {  get; private set; }

    private const string TimeDeltaSaveString = "TimeToEnd";

    private DateTime LastTimeToStart;
    private float DeltaTime;

    private void Awake() {
        Instance = this;

        
        string saveTime = PlayerPrefs.GetString(TimeDeltaSaveString,DateTime.Now.ToString());
        LastTimeToStart = DateTime.Parse(saveTime);

        DeltaTime = (float)(DateTime.Now - LastTimeToStart).TotalSeconds;
    }

    public float GetDeltaTime() {
        return DeltaTime;
    }

    private void OnDestroy() {
        PlayerPrefs.SetString(TimeDeltaSaveString,DateTime.Now.ToString());
        PlayerPrefs.Save();
    }

}
