using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NormalMoney : MonoBehaviour,IMoney{
    public static NormalMoney Instance { get; private set; }
        
    private float MoneyAmount;

    private const string NormalMoneySaved = "MoneySaved";

    //event for Canvas to change Money
    public class OnMoneyChangedEventArgs { public float money; }
    public event EventHandler<OnMoneyChangedEventArgs> OnMoneyChanged;

    private void Awake() {
        Instance = this;

        MoneyAmount = PlayerPrefs.GetFloat(NormalMoneySaved,0f);
    }

    private void Start() {
        TradeStation.Instance.OnTradeForMoney += TradeStation_OnTradeForMoney;
    }

    private void TradeStation_OnTradeForMoney(object sender, TradeStation.OnTradeForMoneyEventArgs e) {
        IncreaseMoney(e.money);
    }

    public void DecreaseMoney(float money) {
        MoneyAmount -= money;

        PlayerPrefs.SetFloat(NormalMoneySaved,MoneyAmount);
        PlayerPrefs.Save();

        OnMoneyChanged?.Invoke(this, new OnMoneyChangedEventArgs { money = MoneyAmount});
    }

    public float GetMoney() {
        return MoneyAmount;
    }

    public void IncreaseMoney(float money) {
        
        MoneyAmount += money;

        PlayerPrefs.SetFloat(NormalMoneySaved, MoneyAmount);
        PlayerPrefs.Save();

        OnMoneyChanged?.Invoke(this, new OnMoneyChangedEventArgs { money = MoneyAmount });

        
    }

    private void OnDestroy() {
        TradeStation.Instance.OnTradeForMoney -= TradeStation_OnTradeForMoney;
    }
}
