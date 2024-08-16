using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class WorkerSounds : MonoBehaviour{

    [SerializeField] private ObjectMakeStation objectMakeStation;

    //Timer Variables
    private float NormalWorkMaxTime;
    private float SlowWorkMaxTime;
    private float SleepMaxTime;
    private float WaitingMaxTime;
    private float Timer;

    private bool isSlowWorking;
    private bool isNormalWorking;
    private bool isSleeping;

    public enum Sounds {
        Working,
        SlowWorking,
        Sleeping,
        Waiting,
    }

    public class OnPlaySoundEventArgs : EventArgs { public Sounds sounds; }
    public static event EventHandler<OnPlaySoundEventArgs> OnPlaySound;


    private void Start() {
        NormalWorkMaxTime = 1.1f;
        SlowWorkMaxTime = 2.2f;
        SleepMaxTime = 3.5f;
        WaitingMaxTime = 5f;
        Timer = 0f;

        objectMakeStation.OnStateChanged += ObjectMakeStation_OnStateChanged;
    }

    private void ObjectMakeStation_OnStateChanged(object sender, ObjectMakeStation.OnStateChagedEventArgs e) {
        isNormalWorking = false;
        isSlowWorking = false;
        isSleeping = false;

        if(e.state == ObjectMakeStation.WorkingStates.Working) {
            isNormalWorking = true;
        }
        else if (e.state == ObjectMakeStation.WorkingStates.Sleeping) {
            isSleeping = true;
        }
        else if(e.state == ObjectMakeStation.WorkingStates.Slowed) {
            isSlowWorking = true;
        }
    }

    private void Update() {
        Timer += Time.deltaTime;

        if (isNormalWorking) {
            if(Timer >= NormalWorkMaxTime) {
                Timer = 0f;

                OnPlaySound?.Invoke(this, new OnPlaySoundEventArgs { sounds = Sounds.Working});
            }
        }
        else if (isSleeping) {
            if (Timer >= SleepMaxTime) {
                Timer = 0f;

                OnPlaySound?.Invoke(this, new OnPlaySoundEventArgs { sounds = Sounds.Sleeping });
            }
        }
        else if (isSlowWorking) {
            if (Timer >= SlowWorkMaxTime) {
                Timer = 0f;

                OnPlaySound?.Invoke(this, new OnPlaySoundEventArgs { sounds = Sounds.SlowWorking });
            }
        }
        else {
            if (Timer >= WaitingMaxTime) {
                Timer = 0f;

                OnPlaySound?.Invoke(this, new OnPlaySoundEventArgs { sounds = Sounds.Waiting });
            }
        }
    }
}
