using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeStationAnimation : MonoBehaviour{

    [SerializeField] private ObjectMakeStation makeStation;

    //references for all the different faces
    [SerializeField] private Sprite HappyFace;
    [SerializeField] private Sprite SmileFace;
    [SerializeField] private Sprite WaitFace;
    [SerializeField] private Sprite SleepFace;

    //Face object
    [SerializeField] private GameObject Face;
    private SpriteRenderer spriteRenderer;

    //animator
    private Animator animator;

    private const string SlowedWorkingString = "IsSlowWorking";
    private const string WorkingString = "IsWorking";
    private const string IdleString = "isIdle";

    private void Awake() {
        animator = GetComponent<Animator>();
        spriteRenderer = Face.GetComponent<SpriteRenderer>();
    }
    private void Start() {
        animator.keepAnimatorStateOnDisable = true;

        makeStation.OnStateChanged += MakeStation_OnStateChanged;
    }

    private void MakeStation_OnStateChanged(object sender, ObjectMakeStation.OnStateChagedEventArgs e) {
        if(e.state == ObjectMakeStation.WorkingStates.Working) {
            spriteRenderer.sprite = SmileFace;
            animator.SetBool(WorkingString,true);
            animator.SetBool(SlowedWorkingString,false);
            animator.SetBool(IdleString,false);
        }
        else if (e.state == ObjectMakeStation.WorkingStates.Sleeping) {
            spriteRenderer.sprite = SleepFace;
            animator.SetBool(WorkingString, false);
            animator.SetBool(SlowedWorkingString, false);
            animator.SetBool(IdleString, true);
        }
        else if (e.state == ObjectMakeStation.WorkingStates.Slowed) {
            spriteRenderer.sprite = HappyFace;
            animator.SetBool(WorkingString, false);
            animator.SetBool(SlowedWorkingString, true);
            animator.SetBool(IdleString, false);
        }
        else if (e.state == ObjectMakeStation.WorkingStates.Waiting) {
            spriteRenderer.sprite = WaitFace;
            animator.SetBool(WorkingString, false);
            animator.SetBool(SlowedWorkingString, false);
            animator.SetBool(IdleString, true);
        }

    }

    private void OnDestroy() {
        makeStation.OnStateChanged -= MakeStation_OnStateChanged;
    }
}
