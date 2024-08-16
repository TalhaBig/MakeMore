using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeObjectTask : MonoBehaviour,IHasProgress{


    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private Tasks tasks;

    //reference of Text to update
    [SerializeField] private TextMeshProUGUI ShowingText;
    
    private void Start() {
        ShowingText.text = tasks.GetCurrentValue(Tasks.DifferentTasks.UpgradeObjectTask).ToString() + "/"  + tasks.GetMaxValue(Tasks.DifferentTasks.UpgradeObjectTask).ToString();

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progress =  tasks.GetCurrentValue(Tasks.DifferentTasks.UpgradeObjectTask) / tasks.GetMaxValue(Tasks.DifferentTasks.UpgradeObjectTask) });


        tasks.OnUpgradeObjectTaskUpdate += Tasks_OnUpgradeObjectTaskUpdate;
    }

    private void Tasks_OnUpgradeObjectTaskUpdate(object sender, Tasks.OnTaskUpdateEventArgs e) {
        ShowingText.text = tasks.GetCurrentValue(Tasks.DifferentTasks.UpgradeObjectTask).ToString() + "/" + tasks.GetMaxValue(Tasks.DifferentTasks.UpgradeObjectTask).ToString();

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progress = e.CurrentValue / e.MaxValue });
    }

    private void OnDestroy() {
        tasks.OnUpgradeObjectTaskUpdate -= Tasks_OnUpgradeObjectTaskUpdate;
    }
}
