using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TradeStation : MonoBehaviour,IHasProgress{
    //singleton pattern
    public static TradeStation Instance {  get; private set; }

    //string for getting the index of the object
    private const string IndexOfObjectString = "IndexOfObject";
    private const string CurrentObjectsTradedString = "CurrentObjectsTraded";
    private const string TimesLoopedString = "TimesLooped";
    private const string TotalObjectsMadeString = "TotalObjectsMade";

    private int IndexOfObject;
    private int MaxObjectNeededToLevelUp;
    private int CurrentObjectsTraded;
    private int TimesLooped;


    [SerializeField] private TextMeshProUGUI CurrentXPPercent;

    //reference for the box collider for not allowing to interact while in upgrade screen
    private BoxCollider2D hitbox;

    //Vector 2 for storing the original hitbox size
    private Vector2 hitboxSize;

    //reference for the ObjectMakeSO
    [SerializeField] private ModelChangeResultSO[] ObjectsToChange;
    private ModelChangeResultSO CurrentObject;

    //event to say to making station to delete all output models
    public event EventHandler OnTrade;

    //event for sending the money script to increase or decerease money
    public class OnTradeForMoneyEventArgs : EventArgs { public float money; }
    public event EventHandler<OnTradeForMoneyEventArgs> OnTradeForMoney;

    //event for saying that object is upgraded
    public class OnObjectUpgradedEventArgs : EventArgs { public ModelChangeResultSO newObject; public int TimesLooped; }
    public event EventHandler<OnObjectUpgradedEventArgs> OnObjectUpgraded;

    //integer to see how many objects have been made
    private int TotalObjectsMade;

    //cash multiplier
    private float cashMultiplier;


    //event for HasProgress
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    private void Awake() {
        //set instance
        Instance = this;

        hitbox = GetComponent<BoxCollider2D>();

        TimesLooped = PlayerPrefs.GetInt(TimesLoopedString,0);
        IndexOfObject = PlayerPrefs.GetInt(IndexOfObjectString,0);
        MaxObjectNeededToLevelUp = (IndexOfObject+1) * 30 + (TimesLooped * ObjectsToChange.Length * 30);

        CurrentObjectsTraded = PlayerPrefs.GetInt(CurrentObjectsTradedString,0);
        CurrentObject = ObjectsToChange[IndexOfObject];

        TotalObjectsMade = PlayerPrefs.GetInt(TotalObjectsMadeString,0);

        CurrentXPPercent.text = (Mathf.FloorToInt((((float)CurrentObjectsTraded / MaxObjectNeededToLevelUp) * 100f))).ToString() + "%";

    }

    private void Start() {
        cashMultiplier = UpgradeWithCashUI.Instance.GetCashMultiplier();

        hitboxSize.x = hitbox.size.x;
        hitboxSize.y = hitbox.size.y;

        ObjectMakeStation.OnAnyObjectMade += ObjectMakeStation_OnObjectMade;
        Player.Instance.OnTradeStationClick += Player_OnTradeStationClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick += MianCanvasUI_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvasUI_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnSettigsButtonClick += MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick += MainCanvas_OnUpgradeCashButtonClick;

        UpgradeWithCashUI.Instance.OnCashMultiplierIncrease += UpgradeCashUI_OnCashMultiplierIncrease;

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs {progress = (float)CurrentObjectsTraded / MaxObjectNeededToLevelUp });
    }

    private void UpgradeCashUI_OnCashMultiplierIncrease(object sender, UpgradeWithCashUI.OnMultiplierIncreaseEventArgs e) {
        cashMultiplier = e.Multiplier;
    }

    private void MainCanvas_OnUpgradeCashButtonClick(object sender, EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnTasksButtonClick(object sender, EventArgs e) {
        Hide();
    }

    private void MainCanvasUI_OnGoToNormalGameplayClick(object sender, EventArgs e) {
        ShowHitbox();
        Show();
    }

    private void MianCanvasUI_OnGoToUpgradeClick(object sender, EventArgs e) {
        HideHitbox();
    }

    private void Player_OnTradeStationClick(object sender, EventArgs e) {

        OnTrade?.Invoke(this, EventArgs.Empty);
        OnTradeForMoney?.Invoke(this, new OnTradeForMoneyEventArgs { money = (TotalObjectsMade * (CurrentObject.Value + (TimesLooped * 30 * ObjectsToChange.Length))) * cashMultiplier });

        CurrentObjectsTraded += TotalObjectsMade;

        if(CurrentObjectsTraded >= MaxObjectNeededToLevelUp) {
            CurrentObjectsTraded -= MaxObjectNeededToLevelUp;

            IndexOfObject++;
            if(IndexOfObject >= ObjectsToChange.Length) {
                IndexOfObject = 0;
                TimesLooped++;

                PlayerPrefs.SetInt(TimesLoopedString,TimesLooped);
                PlayerPrefs.Save();
            }
            CurrentObject = ObjectsToChange[IndexOfObject];

            PlayerPrefs.SetInt(IndexOfObjectString, IndexOfObject);
            PlayerPrefs.Save();

            OnObjectUpgraded?.Invoke(this, new OnObjectUpgradedEventArgs { newObject = CurrentObject, TimesLooped = TimesLooped });

            MaxObjectNeededToLevelUp = (IndexOfObject + 1) * 30 + (TimesLooped * ObjectsToChange.Length * 30);
        }
        CurrentXPPercent.text = (Mathf.FloorToInt(((float)CurrentObjectsTraded / MaxObjectNeededToLevelUp) * 100f)).ToString() + "%";

        PlayerPrefs.SetInt(CurrentObjectsTradedString, CurrentObjectsTraded);
        PlayerPrefs.Save();

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progress = (float)CurrentObjectsTraded/ MaxObjectNeededToLevelUp});

        TotalObjectsMade = 0;
        PlayerPrefs.SetInt(TotalObjectsMadeString,0);
        PlayerPrefs.Save();
    }

    private void ObjectMakeStation_OnObjectMade(object sender, System.EventArgs e) {
        TotalObjectsMade++;

        PlayerPrefs.SetInt(TotalObjectsMadeString,TotalObjectsMade);
        PlayerPrefs.Save();
    }


    private void OnDestroy() {
        ObjectMakeStation.OnAnyObjectMade -= ObjectMakeStation_OnObjectMade;
        Player.Instance.OnTradeStationClick -= Player_OnTradeStationClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MianCanvasUI_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvasUI_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnSettigsButtonClick -= MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick -= MainCanvas_OnUpgradeCashButtonClick;

        UpgradeWithCashUI.Instance.OnCashMultiplierIncrease -= UpgradeCashUI_OnCashMultiplierIncrease;
    }

    public ModelChangeResultSO GetCurrentWorkinObject() {
        return CurrentObject;
    }

    public int GetTimesLooped() {
        return TimesLooped;
    }
    public int GetTotalObjects() {
        return ObjectsToChange.Length;
    }

    private void HideHitbox() {
        hitbox.size = new Vector2(0f,0f);
    }
    private void ShowHitbox() {
        hitbox.size = new Vector2(hitboxSize.x,hitboxSize.y);
    }

    private void Show() {
        gameObject.SetActive(true);
    }
    private void Hide() {
        gameObject.SetActive(false);
    }
}
