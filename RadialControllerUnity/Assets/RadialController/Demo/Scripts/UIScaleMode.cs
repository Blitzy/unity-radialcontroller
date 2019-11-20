using UnityEngine;
using UnityEngine.UI;

public class UIScaleMode : MonoBehaviour {
    public ScaleToggle scaleToggle;
    public Image enabledImage;
    public Image disabledImage;

    private void OnEnable() {
        UpdateDisplay(scaleToggle.ScaleEnabled);
        scaleToggle.onScaleToggled.AddListener(UpdateDisplay);
    }

    private void OnDisable() {
        scaleToggle.onScaleToggled.RemoveListener(UpdateDisplay);
    }

    public void UpdateDisplay(bool scaleEnabled) {
        enabledImage.gameObject.SetActive(scaleEnabled);
        disabledImage.gameObject.SetActive(!scaleEnabled);
    }
    
}