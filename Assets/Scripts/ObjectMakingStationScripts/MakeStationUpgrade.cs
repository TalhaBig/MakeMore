using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MakeStationUpgrade : MonoBehaviour{

    [SerializeField] private Button UpgradeButton;
    [SerializeField] private TextMeshProUGUI UpgradeCostText;

    //reference of makeStation
    [SerializeField] private ObjectMakeStation makeStation;

    //event for telling MakeStation to increase its speed
    public class OnUpgradedEventArgs:EventArgs { public float cost; }
    public event EventHandler<OnUpgradedEventArgs> OnUpgraded;


    //variables for upgrade cost etc
    private float UpgradeCost;
    private float CostMultiplier;

    private void OnEnable() {
        UpgradeButton.onClick.AddListener(() => {
            Debug.Log(UpgradeCost);

            if (NormalMoney.Instance.GetMoney() >= UpgradeCost) {
                Debug.Log("Click");


                NormalMoney.Instance.DecreaseMoney(UpgradeCost);

                UpgradeCost += (CostMultiplier * UpgradeCost);
                Mathf.CeilToInt(UpgradeCost);

                UpdateVisual();

                OnUpgraded?.Invoke(this, new OnUpgradedEventArgs { cost = UpgradeCost });

                SoundManager.Instance.PlayUpgradeSound();
            }
        });
    }

    private void Start() {
        UpgradeCost = 100f;
        CostMultiplier = 1.5f;

        UpdateVisual();

        MainCanvasUI.Instance.OnGoToUpgradeClick += MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnSettigsButtonClick += MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick += MainCanvas_OnUpgradeCashButtonClick;

    }

    private void MainCanvas_OnUpgradeCashButtonClick(object sender, EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnTasksButtonClick(object sender, EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnGoToNormalGameplayClick(object sender, EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnGoToUpgradeClick(object sender, EventArgs e) {
        Show();
    }

    private void UpdateVisual() {
        UpgradeCostText.text = UpgradeCost.ToString();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnSettigsButtonClick -= MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick -= MainCanvas_OnUpgradeCashButtonClick;
    }

    public void SetCost(float cost) {
        UpgradeCost = cost;

        Mathf.CeilToInt(UpgradeCost);

        UpdateVisual();
    }

}
