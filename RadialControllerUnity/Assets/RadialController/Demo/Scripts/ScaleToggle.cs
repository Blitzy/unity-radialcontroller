using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ScaleToggle : MonoBehaviour {

    public BoolEvent onScaleToggled;

    public bool ScaleEnabled { get; private set; }

    public void Toggle() {
        ScaleEnabled = !ScaleEnabled;
        if (onScaleToggled != null) {
            onScaleToggled.Invoke(ScaleEnabled);
        }
    }
}

[System.Serializable]
public class BoolEvent : UnityEvent<bool> {
}
