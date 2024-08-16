using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour{
    //Singleton
    public static SoundManager Instance {  get; private set; }

    //reference of sounds 
    [SerializeField] private SoundEffectSO sounds;

    private const string SFXValueString = "SFXValue";

    private float volume;

    private void Awake() {
        Instance = this;

        //Get volume from Player Prefs
        volume = PlayerPrefs.GetFloat(SFXValueString,1f);
    }

    private void Start() {
        AudioSettingsUI.OnSFXValueChanged += AudioSettingsUI_OnSFXValueChanged;

        WorkerSounds.OnPlaySound += WorkerSounds_OnPlaySound;
        HandAnimation.OnTapSoundPlay += HandAnimation_OnTapSoundPlay;

        NormalMoney.Instance.OnMoneyChanged += Normal_OnMoneyChanged;
        CashMoney.Instance.OnCashChanged += CashMoney_OnCashChanged;

        TradeStation.Instance.OnObjectUpgraded += TradeStation_OnObjectUpgraded;

        ObjectMakeStation.OnAnyObjectMade += ObjectMakeStation_OnAnyObjectMade;
        ObjectMakeStation.OnAnyLimitReached += ObjectMakeStation_OnAnyLimitReached;
    }

    private void ObjectMakeStation_OnAnyLimitReached(object sender, System.EventArgs e) {
        ObjectMakeStation makeStation = sender as ObjectMakeStation;

        PlaySound(sounds.LimitReachedSound,makeStation.transform.position);
    }

    private void ObjectMakeStation_OnAnyObjectMade(object sender, System.EventArgs e) {
        ObjectMakeStation makeStation = sender as ObjectMakeStation;

        PlaySound(sounds.ObjectMakeSound,makeStation.transform.position);
    }

    private void TradeStation_OnObjectUpgraded(object sender, TradeStation.OnObjectUpgradedEventArgs e) {
        PlaySound(sounds.ExperienceSound,TradeStation.Instance.transform.position);
    }

    private void CashMoney_OnCashChanged(object sender, CashMoney.OnCashChangedEventArgs e) {
        PlaySound(sounds.MoneySound,CashMoney.Instance.transform.position);
    }

    private void Normal_OnMoneyChanged(object sender, NormalMoney.OnMoneyChangedEventArgs e) {
        PlaySound(sounds.MoneySound,NormalMoney.Instance.transform.position);
    }

    private void HandAnimation_OnTapSoundPlay(object sender, System.EventArgs e) {
        HandAnimation hand = sender as HandAnimation;

        PlaySound(sounds.TapSound, hand.transform.position);
    }

    private void WorkerSounds_OnPlaySound(object sender, WorkerSounds.OnPlaySoundEventArgs e) {
        WorkerSounds worker = sender as WorkerSounds;

        if(e.sounds == WorkerSounds.Sounds.Working) {
            PlaySound(sounds.WorkingSound,worker.transform.position);
        }
        else if(e.sounds == WorkerSounds.Sounds.Sleeping) {
            PlaySound(sounds.SleepingSound, worker.transform.position);
        }
        else if(e.sounds == WorkerSounds.Sounds.SlowWorking) {
            PlaySound(sounds.SlowWorkingSound, worker.transform.position);
        }
        else {
            PlaySound(sounds.WaitingSound, worker.transform.position);
        }
    }

    private void AudioSettingsUI_OnSFXValueChanged(object sender, AudioSettingsUI.OnSoundValueChangedEventArgs e) {
        volume = e.value;

        PlayerPrefs.SetFloat(SFXValueString, volume);
        PlayerPrefs.Save();
    }

    private void OnDestroy() {
        AudioSettingsUI.OnSFXValueChanged -= AudioSettingsUI_OnSFXValueChanged;

        WorkerSounds.OnPlaySound -= WorkerSounds_OnPlaySound;
        HandAnimation.OnTapSoundPlay -= HandAnimation_OnTapSoundPlay;

        NormalMoney.Instance.OnMoneyChanged -= Normal_OnMoneyChanged;
        CashMoney.Instance.OnCashChanged -= CashMoney_OnCashChanged;

        TradeStation.Instance.OnObjectUpgraded -= TradeStation_OnObjectUpgraded;

        ObjectMakeStation.OnAnyObjectMade -= ObjectMakeStation_OnAnyObjectMade;
        ObjectMakeStation.OnAnyLimitReached -= ObjectMakeStation_OnAnyLimitReached;
    }

    private void PlaySound(AudioClip[] audios,Vector3 position) {
        PlaySound(audios[Random.Range(0,audios.Length)],position);
    }
    private void PlaySound(AudioClip audio, Vector3 position) {
        AudioSource.PlayClipAtPoint(audio,position,volume);
    }

    public void PlayUpgradeSound() {
        PlaySound(sounds.UpgradeSound,Vector3.zero);
    }

}
