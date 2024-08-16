using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class AddStationsSave : MonoBehaviour{

    //starts from 0 as top left to 8 as bottom right with left to right
    [SerializeField] private AddNewStation TopLeft_StationsArr;
    [SerializeField] private AddNewStation TopMiddle_StationsArr;
    [SerializeField] private AddNewStation TopRight_StationsArr;
    [SerializeField] private AddNewStation MiddleLeft_StationsArr;
    [SerializeField] private AddNewStation MiddleMiddle_StationsArr;
    [SerializeField] private AddNewStation MiddleRight_StationsArr;
    [SerializeField] private AddNewStation BottomLeft_StationsArr;
    [SerializeField] private AddNewStation BottomMiddle_StationsArr;
    [SerializeField] private AddNewStation BottomRight_StationsArr;

    //strings for player prefs
    private readonly string[] AddNewStationPrefsSpawnString = { "TopLeftAddNewStationSpawn", "TopMiddleAddNewStationSpawn", "TopRightAddNewStationSpawn", "MiddleLeftAddNewStationSpawn", "MiddleMiddleAddNewStationSpawn", "MiddleRightAddNewStationSpawn", "BottomLeftAddNewStationSpawn", "BottomMiddleAddNewStationSpawn", "BottomRightAddNewStationSpawn" };

    //strings for Upgrade Cost of make stations
    private readonly string[] MakeStationsUpgradeCostString = { "TopLeftStationUpgradeCost", "TopMiddleStationUpgradeCost", "TopRightStationUpgradeCost", "MiddleLeftStationUpgradeCost", "MiddleMiddleStationUpgradeCost", "MiddleRightStationUpgradeCost", "BottomLeftStationUpgradeCost", "BottomMiddleStationUpgradeCost", "BottomRightStationUpgradeCost" };

    //strings for HoldingObjects of Stations
    private readonly string[] MakeStationsHoldObjectsString = { "TopLeftStationHoldObjects", "TopMiddleStationHoldObjects", "TopRightStationHoldObjects", "MiddleLeftStationHoldObjects", "MiddleMiddleStationHoldObjects", "MiddleRightStationHoldObjects", "BottomLeftStationHoldObjects", "BottomMiddleStationHoldObjects", "BottomRightStationHoldObjects" };

    //strings for  Timers of Stations
    private readonly string[] MakeStationsTimerMultipliersString = { "TopLeftStationTimerMultipliers", "TopMiddleStationTimerMultipliers", "TopRightStationTimerMultipliers", "MiddleLeftStationTimerMultipliers", "MiddleMiddleStationTimerMultipliers", "MiddleRightStationTimerMultipliers", "BottomLeftStationTimerMultipliers", "BottomMiddleStationTimerMultipliers", "BottomRightStationTimerMultipliers" };

    private void Awake() {
        //set the isSpawned booleans of add stations

        TopLeft_StationsArr.SetSpawnDefault(PlayerPrefs.GetInt(AddNewStationPrefsSpawnString[0],0) == 1);
        TopMiddle_StationsArr.SetSpawnDefault(PlayerPrefs.GetInt(AddNewStationPrefsSpawnString[1],0) == 1);
        TopRight_StationsArr.SetSpawnDefault(PlayerPrefs.GetInt(AddNewStationPrefsSpawnString[2],0) == 1);
        MiddleLeft_StationsArr.SetSpawnDefault(PlayerPrefs.GetInt(AddNewStationPrefsSpawnString[3],0) == 1);
        MiddleMiddle_StationsArr.SetSpawnDefault(PlayerPrefs.GetInt(AddNewStationPrefsSpawnString[4],1) == 1);
        MiddleRight_StationsArr.SetSpawnDefault(PlayerPrefs.GetInt(AddNewStationPrefsSpawnString[5],0) == 1);
        BottomLeft_StationsArr.SetSpawnDefault(PlayerPrefs.GetInt(AddNewStationPrefsSpawnString[6],0) == 1);
        BottomMiddle_StationsArr.SetSpawnDefault(PlayerPrefs.GetInt(AddNewStationPrefsSpawnString[7],0) == 1);
        BottomRight_StationsArr.SetSpawnDefault(PlayerPrefs.GetInt(AddNewStationPrefsSpawnString[8],0) == 1);

        //give data to all the stations
        TopLeft_StationsArr.WriteMakeStationData(/* Timer Multiplier*/ PlayerPrefs.GetFloat(MakeStationsTimerMultipliersString[0], 1f), /* Cost */PlayerPrefs.GetFloat(MakeStationsUpgradeCostString[0],100f),/* Objects Held */PlayerPrefs.GetInt(MakeStationsHoldObjectsString[0],0));
        TopMiddle_StationsArr.WriteMakeStationData(/* Timer Multiplier*/ PlayerPrefs.GetFloat(MakeStationsTimerMultipliersString[1], 1f), /* Cost */PlayerPrefs.GetFloat(MakeStationsUpgradeCostString[1],100f),/* Objects Held */PlayerPrefs.GetInt(MakeStationsHoldObjectsString[1],0));
        TopRight_StationsArr.WriteMakeStationData(/* Timer Multiplier*/ PlayerPrefs.GetFloat(MakeStationsTimerMultipliersString[2], 1f), /* Cost */PlayerPrefs.GetFloat(MakeStationsUpgradeCostString[2],100f),/* Objects Held */PlayerPrefs.GetInt(MakeStationsHoldObjectsString[2],0));
        MiddleLeft_StationsArr.WriteMakeStationData(/* Timer Multiplier*/ PlayerPrefs.GetFloat(MakeStationsTimerMultipliersString[3], 1f), /* Cost */PlayerPrefs.GetFloat(MakeStationsUpgradeCostString[3],100f),/* Objects Held */PlayerPrefs.GetInt(MakeStationsHoldObjectsString[3],0));
        MiddleMiddle_StationsArr.WriteMakeStationData(/* Timer Multiplier*/ PlayerPrefs.GetFloat(MakeStationsTimerMultipliersString[4], 1f), /* Cost */PlayerPrefs.GetFloat(MakeStationsUpgradeCostString[4],100f),/* Objects Held */PlayerPrefs.GetInt(MakeStationsHoldObjectsString[4],0));
        MiddleRight_StationsArr.WriteMakeStationData(/* Timer Multiplier*/ PlayerPrefs.GetFloat(MakeStationsTimerMultipliersString[5], 1f), /* Cost */PlayerPrefs.GetFloat(MakeStationsUpgradeCostString[5],100f),/* Objects Held */PlayerPrefs.GetInt(MakeStationsHoldObjectsString[5],0));
        BottomLeft_StationsArr.WriteMakeStationData(/* Timer Multiplier*/ PlayerPrefs.GetFloat(MakeStationsTimerMultipliersString[6], 1f), /* Cost */PlayerPrefs.GetFloat(MakeStationsUpgradeCostString[6],100f),/* Objects Held */PlayerPrefs.GetInt(MakeStationsHoldObjectsString[6],0));
        BottomMiddle_StationsArr.WriteMakeStationData(/* Timer Multiplier*/ PlayerPrefs.GetFloat(MakeStationsTimerMultipliersString[7], 1f), /* Cost */PlayerPrefs.GetFloat(MakeStationsUpgradeCostString[7],100f),/* Objects Held */PlayerPrefs.GetInt(MakeStationsHoldObjectsString[7],0));
        BottomRight_StationsArr.WriteMakeStationData(/* Timer Multiplier*/ PlayerPrefs.GetFloat(MakeStationsTimerMultipliersString[8], 1f), /* Cost */PlayerPrefs.GetFloat(MakeStationsUpgradeCostString[8],100f),/* Objects Held */PlayerPrefs.GetInt(MakeStationsHoldObjectsString[8],0));
    }


    private void Start() {

        //events for Adding a station
        TopLeft_StationsArr.OnNewStationAdded += TopLeft_StationsArr_OnNewStationAdded;
        TopMiddle_StationsArr.OnNewStationAdded += TopMiddle_StationsArr_OnNewStationAdded;
        TopRight_StationsArr.OnNewStationAdded += TopRight_StationsArr_OnNewStationAdded;
        MiddleLeft_StationsArr.OnNewStationAdded += MiddleLeft_StationsArr_OnNewStationAdded;
        MiddleMiddle_StationsArr.OnNewStationAdded += MiddleMiddle_StationsArr_OnNewStationAdded;
        MiddleRight_StationsArr.OnNewStationAdded += MiddleRight_StationsArr_OnNewStationAdded;
        BottomLeft_StationsArr.OnNewStationAdded += BottomLeft_StationsArr_OnNewStationAdded;
        BottomMiddle_StationsArr.OnNewStationAdded += BottomMiddle_StationsArr_OnNewStationAdded;
        BottomRight_StationsArr.OnNewStationAdded += BottomRight_StationsArr_OnNewStationAdded;

        //event for when a station is upgraded
        TopLeft_StationsArr.OnStationUpgraded += TopLeft_StationsArr_OnStationUpgraded;
        TopMiddle_StationsArr.OnStationUpgraded += TopMiddle_StationsArr_OnStationUpgraded;
        TopRight_StationsArr.OnStationUpgraded += TopRight_StationsArr_OnStationUpgraded;
        MiddleLeft_StationsArr.OnStationUpgraded += MiddleLeft_StationsArr_OnStationUpgraded;
        MiddleMiddle_StationsArr.OnStationUpgraded += MiddleMiddle_StationsArr_OnStationUpgraded;
        MiddleRight_StationsArr.OnStationUpgraded += MiddleRight_StationsArr_OnStationUpgraded;
        BottomLeft_StationsArr.OnStationUpgraded += BottomLeft_StationsArr_OnStationUpgraded;
        BottomMiddle_StationsArr.OnStationUpgraded += BottomMiddle_StationsArr_OnStationUpgraded;
        BottomRight_StationsArr.OnStationUpgraded += BottomRight_StationsArr_OnStationUpgraded;


        //events for when a station has made a new Object
        TopLeft_StationsArr.OnStationMadeNewObject += TopLeft_StationsArr_OnStationMadeNewObject;
        TopMiddle_StationsArr.OnStationMadeNewObject += TopMiddle_StationsArr_OnStationMadeNewObject;
        TopRight_StationsArr.OnStationMadeNewObject += TopRight_StationsArr_OnStationMadeNewObject;
        MiddleLeft_StationsArr.OnStationMadeNewObject += MiddleLeft_StationsArr_OnStationMadeNewObject;
        MiddleMiddle_StationsArr.OnStationMadeNewObject += MiddleMiddle_StationsArr_OnStationMadeNewObject;
        MiddleRight_StationsArr.OnStationMadeNewObject += MiddleRight_StationsArr_OnStationMadeNewObject;
        BottomLeft_StationsArr.OnStationMadeNewObject += BottomLeft_StationsArr_OnStationMadeNewObject;
        BottomMiddle_StationsArr.OnStationMadeNewObject += BottomMiddle_StationsArr_OnStationMadeNewObject;
        BottomRight_StationsArr.OnStationMadeNewObject += BottomRight_StationsArr_OnStationMadeNewObject;


    }

    private void BottomRight_StationsArr_OnStationMadeNewObject(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        PlayerPrefs.SetInt(MakeStationsHoldObjectsString[8], e.Objects);
        PlayerPrefs.Save();
    }

    private void BottomMiddle_StationsArr_OnStationMadeNewObject(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        PlayerPrefs.SetInt(MakeStationsHoldObjectsString[7], e.Objects);
        PlayerPrefs.Save();
    }

    private void BottomLeft_StationsArr_OnStationMadeNewObject(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        PlayerPrefs.SetInt(MakeStationsHoldObjectsString[6], e.Objects);
        PlayerPrefs.Save();
    }

    private void MiddleRight_StationsArr_OnStationMadeNewObject(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        PlayerPrefs.SetInt(MakeStationsHoldObjectsString[5], e.Objects);
        PlayerPrefs.Save();
    }

    private void MiddleMiddle_StationsArr_OnStationMadeNewObject(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        PlayerPrefs.SetInt(MakeStationsHoldObjectsString[4], e.Objects);
        PlayerPrefs.Save();
    }

    private void MiddleLeft_StationsArr_OnStationMadeNewObject(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        PlayerPrefs.SetInt(MakeStationsHoldObjectsString[3], e.Objects);
        PlayerPrefs.Save();
    }

    private void TopRight_StationsArr_OnStationMadeNewObject(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        PlayerPrefs.SetInt(MakeStationsHoldObjectsString[2], e.Objects);
        PlayerPrefs.Save();
    }

    private void TopMiddle_StationsArr_OnStationMadeNewObject(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        PlayerPrefs.SetInt(MakeStationsHoldObjectsString[1], e.Objects);
        PlayerPrefs.Save();
    }

    private void TopLeft_StationsArr_OnStationMadeNewObject(object sender, ObjectMakeStation.OnNewObjectMadeEventArgs e) {
        PlayerPrefs.SetInt(MakeStationsHoldObjectsString[0],e.Objects);
        PlayerPrefs.Save();
    }

    private void BottomRight_StationsArr_OnStationUpgraded(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        PlayerPrefs.SetFloat(MakeStationsUpgradeCostString[8], e.cost);
        PlayerPrefs.SetFloat(MakeStationsTimerMultipliersString[8], e.TimerMultiplier);
        PlayerPrefs.Save();
    }

    private void BottomMiddle_StationsArr_OnStationUpgraded(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        PlayerPrefs.SetFloat(MakeStationsUpgradeCostString[7], e.cost);
        PlayerPrefs.SetFloat(MakeStationsTimerMultipliersString[7], e.TimerMultiplier);
        PlayerPrefs.Save();
    }

    private void BottomLeft_StationsArr_OnStationUpgraded(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        PlayerPrefs.SetFloat(MakeStationsUpgradeCostString[6], e.cost);
        PlayerPrefs.SetFloat(MakeStationsTimerMultipliersString[6], e.TimerMultiplier);
        PlayerPrefs.Save();
    }

    private void MiddleRight_StationsArr_OnStationUpgraded(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        PlayerPrefs.SetFloat(MakeStationsUpgradeCostString[5], e.cost);
        PlayerPrefs.SetFloat(MakeStationsTimerMultipliersString[5], e.TimerMultiplier);
        PlayerPrefs.Save();
    }

    private void MiddleMiddle_StationsArr_OnStationUpgraded(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        PlayerPrefs.SetFloat(MakeStationsUpgradeCostString[4], e.cost);
        PlayerPrefs.SetFloat(MakeStationsTimerMultipliersString[4], e.TimerMultiplier);
        PlayerPrefs.Save();
    }

    private void MiddleLeft_StationsArr_OnStationUpgraded(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        PlayerPrefs.SetFloat(MakeStationsUpgradeCostString[3], e.cost);
        PlayerPrefs.SetFloat(MakeStationsTimerMultipliersString[3], e.TimerMultiplier);
        PlayerPrefs.Save();
    }

    private void TopRight_StationsArr_OnStationUpgraded(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        PlayerPrefs.SetFloat(MakeStationsUpgradeCostString[2], e.cost);
        PlayerPrefs.SetFloat(MakeStationsTimerMultipliersString[2], e.TimerMultiplier);
        PlayerPrefs.Save();
    }

    private void TopMiddle_StationsArr_OnStationUpgraded(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        PlayerPrefs.SetFloat(MakeStationsUpgradeCostString[1], e.cost);
        PlayerPrefs.SetFloat(MakeStationsTimerMultipliersString[1], e.TimerMultiplier);
        PlayerPrefs.Save();
    }

    private void TopLeft_StationsArr_OnStationUpgraded(object sender, ObjectMakeStation.OnUpgradeEventArgs e) {
        PlayerPrefs.SetFloat(MakeStationsUpgradeCostString[0],e.cost);
        PlayerPrefs.SetFloat(MakeStationsTimerMultipliersString[0],e.TimerMultiplier);
        PlayerPrefs.Save();


    }

    private void BottomRight_StationsArr_OnNewStationAdded(object sender, System.EventArgs e) {
        PlayerPrefs.SetInt(AddNewStationPrefsSpawnString[8], 1);
        PlayerPrefs.Save();
    }

    private void BottomMiddle_StationsArr_OnNewStationAdded(object sender, System.EventArgs e) {
        PlayerPrefs.SetInt(AddNewStationPrefsSpawnString[7], 1);
        PlayerPrefs.Save();
    }

    private void BottomLeft_StationsArr_OnNewStationAdded(object sender, System.EventArgs e) {
        PlayerPrefs.SetInt(AddNewStationPrefsSpawnString[6], 1);
        PlayerPrefs.Save();
    }

    private void MiddleRight_StationsArr_OnNewStationAdded(object sender, System.EventArgs e) {
        PlayerPrefs.SetInt(AddNewStationPrefsSpawnString[5], 1);
        PlayerPrefs.Save();
    }

    private void MiddleMiddle_StationsArr_OnNewStationAdded(object sender, System.EventArgs e) {
        PlayerPrefs.SetInt(AddNewStationPrefsSpawnString[4], 1);
        PlayerPrefs.Save();
    }

    private void MiddleLeft_StationsArr_OnNewStationAdded(object sender, System.EventArgs e) {
        PlayerPrefs.SetInt(AddNewStationPrefsSpawnString[3], 1);
        PlayerPrefs.Save();
    }

    private void TopRight_StationsArr_OnNewStationAdded(object sender, System.EventArgs e) {
        PlayerPrefs.SetInt(AddNewStationPrefsSpawnString[2], 1);
        PlayerPrefs.Save();
    }

    private void TopMiddle_StationsArr_OnNewStationAdded(object sender, System.EventArgs e) {
        PlayerPrefs.SetInt(AddNewStationPrefsSpawnString[1], 1);
        PlayerPrefs.Save();
    }

    private void TopLeft_StationsArr_OnNewStationAdded(object sender, System.EventArgs e) {
        PlayerPrefs.SetInt(AddNewStationPrefsSpawnString[0],1);
        PlayerPrefs.Save();
    }

    private void OnDestroy() {
        TopLeft_StationsArr.OnNewStationAdded -= TopLeft_StationsArr_OnNewStationAdded;
        TopMiddle_StationsArr.OnNewStationAdded -= TopMiddle_StationsArr_OnNewStationAdded;
        TopRight_StationsArr.OnNewStationAdded -= TopRight_StationsArr_OnNewStationAdded;
        MiddleLeft_StationsArr.OnNewStationAdded -= MiddleLeft_StationsArr_OnNewStationAdded;
        MiddleMiddle_StationsArr.OnNewStationAdded -= MiddleMiddle_StationsArr_OnNewStationAdded;
        MiddleRight_StationsArr.OnNewStationAdded -= MiddleRight_StationsArr_OnNewStationAdded;
        BottomLeft_StationsArr.OnNewStationAdded -= BottomLeft_StationsArr_OnNewStationAdded;
        BottomMiddle_StationsArr.OnNewStationAdded -= BottomMiddle_StationsArr_OnNewStationAdded;
        BottomRight_StationsArr.OnNewStationAdded -= BottomRight_StationsArr_OnNewStationAdded;

        TopLeft_StationsArr.OnStationUpgraded -= TopLeft_StationsArr_OnStationUpgraded;
        TopMiddle_StationsArr.OnStationUpgraded -= TopMiddle_StationsArr_OnStationUpgraded;
        TopRight_StationsArr.OnStationUpgraded -= TopRight_StationsArr_OnStationUpgraded;
        MiddleLeft_StationsArr.OnStationUpgraded -= MiddleLeft_StationsArr_OnStationUpgraded;
        MiddleMiddle_StationsArr.OnStationUpgraded -= MiddleMiddle_StationsArr_OnStationUpgraded;
        MiddleRight_StationsArr.OnStationUpgraded -= MiddleRight_StationsArr_OnStationUpgraded;
        BottomLeft_StationsArr.OnStationUpgraded -= BottomLeft_StationsArr_OnStationUpgraded;
        BottomMiddle_StationsArr.OnStationUpgraded -= BottomMiddle_StationsArr_OnStationUpgraded;
        BottomRight_StationsArr.OnStationUpgraded -= BottomRight_StationsArr_OnStationUpgraded;

        TopLeft_StationsArr.OnStationMadeNewObject -= TopLeft_StationsArr_OnStationMadeNewObject;
        TopMiddle_StationsArr.OnStationMadeNewObject -= TopMiddle_StationsArr_OnStationMadeNewObject;
        TopRight_StationsArr.OnStationMadeNewObject -= TopRight_StationsArr_OnStationMadeNewObject;
        MiddleLeft_StationsArr.OnStationMadeNewObject -= MiddleLeft_StationsArr_OnStationMadeNewObject;
        MiddleMiddle_StationsArr.OnStationMadeNewObject -= MiddleMiddle_StationsArr_OnStationMadeNewObject;
        MiddleRight_StationsArr.OnStationMadeNewObject -= MiddleRight_StationsArr_OnStationMadeNewObject;
        BottomLeft_StationsArr.OnStationMadeNewObject -= BottomLeft_StationsArr_OnStationMadeNewObject;
        BottomMiddle_StationsArr.OnStationMadeNewObject -= BottomMiddle_StationsArr_OnStationMadeNewObject;
        BottomRight_StationsArr.OnStationMadeNewObject -= BottomRight_StationsArr_OnStationMadeNewObject;
    }
}
