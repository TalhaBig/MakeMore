using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour{
    //reference for the camera
    [SerializeField] private Camera lookCamera;

    //event for all Making Stations to reduce Time
    public event EventHandler OnNonInteractableObjectClick;

    //event to send the trade station 
    public event EventHandler OnTradeStationClick;

    public static Player Instance { get; private set; }

    //bool to see if the player is OnNormalGameplay
    private bool isOnNormalGame;

    private void Awake() {
        Instance = this;    
    }

    private void Start() {
        isOnNormalGame = true;

        InputManager.Instance.OnClickOrTouch += InputManager_OnClickOrTouch;
        MainCanvasUI.Instance.OnGoToUpgradeClick += MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnSettigsButtonClick += MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick += MainCanvas_OnUpgradeCashButtonClick;

    }

    private void MainCanvas_OnUpgradeCashButtonClick(object sender, EventArgs e) {
        isOnNormalGame = false;
    }

    private void MainCanvas_OnTasksButtonClick(object sender, EventArgs e) {
        isOnNormalGame = false;
    }

    private void MainCanvas_OnGoToNormalGameplayClick(object sender, EventArgs e) {
        isOnNormalGame = true;
    }

    private void MainCanvas_OnGoToUpgradeClick(object sender, EventArgs e) {
        isOnNormalGame = false;
    }

    private void InputManager_OnClickOrTouch(object sender, System.EventArgs e) {
        
        //get the raycasthit
        var rayCastHit = Physics2D.GetRayIntersection(lookCamera.ScreenPointToRay(InputManager.Instance.GetTapPosition()));

        if (rayCastHit) {
            //player hit something
            if (rayCastHit.transform.TryGetComponent(out TradeStation tradeStation)) {
                //the player hit the trade station

                OnTradeStationClick?.Invoke(this, EventArgs.Empty);
            }
        }
        else if(isOnNormalGame) {
            OnNonInteractableObjectClick?.Invoke(this, EventArgs.Empty);
        }
    }

    private void OnDestroy() {
        InputManager.Instance.OnClickOrTouch -= InputManager_OnClickOrTouch;
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnSettigsButtonClick -= MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick -= MainCanvas_OnUpgradeCashButtonClick;
    }
}
