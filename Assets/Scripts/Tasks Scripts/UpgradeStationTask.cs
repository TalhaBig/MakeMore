using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeStationTask : MonoBehaviour,IHasProgress{


    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private Tasks tasks;

    //reference of Text to update
    [SerializeField] private TextMeshProUGUI ShowingText;

    private void Start() {
        ShowingText.text = tasks.GetCurrentValue(Tasks.DifferentTasks.UpgradeStationTask).ToString() + "/" + tasks.GetMaxValue(Tasks.DifferentTasks.UpgradeStationTask).ToString();
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progress = tasks.GetCurrentValue(Tasks.DifferentTasks.UpgradeStationTask) /tasks.GetMaxValue(Tasks.DifferentTasks.UpgradeStationTask) });

        tasks.OnUpgradeStationTaskUpdate += Tasks_OnUpgradeStationTaskUpdate;
    }

    private void Tasks_OnUpgradeStationTaskUpdate(object sender, Tasks.OnTaskUpdateEventArgs e) {
        ShowingText.text = tasks.GetCurrentValue(Tasks.DifferentTasks.UpgradeStationTask).ToString() + "/" + tasks.GetMaxValue(Tasks.DifferentTasks.UpgradeStationTask).ToString();

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progress = e.CurrentValue / e.MaxValue });
    }

    private void OnDestroy() {
        tasks.OnUpgradeStationTaskUpdate -= Tasks_OnUpgradeStationTaskUpdate;
    }
}
