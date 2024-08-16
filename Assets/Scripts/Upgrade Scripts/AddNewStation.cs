using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddNewStation : MonoBehaviour{
    //reference of makeStation 
    [SerializeField] private ObjectMakeStation MakeStationPrefab;

    //string for Player prefs
    private const string CostMultiplierString = "CostMultiplier";
    private const string PriceOfAddingString = "PriveOfAdding";

    private ObjectMakeStation makeStation;

    //reference to see if there should be a makeStation at the start
    [SerializeField] private bool ShouldMakeStationSpawn;

    //reference of button and text
    [SerializeField] private Button AddButton;
    [SerializeField] private TextMeshProUGUI PriceText;

    //reference of canvas to show only on upgrade click
    [SerializeField] private GameObject AddCanvas;

    //bool to see if a prefab has already been spawned
    private bool isMakeStationSpawned;

    //price of adding
    private static float CostMultiplier;
    private static float PriceOfAdding;

    //static event to updatevisual on any change
    private static event EventHandler OnPriceChanged;

    //event to send On Adding a new one
    public event EventHandler OnNewStationAdded;

    //event to send that the makeStation has Updated
    public event EventHandler<ObjectMakeStation.OnUpgradeEventArgs> OnStationUpgraded;
    public event EventHandler<ObjectMakeStation.OnNewObjectMadeEventArgs> OnStationMadeNewObject;


    private void Awake() {
        CostMultiplier = PlayerPrefs.GetFloat(CostMultiplierString,1f);

        PriceOfAdding = PlayerPrefs.GetFloat(PriceOfAddingString,200*CostMultiplier);

        Mathf.RoundToInt(PriceOfAdding);

        AddButton.onClick.AddListener(() => {
            if (!isMakeStationSpawned) {
                //spawn a new station

                if (NormalMoney.Instance.GetMoney() >= PriceOfAdding) {
                    
                    makeStation = Instantiate(MakeStationPrefab, transform.position,Quaternion.identity);

                    makeStation.OnUpgrade += MakeStation_OnUpgrade;
                    makeStation.OnNewObjectMade += MakeStation_OnNewObjectMade;

                    isMakeStationSpawned = true;

                    OnNewStationAdded?.Invoke(this, EventArgs.Empty);

                    makeStation.HideNormalObject();

                    NormalMoney.Instance.DecreaseMoney(PriceOfAdding);

                    CostMultiplier += 0.2f;

                    PlayerPrefs.SetFloat(CostMultiplierString,CostMultiplier);

                    PriceOfAdding += (PriceOfAdding * CostMultiplier);

                    Mathf.RoundToInt(PriceOfAdding);

                    PlayerPrefs.SetFloat(PriceOfAddingString,PriceOfAdding);

                    PlayerPrefs.Save();

                    UpdateVisual();

                    OnPriceChanged?.Invoke(this, EventArgs.Empty);

                    HideCanvas();

                    SoundManager.Instance.PlayUpgradeSound();
                }
            }
        });
    }

    private void Start() {

        UpdateVisual();
        HideCanvas();

        if (ShouldMakeStationSpawn) {
            //spawn the make station

            makeStation = Instantiate(MakeStationPrefab, transform.position, Quaternion.identity);

            isMakeStationSpawned = true;

            makeStation.OnUpgrade += MakeStation_OnUpgrade;
            makeStation.OnNewObjectMade += MakeStation_OnNewObjectMade;
        }

        MainCanvasUI.Instance.OnGoToUpgradeClick += MianCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnSettigsButtonClick += MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick += MianCanvas_OnUpgradeCashButtonClick;

        OnPriceChanged += AddNewStation_OnPriceChanged;
    }

    private void MakeStation_OnNewObjectMade(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        OnStationMadeNewObject?.Invoke(this, new ObjectMakeStation.OnNewObjectMadeEventArgs { Objects = e.Objects });
    }

    private void MakeStation_OnUpgrade(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        OnStationUpgraded?.Invoke(this, new ObjectMakeStation.OnUpgradeEventArgs { cost = e.cost,TimerMultiplier = e.TimerMultiplier });   
    }


    private void AddNewStation_OnPriceChanged(object sender, EventArgs e) {
        UpdateVisual();
    }

    private void MianCanvas_OnUpgradeCashButtonClick(object sender, EventArgs e) {
        HideCanvas();
    }

    private void MainCanvas_OnTasksButtonClick(object sender, EventArgs e) {
        HideCanvas();   
    }

    private void MainCanvas_OnGoToNormalGameplayClick(object sender, System.EventArgs e) {
        HideCanvas();
    }

    private void MianCanvas_OnGoToUpgradeClick(object sender, System.EventArgs e) {
        ShowCanvas();
    }

    private void ShowCanvas() {
        if (!isMakeStationSpawned) {
            AddCanvas.SetActive(true);
        }
    }
    private void HideCanvas() {
        AddCanvas.SetActive(false);
    }

    private void UpdateVisual() {
        PriceText.text = PriceOfAdding.ToString();
    }

    private void OnDestroy() {
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MianCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnSettigsButtonClick -= MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick -= MianCanvas_OnUpgradeCashButtonClick;

        OnPriceChanged -= AddNewStation_OnPriceChanged;

        if (makeStation != null) {
            makeStation.OnUpgrade -= MakeStation_OnUpgrade;
            makeStation.OnNewObjectMade -= MakeStation_OnNewObjectMade;
        }
    }

    public void SetSpawnDefault(bool isSpawned) {
        ShouldMakeStationSpawn = isSpawned;
    }

    public void WriteMakeStationData(float TimerIncreaseMultiplier, float CostOfUpgrade, int ObjectHoldCurrently) {
        if (ShouldMakeStationSpawn) {
            //only give data if it should spawn
            StartCoroutine(SetMakeStationDataCoroutine(TimerIncreaseMultiplier,CostOfUpgrade,ObjectHoldCurrently));
        }
    }

    private IEnumerator SetMakeStationDataCoroutine(float TimerIncreaseMultiplier, float CostOfUpgrade, int ObjectHoldCurrently) {
        //wait for the station to spawn then give data
        
        yield return new WaitUntil(()=> isMakeStationSpawned);

        makeStation.SetStationData(TimerIncreaseMultiplier,CostOfUpgrade,ObjectHoldCurrently);
    }
}
