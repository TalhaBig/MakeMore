using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour{
    //Singleton
    public static SettingsUI Instance { get; private set; }

    public event EventHandler OnTasksButtonClick;
    public event EventHandler OnAudioSettingsButtonClick;

    //reference of sliedrs and tasks button
    [SerializeField] private Button TasksButton;
    [SerializeField] private Button AudioSettingsButton;


    private void Awake() {
        Instance = this;

        TasksButton.onClick.AddListener(() => {
            OnTasksButtonClick?.Invoke(this, EventArgs.Empty);
            Hide();
        });
        AudioSettingsButton.onClick.AddListener(() => { 
            OnAudioSettingsButtonClick?.Invoke(this, EventArgs.Empty);
            Hide();
        });
    }

    private void Start() {
        Hide();

        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick += MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnSettigsButtonClick += MainCanvas_OnSettigsButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick += MainCanvas_OnUpgradeCashButtonClick;
    }

    private void MainCanvas_OnUpgradeCashButtonClick(object sender, System.EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnSettigsButtonClick(object sender, System.EventArgs e) {
        Show();
    }

    private void MainCanvas_OnGoToUpgradeClick(object sender, System.EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnGoToNormalGameplayClick(object sender, System.EventArgs e) {
        Hide();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void OnDestroy() {
        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnSettigsButtonClick -= MainCanvas_OnSettigsButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick -= MainCanvas_OnUpgradeCashButtonClick;
    }
}
