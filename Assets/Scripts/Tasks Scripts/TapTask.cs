using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TapTask : MonoBehaviour,IHasProgress{

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField] private Tasks tasks;

    //reference of Text to update
    [SerializeField] private TextMeshProUGUI ShowingText;

    private void Start() {
        ShowingText.text = tasks.GetCurrentValue(Tasks.DifferentTasks.TapTask).ToString() + "/" + tasks.GetMaxValue(Tasks.DifferentTasks.TapTask).ToString();
        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progress = tasks.GetCurrentValue(Tasks.DifferentTasks.TradeTask) /tasks.GetMaxValue(Tasks.DifferentTasks.TapTask) });

        tasks.OnPlayerClickTaskUpdate += Tasks_OnPlayerClickTaskUpdate;
    }

    private void Tasks_OnPlayerClickTaskUpdate(object sender, Tasks.OnTaskUpdateEventArgs e) {
        ShowingText.text = tasks.GetCurrentValue(Tasks.DifferentTasks.TapTask).ToString() + "/" + tasks.GetMaxValue(Tasks.DifferentTasks.TapTask).ToString();

        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs { progress = e.CurrentValue / e.MaxValue });
    }

    private void OnDestroy() {
        tasks.OnPlayerClickTaskUpdate -= Tasks_OnPlayerClickTaskUpdate;
    }
}
