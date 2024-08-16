using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class AudioSettingsUI : MonoBehaviour {
    private const string MusicValueString = "MusicValue";
    private const string SFXValueString = "SFXValue";

    [SerializeField] private Slider MusicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Button ResetButton;

    [SerializeField] private TextMeshProUGUI MusicPercent;
    [SerializeField] private TextMeshProUGUI SFXPercent;

    public class OnSoundValueChangedEventArgs: EventArgs { public float value; }
    public static event EventHandler<OnSoundValueChangedEventArgs> OnMusicValueChanged;
    public static event EventHandler<OnSoundValueChangedEventArgs> OnSFXValueChanged;

    private void Awake() {
        MusicSlider.value = PlayerPrefs.GetFloat(MusicValueString,1f);
        SFXSlider.value = PlayerPrefs.GetFloat(SFXValueString,1f);

        MusicSlider.onValueChanged.AddListener(newValue => {
            MusicPercent.text = (Mathf.FloorToInt(newValue * 100f)).ToString() + "%";
            OnMusicValueChanged?.Invoke(this, new OnSoundValueChangedEventArgs { value = newValue});
        });
        SFXSlider.onValueChanged.AddListener(newValue => {
            SFXPercent.text = (Mathf.FloorToInt(newValue * 100f)).ToString() + "%";
            OnSFXValueChanged?.Invoke(this, new OnSoundValueChangedEventArgs { value = newValue });
        });
        ResetButton.onClick.AddListener(() => {
            MusicPercent.text = "100%";
            OnMusicValueChanged?.Invoke(this, new OnSoundValueChangedEventArgs { value = 1f });
            SFXPercent.text = "100%";
            OnSFXValueChanged?.Invoke(this, new OnSoundValueChangedEventArgs { value = 1f });

            MusicSlider.value = 1;
            SFXSlider.value = 1;
        });
    }

    private void Start() {
        Hide();

        MusicPercent.text = (Mathf.FloorToInt((float)MusicSlider.value * 100f)).ToString() + "%";
        SFXPercent.text = (Mathf.FloorToInt((float)SFXSlider.value * 100f)).ToString() + "%";

        SettingsUI.Instance.OnAudioSettingsButtonClick += Settnigs_OnAudioSettingsButtonClick;
        SettingsUI.Instance.OnTasksButtonClick += Settings_OnTasksButtonClick;

        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick += MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnSettigsButtonClick += MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick += MainCanvas_OnAnyButtonClick;
    }

    private void Settings_OnTasksButtonClick(object sender, System.EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnAnyButtonClick(object sender, System.EventArgs e) {
        Hide();
    }

    private void Settnigs_OnAudioSettingsButtonClick(object sender, System.EventArgs e) {
        Show();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
    private void Show() {
        gameObject.SetActive(true);
    }

    private void OnDestroy() {
        SettingsUI.Instance.OnAudioSettingsButtonClick -= Settnigs_OnAudioSettingsButtonClick;
        SettingsUI.Instance.OnTasksButtonClick -= Settings_OnTasksButtonClick;

        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnSettigsButtonClick -= MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick -= MainCanvas_OnAnyButtonClick;
    }
}
