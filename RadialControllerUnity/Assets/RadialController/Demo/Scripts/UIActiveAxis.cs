using UnityEngine;
using UnityEngine.UI;

public class UIActiveAxis : MonoBehaviour {
    public Axis axis;
    public AxisToggle axisToggle;
    public Image enabledImage;
    public Image disabledImage;

    private void OnEnable() {
        UpdateDisplay(axisToggle.ActiveAxis);
        axisToggle.onAxisChanged.AddListener(UpdateDisplay);
    }

    private void OnDisable() {
        axisToggle.onAxisChanged.RemoveListener(UpdateDisplay);
    }

    public void UpdateDisplay(Axis activeAxis) {
        enabledImage.gameObject.SetActive(axis == activeAxis);
        disabledImage.gameObject.SetActive(axis != activeAxis);
    }
}