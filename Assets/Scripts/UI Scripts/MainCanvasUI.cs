using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MainCanvasUI : MonoBehaviour{
    //Singleton pattern
    public static MainCanvasUI Instance { get; private set; }
    
    [SerializeField] private TextMeshProUGUI MoneyText;
    [SerializeField] private TextMeshProUGUI CashText;

    //Upgrade button
    [SerializeField] private Button GoToUpgradeButton;
    [SerializeField] private Button GoToNormalGameplayButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button UpgradeWithCashButton;


    [SerializeField] private Button UpgradeHoldLimitButton;

    //event for make stations to show upgrade 
    public event EventHandler OnGoToUpgradeClick;
    public event EventHandler OnGoToNormalGameplayClick;
    public event EventHandler OnMaxHoldLimitUpgradeClick;
    public event EventHandler OnSettigsButtonClick;
    public event EventHandler OnUpgradeCashButtonClick;

    [SerializeField] private TextMeshProUGUI MaxHoldObjectsQuantityText;
    [SerializeField] private TextMeshProUGUI MaxHoldObjectsCostText;

    private bool isFirstUpdate;

    private void Awake() {
        Instance = this;

        GoToUpgradeButton.onClick.AddListener(() => {
            OnGoToUpgradeClick?.Invoke(this, EventArgs.Empty);
            ShowUpgradeButtons();

        });

        GoToNormalGameplayButton.onClick.AddListener(() => {
            OnGoToNormalGameplayClick?.Invoke(this, EventArgs.Empty);
            HideUpgradeButtons();
        });

        UpgradeHoldLimitButton.onClick.AddListener(() => {
            OnMaxHoldLimitUpgradeClick?.Invoke(this, EventArgs.Empty);
        });
        SettingsButton.onClick.AddListener(() => {
            OnSettigsButtonClick?.Invoke(this, EventArgs.Empty);

            HideUpgradeButtons();
        });
        UpgradeWithCashButton.onClick.AddListener(() => {
            OnUpgradeCashButtonClick?.Invoke(this, EventArgs.Empty);

            HideUpgradeButtons();
        });


    }

    private void Start() {
        isFirstUpdate = true;

        HideUpgradeButtons();
        
        NormalMoney.Instance.OnMoneyChanged += NormalMoney_OnMoneyChanged;
        CashMoney.Instance.OnCashChanged += CashMoney_OnCashChanged;

        MoneyText.text = NormalMoney.Instance.GetMoney().ToString();
        CashText.text = CashMoney.Instance.GetMoney().ToString();

        MaxHoldObjectsCostText.text = MaxHoldObjectUpgrade.Instance.GetCost().ToString();
        MaxHoldObjectsQuantityText.text = MaxHoldObjectUpgrade.Instance.GetMaxObjectHold().ToString() + "->" + (MaxHoldObjectUpgrade.Instance.GetMaxObjectHold() + 2).ToString();

        MaxHoldObjectUpgrade.Instance.OnMaxHoldUpgrade += MaxHold_OnMaxHoldUpgrade;
    }

    private void MaxHold_OnMaxHoldUpgrade(object sender, MaxHoldObjectUpgrade.OnMaxHoldUpgradeEventArgs e) {
        MaxHoldObjectsCostText.text = e.cost.ToString();
        MaxHoldObjectsQuantityText.text = e.HoldObjects.ToString() + "->" + (e.HoldObjects+2).ToString();
    }

    private void CashMoney_OnCashChanged(object sender, CashMoney.OnCashChangedEventArgs e) {
        CashText.text = CashMoney.Instance.GetMoney().ToString();
    }

    private void NormalMoney_OnMoneyChanged(object sender, NormalMoney.OnMoneyChangedEventArgs e) {
        MoneyText.text = e.money.ToString();
    }


    private void OnDestroy() {
        NormalMoney.Instance.OnMoneyChanged -= NormalMoney_OnMoneyChanged;
        CashMoney.Instance.OnCashChanged -= CashMoney_OnCashChanged;

        MaxHoldObjectUpgrade.Instance.OnMaxHoldUpgrade -= MaxHold_OnMaxHoldUpgrade;
    }

    private void HideUpgradeButtons() {
        UpgradeHoldLimitButton.gameObject.SetActive(false);
    }
    private void ShowUpgradeButtons() {
        UpgradeHoldLimitButton.gameObject.SetActive(true);
    }

    private void Update() {
        if (isFirstUpdate) {
            OnGoToNormalGameplayClick?.Invoke(this,EventArgs.Empty);

            isFirstUpdate = false;
        }
    }
}
