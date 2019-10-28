using UnityEngine;

public class ActiveAxisRotate : MonoBehaviour {
    public AxisToggle axisToggle;

    public void Rotate(float amount) {
        if (axisToggle == null) {
            Debug.LogError("[ActiveAxisRotate] Need to have axis toggle reference assigned.");
            return;
        }

        Axis axis = axisToggle.ActiveAxis;

        if (axis == Axis.X) {
            transform.Rotate(amount, 0.0f, 0.0f, Space.Self);
        } else if (axis == Axis.Y) {
            transform.Rotate(0.0f, amount, 0.0f, Space.Self);
        } else {
            transform.Rotate(0.0f, 0.0f, amount, Space.Self);
        }
    }
}