using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandAnimation : MonoBehaviour{

    private const string LeftHandTriger = "LeftHandTrigger";
    private const string RightHandTriger = "RightHandTrigger";

    //reference of Animator
    private Animator animator;

    public static event EventHandler OnTapSoundPlay;

    //bool to see if animation should play
    private bool ShouldPlayAnimation;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start() {
        ShouldPlayAnimation = true;
        Player.Instance.OnNonInteractableObjectClick += Player_OnNonInteractableObjectClick;

        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick += MainCanvas_OnGoToUpgradeClick;
    }

    private void MainCanvas_OnGoToUpgradeClick(object sender, System.EventArgs e) {
        ShouldPlayAnimation = false;
    }

    private void MainCanvas_OnGoToNormalGameplayClick(object sender, System.EventArgs e) {
        ShouldPlayAnimation = true;
    }

    private void Player_OnNonInteractableObjectClick(object sender, System.EventArgs e) {
        if (ShouldPlayAnimation) {
            bool isLeftHand = UnityEngine.Random.Range(0, 2) <= 0.5f ? true : false;

            if (isLeftHand) {
                animator.SetTrigger(LeftHandTriger);
            }
            else {
                animator.SetTrigger(RightHandTriger);
            }

            OnTapSoundPlay?.Invoke(this, EventArgs.Empty);
        }
    }


    private void OnDestroy() {
        Player.Instance.OnNonInteractableObjectClick -= Player_OnNonInteractableObjectClick;

        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MainCanvas_OnGoToUpgradeClick;
    }
}
