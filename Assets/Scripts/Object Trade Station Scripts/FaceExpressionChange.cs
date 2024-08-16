using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class FaceExpressionChange : MonoBehaviour{
    //reference of all sprites
    [SerializeField] private Sprite AngryFace; // index 0
    [SerializeField] private Sprite HappyFace;// index 1
    [SerializeField] private Sprite WaitingFace;// index 2
    [SerializeField] private Sprite SadFace;// index 3

    private SpriteRenderer spriteRenderer;

    private int SpriteIndex;


    //timer variables
    private float MaxTransitionTime;
    private float TransitionTimer;
    private int MaxClicksNeeded;
    private int ClicksCurrently;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        SpriteIndex = 2;
        MaxClicksNeeded = 20;
        MaxTransitionTime = 10f;
        TransitionTimer = 0f;
        ClicksCurrently = 0;

        Player.Instance.OnNonInteractableObjectClick += PLayer_OnNonInteractableObjectClick;
    }

    private void PLayer_OnNonInteractableObjectClick(object sender, System.EventArgs e) {
        ClicksCurrently++;

        if (ClicksCurrently >= MaxClicksNeeded) {
            ClicksCurrently = 0;
            TransitionTimer = 0f;

            SwitchFace(false);
        }
    }

    private void Update() {
        TransitionTimer += Time.deltaTime;

        if(TransitionTimer >= MaxTransitionTime) {
            TransitionTimer = 0f;
            ClicksCurrently = 0;

            SwitchFace(true);
        }
    }

    private void SwitchFace(bool ForwardFace) {
        if (ForwardFace) {
            if(SpriteIndex == 0) {
                //was at angry and gone to happy 
                
                SpriteIndex = 1;

                spriteRenderer.sprite = HappyFace;
            }
            else if(SpriteIndex == 1) {
                //was at happy and gone to waiting

                SpriteIndex = 2;

                spriteRenderer.sprite = WaitingFace;
            }
            else if (SpriteIndex == 2) {
                //was at waiting now gone to sad

                SpriteIndex = 3;

                spriteRenderer.sprite = SadFace;
            }
        }
        else {
            if(SpriteIndex == 3) {
                //was at angry and gone to happy 
                
                SpriteIndex = 2;

                spriteRenderer.sprite = WaitingFace;
            }
            else if(SpriteIndex == 2) {
                //was at happy and gone to waiting

                SpriteIndex = 1;

                spriteRenderer.sprite = HappyFace;
            }
            else if (SpriteIndex == 1) {
                //was at waiting now gone to sad

                SpriteIndex = 0;

                spriteRenderer.sprite = AngryFace;
            }
        }
    }

}
