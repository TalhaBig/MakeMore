using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeWithCashUI : MonoBehaviour{
    //Singleton
    public static UpgradeWithCashUI Instance {  get; private set; }

    [SerializeField] private Button UpgradeClickMultiplierButton;
    [SerializeField] private Button UpgradeCashMultiplier;
    [SerializeField] private Button UpgradeMakeTime;

    //strings for player prefs
    private const string ClickMultiplierString = "ClickMultiplier";
    private const string CashMultiplierString = "CashMultiplier";
    private const string TimeMultiplierString = "TimeMultiplier";

    //event to send the respetive Scripts to increase multiplier
    public class OnMultiplierIncreaseEventArgs:EventArgs { public float Multiplier; }
    public event EventHandler<OnMultiplierIncreaseEventArgs> OnClickMultiplierIncrease;
    public event EventHandler<OnMultiplierIncreaseEventArgs> OnCashMultiplierIncrease;
    public event EventHandler<OnMultiplierIncreaseEventArgs> OnMakeTimeIncrease;

    private float ClickMultiplier;
    private float CashMultiplier;
    private float TimeMultiplier;

    [SerializeField] private TextMeshProUGUI ClickTimerMultiplierText;
    [SerializeField] private TextMeshProUGUI CashMultiplierText;
    [SerializeField] private TextMeshProUGUI MakeTimerMultiplierText;

    //price of upgraing 
    private float UpgradePrice = 1f;

    private void Awake() {

        Instance = this;

        ClickMultiplier = PlayerPrefs.GetFloat(ClickMultiplierString,1f);
        CashMultiplier = PlayerPrefs.GetFloat(CashMultiplierString,1f);
        TimeMultiplier = PlayerPrefs.GetFloat(TimeMultiplierString,0.1f);

        CashMultiplier = (float)Math.Round(CashMultiplier, 1);
        ClickMultiplier = (float)Math.Round(ClickMultiplier, 1);
        TimeMultiplier = (float)Math.Round(TimeMultiplier, 1);

        UpgradeCashMultiplier.onClick.AddListener(() => {
            CashMultiplier += 0.1f;

            CashMultiplier = (float)Math.Round(CashMultiplier, 1);

            PlayerPrefs.SetFloat(CashMultiplierString,CashMultiplier);
            PlayerPrefs.Save();

            OnCashMultiplierIncrease?.Invoke(this, new OnMultiplierIncreaseEventArgs { Multiplier = CashMultiplier });

            CashMoney.Instance.DecreaseMoney(UpgradePrice);

            SoundManager.Instance.PlayUpgradeSound();
        });
        UpgradeClickMultiplierButton.onClick.AddListener(() => {
            ClickMultiplier += 0.1f;

            ClickMultiplier = (float)Math.Round(ClickMultiplier, 1);

            PlayerPrefs.SetFloat(ClickMultiplierString, ClickMultiplier);
            PlayerPrefs.Save();

            OnClickMultiplierIncrease?.Invoke(this, new OnMultiplierIncreaseEventArgs { Multiplier = ClickMultiplier});
            
            CashMoney.Instance.DecreaseMoney(UpgradePrice);

            SoundManager.Instance.PlayUpgradeSound();
        });
        UpgradeMakeTime.onClick.AddListener(() => {
            TimeMultiplier += 0.1f;
            
            TimeMultiplier = (float)Math.Round(TimeMultiplier, 1);
            
            PlayerPrefs.SetFloat(TimeMultiplierString, TimeMultiplier);
            PlayerPrefs.Save();

            OnMakeTimeIncrease?.Invoke(this, new OnMultiplierIncreaseEventArgs { Multiplier = TimeMultiplier });

            CashMoney.Instance.DecreaseMoney(UpgradePrice);

            SoundManager.Instance.PlayUpgradeSound();
        });
    }

    private void Start() {
        UpgradeClickMultiplierButton.interactable = UpgradeCashMultiplier.interactable = UpgradeMakeTime.interactable = CashMoney.Instance.GetMoney() >= UpgradePrice;

        ClickTimerMultiplierText.text = ClickMultiplier.ToString();
        MakeTimerMultiplierText.text = TimeMultiplier.ToString();
        CashMultiplierText.text = CashMultiplier.ToString();
        
        Hide();

        MainCanvasUI.Instance.OnUpgradeCashButtonClick += MainCanvas_OnUpgradeCashButtonClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick += MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnSettigsButtonClick += MainCanvas_OnTasksButtonClick;

        CashMoney.Instance.OnCashChanged += Cash_OnCashChanged;
    }

    private void Cash_OnCashChanged(object sender, CashMoney.OnCashChangedEventArgs e) {
        UpgradeClickMultiplierButton.interactable = UpgradeCashMultiplier.interactable = UpgradeMakeTime.interactable = CashMoney.Instance.GetMoney() >= UpgradePrice;

        ClickTimerMultiplierText.text = ClickMultiplier.ToString();
        MakeTimerMultiplierText.text = TimeMultiplier.ToString();
        CashMultiplierText.text = CashMultiplier.ToString();
    }

    private void MainCanvas_OnTasksButtonClick(object sender, EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnGoToUpgradeClick(object sender, EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnGoToNormalGameplayClick(object sender, EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnUpgradeCashButtonClick(object sender, EventArgs e) {
        Show();
    }


    private void Hide() {
        gameObject.SetActive(false);
    }
    private void Show() {
        gameObject.SetActive(true);
    }

    private void OnDestroy() {
        MainCanvasUI.Instance.OnUpgradeCashButtonClick -= MainCanvas_OnUpgradeCashButtonClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnSettigsButtonClick -= MainCanvas_OnTasksButtonClick;

        CashMoney.Instance.OnCashChanged -= Cash_OnCashChanged;
    }

    public float GetCashMultiplier() {
        return CashMultiplier;
    }

    public float GetClickMultiplier() {
        return ClickMultiplier;
    }

    public float GetTimeMultiplier() {
        return TimeMultiplier;
    }


}
