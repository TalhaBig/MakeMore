using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffectSO", menuName = "ScriptableObjecs/SoundEffectSO")]
public class SoundEffectSO : ScriptableObject{
    public AudioClip[] WorkingSound;
    public AudioClip[] SlowWorkingSound;
    public AudioClip[] SleepingSound;
    public AudioClip[] WaitingSound;

    public AudioClip[] TapSound;
    
    public AudioClip[] MoneySound;
    
    public AudioClip[] ExperienceSound;
    
    public AudioClip[] ObjectMakeSound;
    public AudioClip[] LimitReachedSound;

    public AudioClip[] UpgradeSound;
}
