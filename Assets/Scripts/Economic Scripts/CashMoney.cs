using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashMoney : MonoBehaviour, IMoney {
    //Singleton
    public static CashMoney Instance {  get; private set; }

    private const string TotalCashString = "CashSaved";

    public class OnCashChangedEventArgs : EventArgs { public float cash; }
    public event EventHandler<OnCashChangedEventArgs> OnCashChanged;

    private float CashAmount;

    private void Awake() {
        Instance = this;

        CashAmount = PlayerPrefs.GetFloat(TotalCashString,0f);
    }


    public void DecreaseMoney(float money) {
        CashAmount -= money;

        PlayerPrefs.SetFloat(TotalCashString,CashAmount);
        PlayerPrefs.Save();

        OnCashChanged?.Invoke(this, new OnCashChangedEventArgs { cash = CashAmount });
    }

    public float GetMoney() {
        return CashAmount;
    }

    public void IncreaseMoney(float money) {
        CashAmount += money;

        PlayerPrefs.SetFloat(TotalCashString,CashAmount);
        PlayerPrefs.Save();

        OnCashChanged?.Invoke(this, new OnCashChangedEventArgs { cash = CashAmount });
    }
}
