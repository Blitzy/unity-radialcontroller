using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AxisToggle : MonoBehaviour {

    public AxisEvent onAxisChanged;

    public Axis ActiveAxis { get; private set; }

    public void Toggle() { 
        if (ActiveAxis == Axis.X) {
            ActiveAxis = Axis.Y;
        } else if (ActiveAxis == Axis.Y) {
            ActiveAxis = Axis.Z;
        } else {
            ActiveAxis = Axis.X;
        }

        if (onAxisChanged != null) {
            onAxisChanged.Invoke(ActiveAxis);
        }
    }
}

[System.Serializable]
public class AxisEvent : UnityEvent<Axis> {
}
