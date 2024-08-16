using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour{
    //string for gettnig the value
    private const string MusicValueString = "MusicValue";

    private AudioSource audioSource;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = PlayerPrefs.GetFloat(MusicValueString,1f);
    }

    private void Start() {
        AudioSettingsUI.OnMusicValueChanged += AudioSettingsUI_OnMusicValueChanged;
    }

    private void AudioSettingsUI_OnMusicValueChanged(object sender, AudioSettingsUI.OnSoundValueChangedEventArgs e) {
        audioSource.volume = e.value;

        PlayerPrefs.SetFloat(MusicValueString,e.value);
        PlayerPrefs.Save();
    }

    private void OnDestroy() {
        AudioSettingsUI.OnMusicValueChanged -= AudioSettingsUI_OnMusicValueChanged;
    }
}
