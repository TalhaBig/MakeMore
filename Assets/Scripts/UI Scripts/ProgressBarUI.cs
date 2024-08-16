using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    //reference of gameObject that has IHasProgess
    [SerializeField] private GameObject HasProgressGameObject;

    private IHasProgress hasProgress;

    //reference of image
    [SerializeField] private Image fillImage;

    private void Awake() {
        if (HasProgressGameObject.TryGetComponent(out IHasProgress _hasProgress)) {
            hasProgress = _hasProgress;
        }
        else {
            Debug.LogError("No IHasProgress on GameObject" + HasProgressGameObject);
        }
    }

    private void Start() {
        hasProgress.OnProgressChanged += HasProgress_OnProgressChanged;
    }

    private void HasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        fillImage.fillAmount = e.progress;
    }

    private void OnDestroy() {
        hasProgress.OnProgressChanged -= HasProgress_OnProgressChanged;
    }
}
