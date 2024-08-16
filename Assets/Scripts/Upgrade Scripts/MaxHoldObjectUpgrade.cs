using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHoldObjectUpgrade : MonoBehaviour{
    //Singleton
    public static MaxHoldObjectUpgrade Instance {  get; private set; }

    //strings for player prefs
    private const string HoldObjectCostSctring = "HoldObjectCost";
    private const string HoldObjectQuantityString = "HoldObjectQuantity";


    private float Cost;

    private int MaxHoldObjects;

    public class OnMaxHoldUpgradeEventArgs:EventArgs { public int HoldObjects; public float cost; }
    public event EventHandler<OnMaxHoldUpgradeEventArgs> OnMaxHoldUpgrade;

    private void Awake() {
        Instance = this;

        Cost = PlayerPrefs.GetFloat(HoldObjectCostSctring,300);
        MaxHoldObjects = PlayerPrefs.GetInt(HoldObjectQuantityString,4);
    }

    private void Start() {

        MainCanvasUI.Instance.OnMaxHoldLimitUpgradeClick += MainCanvas_OnMaxHoldLimitUpgradeClick;
    }

    private void MainCanvas_OnMaxHoldLimitUpgradeClick(object sender, System.EventArgs e) {
        if(NormalMoney.Instance.GetMoney() >= Cost) {

            MaxHoldObjects += 2;

            NormalMoney.Instance.DecreaseMoney(Cost);
            
            Cost += 100;

            //Save the player Prefs
            PlayerPrefs.SetFloat(HoldObjectCostSctring,Cost);
            PlayerPrefs.SetInt(HoldObjectQuantityString,MaxHoldObjects);

            PlayerPrefs.Save();

            OnMaxHoldUpgrade?.Invoke(this, new OnMaxHoldUpgradeEventArgs { HoldObjects = MaxHoldObjects, cost = Cost });

            SoundManager.Instance.PlayUpgradeSound();
        }
    }

    private void OnDestroy() {
        MainCanvasUI.Instance.OnMaxHoldLimitUpgradeClick -= MainCanvas_OnMaxHoldLimitUpgradeClick;
    }

    public float GetCost() {
        return Cost;
    }

    public int GetMaxObjectHold() {
        return MaxHoldObjects;
    }
}
