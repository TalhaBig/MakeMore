using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class Tasks : MonoBehaviour{
    //strings for Counts of different tasks
    private readonly string[] CountPrefsStrings = { "TradeCount", "ObjectUpgradeCount" , "TapCount" , "StationUpgradeCount" , "MoneyRequired" };

    //strings for timers
    private readonly string[] TimerPrefsStrings = { "TradeTimer" , "ObjectUpgradeTimer" , "TapTimer" , "StationUpgradeTimer" , "MoneyTimer" };

    //string for availablilty
    private readonly string[] AvailablePrefsStrings = { "TradeAvailable", "ObjectUpgradeAvailable", "TapAvailable", "StationUpgradeAvailable", "MoneyAvailable" };

    //strings for completion
    private readonly string[] CompletionPrefsStrings = { "TradeCompletion", "ObjectUpgradeCompletion", "TapCompletion", "StationUpgradeCompletion", "MoneyCompletion" };

    private int TotalTasks;

    //arrays of tasks
    private string[] TasksTexts = {"Trade","Upgrade object","Tap","Upgrade station","Get money"};

    //array of times
    private float[] TaskTimes = {0f,0f,0f,0f,0f};

    //array of bools to see if a task is completed
    private bool[] TaskCompleted = {false,false,false,false,false};

    //array of bools to see if the task is available
    private bool[] TaskAvailability = {true,true,true,true,true};

    private float MaxTime;

    //task ui reference
    [SerializeField] private TasksUI taskUI;

    //variables for each task

    //tap task variables
    private int PlayerTapsCount;
    private int PlayerTapCountMax;
    private bool isOnNormalGameplay;

    //trade task variables
    private int TradeCount;
    private int TradeCountMax;

    //upgrad station variables
    private int StationUpgradeCount;
    private int StationUpgradeCountMax;

    //Get money task variables
    private float MoneyRequired;

    //Upgrade Object Tasks
    private int ObjectUpgradeCount;
    private int ObjectUpgradeCountMax;

    //enum for different tasks
    public enum DifferentTasks {
        TradeTask,
        UpgradeObjectTask,
        TapTask,
        UpgradeStationTask,
        GetMoneyTask,
    }
    
    public class OnTaskCompleteEventArgs : EventArgs { public DifferentTasks taskComplete; }
    public event EventHandler<OnTaskCompleteEventArgs> OnTaskComplete;

    //event to send the tasks ui that it has done its work for task completion
    public event EventHandler OnTaskCompletionChange;


    //events for different tasks to update UI
    public class OnTaskUpdateEventArgs : EventArgs { public float CurrentValue; public float MaxValue; }

    public event EventHandler<OnTaskUpdateEventArgs> OnPlayerClickTaskUpdate;
    public event EventHandler<OnTaskUpdateEventArgs> OnGetMoneyTaskUpdate;
    public event EventHandler<OnTaskUpdateEventArgs> OnUpgradeStationTaskUpdate;
    public event EventHandler<OnTaskUpdateEventArgs> OnTradeTaskUpdate;
    public event EventHandler<OnTaskUpdateEventArgs> OnUpgradeObjectTaskUpdate;


    //event for Timer of tasks

    public class OnTaskTimerChangeEventArgs : EventArgs { public float timer; public float MaxTime; public DifferentTasks task; }
    public event EventHandler<OnTaskTimerChangeEventArgs> OnTaskTimerChange;

    private void Awake() {
        TotalTasks = 5;
        MaxTime = 5f;

        PlayerTapsCount = PlayerPrefs.GetInt(CountPrefsStrings[2],0);
        PlayerTapCountMax = 5;

        OnPlayerClickTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = PlayerTapsCount, MaxValue = PlayerTapCountMax });

        TradeCount = PlayerPrefs.GetInt(CountPrefsStrings[0],0);
        TradeCountMax = 5;

        OnTradeTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { MaxValue = TradeCountMax, CurrentValue = TradeCount });

        StationUpgradeCount = PlayerPrefs.GetInt(CountPrefsStrings[3],0);
        StationUpgradeCountMax = 2;

        OnUpgradeStationTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = StationUpgradeCount, MaxValue = StationUpgradeCountMax });

        MoneyRequired = PlayerPrefs.GetFloat(CountPrefsStrings[4],5000f);
        OnGetMoneyTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = NormalMoney.Instance.GetMoney(), MaxValue = MoneyRequired });

        ObjectUpgradeCount = PlayerPrefs.GetInt(CountPrefsStrings[1],0);
        ObjectUpgradeCountMax = 4;

        OnUpgradeObjectTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = ObjectUpgradeCount, MaxValue = ObjectUpgradeCountMax });


        //timers getting from player Prefs
        TaskTimes[0] = PlayerPrefs.GetFloat(TimerPrefsStrings[0],0f);
        TaskTimes[1] = PlayerPrefs.GetFloat(TimerPrefsStrings[1],0f);
        TaskTimes[2] = PlayerPrefs.GetFloat(TimerPrefsStrings[2],0f);
        TaskTimes[3] = PlayerPrefs.GetFloat(TimerPrefsStrings[3],0f);
        TaskTimes[4] = PlayerPrefs.GetFloat(TimerPrefsStrings[4],0f);

        //add the time it took to open the game again
        TaskTimes[0] += TimeSaveDelta.Instance.GetDeltaTime();
        TaskTimes[1] += TimeSaveDelta.Instance.GetDeltaTime();
        TaskTimes[2] += TimeSaveDelta.Instance.GetDeltaTime();
        TaskTimes[3] += TimeSaveDelta.Instance.GetDeltaTime();
        TaskTimes[4] += TimeSaveDelta.Instance.GetDeltaTime();

        OnTaskTimerChange?.Invoke(this, new OnTaskTimerChangeEventArgs { MaxTime = MaxTime, timer = TaskTimes[0], task = DifferentTasks.TradeTask});
        OnTaskTimerChange?.Invoke(this, new OnTaskTimerChangeEventArgs { MaxTime = MaxTime, timer = TaskTimes[1], task = DifferentTasks.UpgradeObjectTask});
        OnTaskTimerChange?.Invoke(this, new OnTaskTimerChangeEventArgs { MaxTime = MaxTime, timer = TaskTimes[2], task = DifferentTasks.TapTask});
        OnTaskTimerChange?.Invoke(this, new OnTaskTimerChangeEventArgs { MaxTime = MaxTime, timer = TaskTimes[3], task = DifferentTasks.UpgradeStationTask});
        OnTaskTimerChange?.Invoke(this, new OnTaskTimerChangeEventArgs { MaxTime = MaxTime, timer = TaskTimes[4], task = DifferentTasks.GetMoneyTask});

        //completion getting from player prefs
        TaskCompleted[0] = PlayerPrefs.GetInt(CompletionPrefsStrings[0], 0) == 1;
        TaskCompleted[1] = PlayerPrefs.GetInt(CompletionPrefsStrings[1], 0) == 1;
        TaskCompleted[2] = PlayerPrefs.GetInt(CompletionPrefsStrings[2], 0) == 1;
        TaskCompleted[3] = PlayerPrefs.GetInt(CompletionPrefsStrings[3], 0) == 1 ;
        TaskCompleted[4] = PlayerPrefs.GetInt(CompletionPrefsStrings[4], 0) == 1;

        // available getting from player prefs
        TaskAvailability[0] = PlayerPrefs.GetInt(AvailablePrefsStrings[0], 0) == 0;
        TaskAvailability[1] = PlayerPrefs.GetInt(AvailablePrefsStrings[1], 0) == 0;
        TaskAvailability[2] = PlayerPrefs.GetInt(AvailablePrefsStrings[2], 0) == 0;
        TaskAvailability[3] = PlayerPrefs.GetInt(AvailablePrefsStrings[3], 0) == 0;
        TaskAvailability[4] = PlayerPrefs.GetInt(AvailablePrefsStrings[4], 0) == 0;
    }

    private void Start() {
        taskUI.OnTaskMoneyReceived += TaskUI_OnTaskMoneyReceived;

        Player.Instance.OnNonInteractableObjectClick += Player_OnNonInteractableObjectClick;
        TradeStation.Instance.OnTradeForMoney += TradeStation_OnTradeForMoney;
        ObjectMakeStation.OnAnyUpgrade += ObjectMakeStation_OnUpgrade;
        NormalMoney.Instance.OnMoneyChanged += NormalMoney_OnMoneyChanged;
        TradeStation.Instance.OnObjectUpgraded += TradeStation_OnObjectUpgraded;    

        MainCanvasUI.Instance.OnGoToNormalGameplayClick += MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick += MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnSettigsButtonClick += MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick += MainCanvas_OnUpgradeCashButtonClick;
    }

    private void TradeStation_OnObjectUpgraded(object sender, TradeStation.OnObjectUpgradedEventArgs e) {
        if (TaskAvailability[GetIndex(DifferentTasks.UpgradeObjectTask)]) {

            ObjectUpgradeCount++;

            PlayerPrefs.SetInt(CountPrefsStrings[1],ObjectUpgradeCount);
            PlayerPrefs.Save();

            OnUpgradeObjectTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = ObjectUpgradeCount, MaxValue = ObjectUpgradeCountMax });

            if (ObjectUpgradeCount >= ObjectUpgradeCountMax) {
                //send event
                OnTaskComplete?.Invoke(this, new OnTaskCompleteEventArgs { taskComplete = DifferentTasks.UpgradeObjectTask });

                TaskCompleted[GetIndex(DifferentTasks.UpgradeObjectTask)] = true;

                PlayerPrefs.SetInt(CompletionPrefsStrings[1], 1);
                PlayerPrefs.Save();
            }
        }
    }

    private void MainCanvas_OnUpgradeCashButtonClick(object sender, EventArgs e) {
        isOnNormalGameplay = false;
    }

    private void MainCanvas_OnTasksButtonClick(object sender, EventArgs e) {
        isOnNormalGameplay = false;
    }

    private void MainCanvas_OnGoToUpgradeClick(object sender, EventArgs e) {
        isOnNormalGameplay = false;
    }

    private void MainCanvas_OnGoToNormalGameplayClick(object sender, EventArgs e) {
        isOnNormalGameplay = true;
    }

    private void NormalMoney_OnMoneyChanged(object sender, NormalMoney.OnMoneyChangedEventArgs e) {
        if (TaskAvailability[GetIndex(DifferentTasks.GetMoneyTask)]) {

            OnGetMoneyTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = NormalMoney.Instance.GetMoney(), MaxValue = MoneyRequired });

            if (NormalMoney.Instance.GetMoney() >= MoneyRequired) {
                //send events
                OnTaskComplete?.Invoke(this, new OnTaskCompleteEventArgs { taskComplete = DifferentTasks.GetMoneyTask });

                TaskCompleted[GetIndex(DifferentTasks.GetMoneyTask)] = true;

                PlayerPrefs.SetInt(CompletionPrefsStrings[4],1);
                PlayerPrefs.Save();
            }
        }
    }

    private void ObjectMakeStation_OnUpgrade(object sender, EventArgs e) {
        //check if the task is not alreday done i.e is available
        if (TaskAvailability[GetIndex(DifferentTasks.UpgradeStationTask)]) {

            StationUpgradeCount++;

            PlayerPrefs.SetInt(CountPrefsStrings[3],StationUpgradeCount);
            PlayerPrefs.Save();

            OnUpgradeStationTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = StationUpgradeCount , MaxValue = StationUpgradeCountMax });

            if(StationUpgradeCount >= StationUpgradeCountMax) {
                OnTaskComplete?.Invoke(this, new OnTaskCompleteEventArgs { taskComplete = DifferentTasks.UpgradeStationTask });

                TaskCompleted[GetIndex(DifferentTasks.UpgradeStationTask)] = true;

                PlayerPrefs.SetInt(CompletionPrefsStrings[3],1);
                PlayerPrefs.Save();
            }
        }
    }

    private void TaskUI_OnTaskMoneyReceived(object sender, OnTaskCompleteEventArgs e) {
        //check which task was completed and set the timer accordingly

        TaskAvailability[GetIndex(e.taskComplete)] = false;

        PlayerPrefs.SetInt(AvailablePrefsStrings[GetIndex(e.taskComplete)],0);
        PlayerPrefs.Save();

        if(e.taskComplete == DifferentTasks.TradeTask) {
            TradeCount -= TradeCountMax;

            PlayerPrefs.SetInt(CountPrefsStrings[0],TradeCount);
            PlayerPrefs.Save();

            //if condition to see whether the player has alread done this task so that he can get money 2 times
            if(TradeCount < TradeCountMax) {
                //set task completed to false

                TaskCompleted[GetIndex(DifferentTasks.TradeTask)] = false;

                PlayerPrefs.SetInt(CompletionPrefsStrings[0],0);
                PlayerPrefs.Save();
            }

            OnTradeTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = TradeCount, MaxValue = TradeCountMax });
        }
        else if(e.taskComplete == DifferentTasks.UpgradeObjectTask) {
            //have to save here have to save here have to save here have to save here have to save here have to save herehave to save here have to save herehave to save here have to save here have to save here have to save here have to save here have to save here

            ObjectUpgradeCount -= ObjectUpgradeCountMax;

            PlayerPrefs.SetInt(CountPrefsStrings[1], ObjectUpgradeCount);
            PlayerPrefs.Save();

            //if condition to see whether the player has alread done this task so that he can get money 2 times
            if (ObjectUpgradeCount < ObjectUpgradeCountMax) {
                //set task completed to false

                TaskCompleted[GetIndex(DifferentTasks.UpgradeObjectTask)] = false;

                PlayerPrefs.SetInt(CompletionPrefsStrings[1], 0);
                PlayerPrefs.Save();
            }

            OnUpgradeObjectTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = ObjectUpgradeCount, MaxValue = ObjectUpgradeCountMax});
        }
        else if(e.taskComplete == DifferentTasks.TapTask) {
            PlayerTapsCount -= PlayerTapCountMax;

            PlayerPrefs.SetInt(CountPrefsStrings[2],PlayerTapsCount);
            PlayerPrefs.Save();

            //if condition to see whether the player has alread done this task so that he can get money 2 times
            if (PlayerTapsCount < PlayerTapCountMax) {
                TaskCompleted[GetIndex(DifferentTasks.TapTask)] = false;

                PlayerPrefs.SetInt(CompletionPrefsStrings[2],0);
                PlayerPrefs.Save();
            }

            OnPlayerClickTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = PlayerTapsCount, MaxValue = PlayerTapCountMax });
        }
        else if(e.taskComplete == DifferentTasks.UpgradeStationTask) {

            StationUpgradeCount -= StationUpgradeCountMax;

            PlayerPrefs.SetInt(CountPrefsStrings[3],StationUpgradeCount);
            PlayerPrefs.Save();

            //if condition to see whether the player has alread done this task so that he can get money 2 times
            if (StationUpgradeCount < StationUpgradeCountMax) {
                TaskCompleted[GetIndex(DifferentTasks.UpgradeStationTask)] = false;

                PlayerPrefs.SetInt(CompletionPrefsStrings[3], 0);
                PlayerPrefs.Save();
            }

            OnUpgradeStationTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = StationUpgradeCount, MaxValue = StationUpgradeCountMax });
        }
        else {
            MoneyRequired += 4000f;

            PlayerPrefs.SetFloat(CountPrefsStrings[4],MoneyRequired);
            PlayerPrefs.Save();

            if(NormalMoney.Instance.GetMoney() < MoneyRequired) {
                TaskCompleted[GetIndex(DifferentTasks.GetMoneyTask)] = false;

                PlayerPrefs.SetInt(CompletionPrefsStrings[4], 0);
                PlayerPrefs.Save();
            }

            OnGetMoneyTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = NormalMoney.Instance.GetMoney(), MaxValue = MoneyRequired });
        }

        OnTaskCompletionChange?.Invoke(this,EventArgs.Empty);
    }

    private void TradeStation_OnTradeForMoney(object sender, TradeStation.OnTradeForMoneyEventArgs e) {
        if (TaskAvailability[GetIndex(DifferentTasks.TradeTask)]) {
            //if the task is available then add to the count


            if (e.money > 0) {
                //the trade was for money and not just a simple click

                TradeCount++;

                PlayerPrefs.SetInt(CountPrefsStrings[0],TradeCount);
                PlayerPrefs.Save();

                OnTradeTaskUpdate?.Invoke(this, new OnTaskUpdateEventArgs { CurrentValue = TradeCount, MaxValue = TradeCountMax });

                if (TradeCount >= TradeCountMax) {
                    OnTaskComplete?.Invoke(this, new OnTaskCompleteEventArgs { taskComplete = DifferentTasks.TradeTask });

                    TaskCompleted[GetIndex(DifferentTasks.TradeTask)] = true;

                    PlayerPrefs.SetInt(CompletionPrefsStrings[0],1);
                    PlayerPrefs.Save();
                }
            }
        }
    }

    private void Player_OnNonInteractableObjectClick(object sender, EventArgs e) {
        if (isOnNormalGameplay) {
            if (TaskAvailability[GetIndex(DifferentTasks.TapTask)]) {

                PlayerTapsCount++;
                PlayerPrefs.SetInt(CountPrefsStrings[2],PlayerTapsCount);
                PlayerPrefs.Save();

                OnPlayerClickTaskUpdate.Invoke(this, new OnTaskUpdateEventArgs { MaxValue = PlayerTapCountMax, CurrentValue = PlayerTapsCount });

                if (PlayerTapsCount >= PlayerTapCountMax) {
                    //send event
                    OnTaskComplete?.Invoke(this, new OnTaskCompleteEventArgs { taskComplete = DifferentTasks.TapTask });

                    TaskCompleted[GetIndex(DifferentTasks.TapTask)] = true;

                    PlayerPrefs.SetInt(CompletionPrefsStrings[2], 1);
                    PlayerPrefs.Save();
                }
            }
        }
    }

    public string GetTaskText(DifferentTasks task) {
        switch(task){
            case DifferentTasks.TradeTask:
                return TasksTexts[0];
            case DifferentTasks.UpgradeObjectTask:
                return TasksTexts[1];
            case DifferentTasks.TapTask:
                return TasksTexts[2];
            case DifferentTasks.UpgradeStationTask:
                return TasksTexts[3];
            case DifferentTasks.GetMoneyTask:
                return TasksTexts[4];
            default:
                return null;
        }
    }
    
    public bool GetTaskCompletion( DifferentTasks task) {
        //see which task is completed and available
        
        switch (task) {
            case DifferentTasks.TradeTask:
                return TaskCompleted[0] && TaskAvailability[0];
            case DifferentTasks.UpgradeObjectTask:
                return TaskCompleted[1] && TaskAvailability[1];
            case DifferentTasks.TapTask:
                return TaskCompleted[2] && TaskAvailability[2];
            case DifferentTasks.UpgradeStationTask:
                return TaskCompleted[3] && TaskAvailability[3];
            case DifferentTasks.GetMoneyTask:
                return TaskCompleted[4] && TaskAvailability[4];
            default:
                return false;
        }
    }

    private int GetIndex(DifferentTasks task) {
        switch (task) {
            case DifferentTasks.TradeTask:
                return 0;
            case DifferentTasks.UpgradeObjectTask:
                return 1;
            case DifferentTasks.TapTask:
                return 2;
            case DifferentTasks.UpgradeStationTask:
                return 3;
            case DifferentTasks.GetMoneyTask:
                return 4;
            default:
                return 0;
        }
    }


    //function for different tasks to get the values at the start

    public float GetCurrentValue(DifferentTasks task) {
        switch (task) {
            case DifferentTasks.TradeTask:
                return TradeCount;
            case DifferentTasks.TapTask:
                return PlayerTapsCount;
            case DifferentTasks.GetMoneyTask:
                return NormalMoney.Instance.GetMoney();
            case DifferentTasks.UpgradeStationTask:
                return StationUpgradeCount;
            case DifferentTasks.UpgradeObjectTask:
                return ObjectUpgradeCount;
            default:
                return 0f;
        }
    }

    public float GetMaxValue(DifferentTasks task) {
        switch (task) {
            case DifferentTasks.TradeTask:
                return TradeCountMax;
            case DifferentTasks.TapTask:
                return PlayerTapCountMax;
            case DifferentTasks.GetMoneyTask:
                return MoneyRequired;
            case DifferentTasks.UpgradeStationTask:
                return StationUpgradeCountMax;
            case DifferentTasks.UpgradeObjectTask:
                return ObjectUpgradeCountMax;
            default:
                return 0f;
        }
    }

    private void Update() {
        //increase task timers based on if they are available
        for (int i = 0; i < TotalTasks; i++) {
            DifferentTasks currentTask;

            switch (i) {
                case 0:
                    currentTask = DifferentTasks.TradeTask;
                    break;
                case 1:
                    currentTask = DifferentTasks.UpgradeObjectTask;
                    break;
                case 2:
                    currentTask = DifferentTasks.TapTask;
                    break;
                case 3:
                    currentTask = DifferentTasks.UpgradeStationTask;
                    break;
                case 4:
                    currentTask = DifferentTasks.GetMoneyTask;
                    break;
                default:
                    currentTask = DifferentTasks.TradeTask;
                    break;
            }

            if (!TaskAvailability[i]) {
                TaskTimes[i] += Time.deltaTime;
                PlayerPrefs.SetFloat(TimerPrefsStrings[i], TaskTimes[i]);

                OnTaskTimerChange?.Invoke(this, new OnTaskTimerChangeEventArgs { timer = TaskTimes[i], MaxTime = MaxTime, task = currentTask });

                if (TaskTimes[i] > MaxTime) {
                    //task available again
                    TaskTimes[i] = 0f;

                    TaskAvailability[i] = true;
                    PlayerPrefs.SetFloat(TimerPrefsStrings[i], TaskTimes[i]);

                    OnTaskCompletionChange?.Invoke(this, EventArgs.Empty);
                }
            }
            else {
                //task is available

                OnTaskTimerChange?.Invoke(this, new OnTaskTimerChangeEventArgs { timer = MaxTime + 1, MaxTime = MaxTime, task = currentTask });
            }
        }

        PlayerPrefs.Save();
    }

    private void OnDestroy() {
        taskUI.OnTaskMoneyReceived -= TaskUI_OnTaskMoneyReceived;

        Player.Instance.OnNonInteractableObjectClick -= Player_OnNonInteractableObjectClick;
        TradeStation.Instance.OnTradeForMoney -= TradeStation_OnTradeForMoney;
        ObjectMakeStation.OnAnyUpgrade -= ObjectMakeStation_OnUpgrade;
        NormalMoney.Instance.OnMoneyChanged -= NormalMoney_OnMoneyChanged;

        MainCanvasUI.Instance.OnGoToNormalGameplayClick -= MainCanvas_OnGoToNormalGameplayClick;
        MainCanvasUI.Instance.OnGoToUpgradeClick -= MainCanvas_OnGoToUpgradeClick;
        MainCanvasUI.Instance.OnSettigsButtonClick -= MainCanvas_OnTasksButtonClick;
        MainCanvasUI.Instance.OnUpgradeCashButtonClick -= MainCanvas_OnUpgradeCashButtonClick;
    }

}
