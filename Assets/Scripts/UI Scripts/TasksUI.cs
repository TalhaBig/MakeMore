using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TasksUI : MonoBehaviour{

    //references of all buttons
    [SerializeField] private Button TradeTaskButton;
    [SerializeField] private Button UpgradeWorkingObjectTaskButton;
    [SerializeField] private Button TapTaskButton;
    [SerializeField] private Button UpgradeWorkStationTaskButton;
    [SerializeField] private Button GetMoneyTaskButton;

    //references of texts
    [SerializeField] private TextMeshProUGUI TradeTaskText;
    [SerializeField] private TextMeshProUGUI UpgradeWorkingObjectTaskText;
    [SerializeField] private TextMeshProUGUI TapTaskText;
    [SerializeField] private TextMeshProUGUI UpgradeWorkStationTaskText;
    [SerializeField] private TextMeshProUGUI GetMoneyTaskText;

    //timer texts references
    [SerializeField] private TextMeshProUGUI TradeTaskTimer;
    [SerializeField] private TextMeshProUGUI ObjectUpgradeTaskTimer;
    [SerializeField] private TextMeshProUGUI TapTaskTimer;
    [SerializeField] private TextMeshProUGUI StationUpgradeTaskTimer;
    [SerializeField] private TextMeshProUGUI MoneyTaskTimer;
 
    //reference of tasks
    [SerializeField] private Tasks tasks;

    //event to send the tasks script to reset the task
    public event EventHandler<Tasks.OnTaskCompleteEventArgs> OnTaskMoneyReceived;

    private void Awake() {
        TradeTaskButton.onClick.AddListener(() => {
            CashMoney.Instance.IncreaseMoney(2f);

            OnTaskMoneyReceived?.Invoke(this, new Tasks.OnTaskCompleteEventArgs { taskComplete = Tasks.DifferentTasks.TradeTask });
        });
        UpgradeWorkingObjectTaskButton.onClick.AddListener(() => {
            CashMoney.Instance.IncreaseMoney(2f);

            OnTaskMoneyReceived?.Invoke(this, new Tasks.OnTaskCompleteEventArgs { taskComplete = Tasks.DifferentTasks.UpgradeObjectTask });
        });
        TapTaskButton.onClick.AddListener(() => {
            CashMoney.Instance.IncreaseMoney(2f);

            OnTaskMoneyReceived?.Invoke(this, new Tasks.OnTaskCompleteEventArgs { taskComplete = Tasks.DifferentTasks.TapTask });
        });
        UpgradeWorkStationTaskButton.onClick.AddListener(() => {
            CashMoney.Instance.IncreaseMoney(2f);

            OnTaskMoneyReceived?.Invoke(this, new Tasks.OnTaskCompleteEventArgs { taskComplete = Tasks.DifferentTasks.UpgradeStationTask });
        });
        GetMoneyTaskButton.onClick.AddListener(() => {
            CashMoney.Instance.IncreaseMoney(2f);

            OnTaskMoneyReceived?.Invoke(this, new Tasks.OnTaskCompleteEventArgs { taskComplete = Tasks.DifferentTasks.GetMoneyTask });
        });
    }

    private void Start() {
        StartCoroutine(HideDelay());

        SettingsUI.Instance.OnTasksButtonClick += Settings_OnTasksButtonClick;
        SettingsUI.Instance.OnAudioSettingsButtonClick += Settings_OnAudioSettingsButtonClick;

        tasks.OnTaskComplete += Tasks_OnTaskComplete;
        tasks.OnTaskCompletionChange += Tasks_OnTaskCompletionChange;
        tasks.OnTaskTimerChange += Tasks_OnTaskTimerChange;

        //enable or disable buttons
        SetButtonInteractabilityAndImageOpacity();

        //set text
        TradeTaskText.text = tasks.GetTaskText(Tasks.DifferentTasks.TradeTask);
        GetMoneyTaskText.text = tasks.GetTaskText(Tasks.DifferentTasks.GetMoneyTask);
        TapTaskText.text = tasks.GetTaskText(Tasks.DifferentTasks.TapTask);
        UpgradeWorkingObjectTaskText.text = tasks.GetTaskText(Tasks.DifferentTasks.UpgradeObjectTask);
        UpgradeWorkStationTaskText.text = tasks.GetTaskText(Tasks.DifferentTasks.UpgradeStationTask);

        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick += MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnSettigsButtonClick += MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick += MainCanvas_OnAnyButtonClick;
    }

    private void Settings_OnAudioSettingsButtonClick(object sender, EventArgs e) {
        Hide();
    }

    private void MainCanvas_OnAnyButtonClick(object sender, EventArgs e) {
        Hide();
    }

    private void Settings_OnTasksButtonClick(object sender, EventArgs e) {
        Show();
    }

    private void Tasks_OnTaskTimerChange(object sender, Tasks.OnTaskTimerChangeEventArgs e) {

        int e_hours = Mathf.FloorToInt(e.timer / 3600);
        int e_minutes = Mathf.FloorToInt((e.timer % 3600) / 60);
        int e_seconds = Mathf.FloorToInt(e.timer % 60);


        int m_hours = Mathf.FloorToInt(e.MaxTime / 3600);
        int m_minutes = Mathf.FloorToInt((e.MaxTime % 3600) / 60);
        int m_seconds = Mathf.FloorToInt(e.MaxTime % 60);

        //set opacity of timers so that it doesnt mess up the vertical layout group
        if (e.task == Tasks.DifferentTasks.TradeTask) {
            Color color = TradeTaskTimer.color;

            if (e.timer >= e.MaxTime) {
                color.a = 0f;
            }
            else {
                color.a = 1f;
            }

            TradeTaskTimer.color = color;

            TradeTaskTimer.text = string.Format("{0:00}:{1:00}:{2:00}", e_hours, e_minutes, e_seconds) + "/" + string.Format("{0:00}:{1:00}:{2:00}", m_hours, m_minutes, m_seconds);
        }
        else if (e.task == Tasks.DifferentTasks.UpgradeObjectTask) {
            Color color = ObjectUpgradeTaskTimer.color;

            if (e.timer >= e.MaxTime) {
                color.a = 0f;
            }
            else {
                color.a = 1f;
            }

            ObjectUpgradeTaskTimer.color = color;

            ObjectUpgradeTaskTimer.text = string.Format("{0:00}:{1:00}:{2:00}", e_hours, e_minutes, e_seconds) + "/" + string.Format("{0:00}:{1:00}:{2:00}", m_hours, m_minutes, m_seconds);
        }
        else if (e.task == Tasks.DifferentTasks.TapTask) {
            Color color = TapTaskTimer.color;

            if (e.timer >= e.MaxTime) {
                color.a = 0f;
            }
            else {
                color.a = 1f;
            }

            TapTaskTimer.color = color;

            TapTaskTimer.text = string.Format("{0:00}:{1:00}:{2:00}", e_hours, e_minutes, e_seconds) + "/" + string.Format("{0:00}:{1:00}:{2:00}", m_hours, m_minutes, m_seconds);
        }
        else if (e.task == Tasks.DifferentTasks.UpgradeStationTask) {
            Color color = StationUpgradeTaskTimer.color;

            if (e.timer >= e.MaxTime) {
                color.a = 0f;
            }
            else {
                color.a = 1f;
            }

            StationUpgradeTaskTimer.color = color;

            StationUpgradeTaskTimer.text = string.Format("{0:00}:{1:00}:{2:00}", e_hours, e_minutes, e_seconds) + "/" + string.Format("{0:00}:{1:00}:{2:00}", m_hours, m_minutes, m_seconds);
        }
        else {
            Color color = MoneyTaskTimer.color;

            if (e.timer >= e.MaxTime) {
                color.a = 0f;
            }
            else {
                color.a = 1f;
            }

            MoneyTaskTimer.color = color;
            MoneyTaskTimer.text = string.Format("{0:00}:{1:00}:{2:00}", e_hours, e_minutes, e_seconds) + "/" + string.Format("{0:00}:{1:00}:{2:00}", m_hours, m_minutes, m_seconds);
        }
    }

    private void Tasks_OnTaskCompletionChange(object sender, EventArgs e) {
        SetButtonInteractabilityAndImageOpacity();
        
    }

    private void Tasks_OnTaskComplete(object sender, Tasks.OnTaskCompleteEventArgs e) {
        // see which task was completed and set the interactable accordingly
        if(e.taskComplete == Tasks.DifferentTasks.TradeTask) {
            TradeTaskButton.interactable = true;
        }
        else if (e.taskComplete == Tasks.DifferentTasks.UpgradeObjectTask) {
            UpgradeWorkingObjectTaskButton.interactable = true;
        }
        else if(e.taskComplete == Tasks.DifferentTasks.TapTask) {
            TapTaskButton.interactable = true;
        }
        else if(e.taskComplete == Tasks.DifferentTasks.UpgradeStationTask) {
            UpgradeWorkStationTaskButton.interactable = true;
        }
        else {
            GetMoneyTaskButton.interactable = true;
        }
    }
    
    private void Hide() {
        gameObject.SetActive(false);
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private IEnumerator HideDelay() {
        yield return new WaitForSeconds(0.5f);

        Hide();
    }

    private void OnDestroy() {

        tasks.OnTaskComplete -= Tasks_OnTaskComplete;
        tasks.OnTaskCompletionChange -= Tasks_OnTaskCompletionChange;
        tasks.OnTaskTimerChange -= Tasks_OnTaskTimerChange;

        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnSettigsButtonClick -= MainCanvas_OnAnyButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick -= MainCanvas_OnAnyButtonClick;

        SettingsUI.Instance.OnTasksButtonClick -= Settings_OnTasksButtonClick;
        SettingsUI.Instance.OnAudioSettingsButtonClick -= Settings_OnAudioSettingsButtonClick;

    }


    private void SetButtonInteractabilityAndImageOpacity() {
        // set the buttons interactability accordingly
        TradeTaskButton.interactable = tasks.GetTaskCompletion(Tasks.DifferentTasks.TradeTask);
        GetMoneyTaskButton.interactable = tasks.GetTaskCompletion(Tasks.DifferentTasks.GetMoneyTask);
        TapTaskButton.interactable = tasks.GetTaskCompletion(Tasks.DifferentTasks.TapTask);
        UpgradeWorkingObjectTaskButton.interactable = tasks.GetTaskCompletion(Tasks.DifferentTasks.UpgradeObjectTask);
        UpgradeWorkStationTaskButton.interactable = tasks.GetTaskCompletion(Tasks.DifferentTasks.UpgradeStationTask);
    }
}
