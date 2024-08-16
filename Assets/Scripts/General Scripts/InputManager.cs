using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


[DefaultExecutionOrder(-1)]
public class InputManager : MonoBehaviour{
    //Singleton
    public static InputManager Instance { get; private set;}


    //events to send the Click input And posiiton
    public event EventHandler OnClickOrTouch;


    private TouchClickInputActions inputActions;


    private void Awake() {
        Instance = this;

        inputActions = new TouchClickInputActions();

        inputActions.TouchClick.Enable();
    }

    private void OnDestroy() {
        inputActions.TouchClick.TapContact.performed -= TapContact_performed;

        inputActions.Dispose();
    }


    private void Start() {
        inputActions.TouchClick.TapContact.performed += TapContact_performed;
    }

    private void TapContact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj) {
        OnClickOrTouch?.Invoke(this, EventArgs.Empty);
    }

    //get position function
    public Vector2 GetTapPosition() {
        return inputActions.TouchClick.TapPosition.ReadValue<Vector2>();
    }


}
