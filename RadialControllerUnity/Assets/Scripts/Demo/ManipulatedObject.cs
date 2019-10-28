using UnityEngine;

public class ManipulatedObject : MonoBehaviour {
    public AxisToggle axisToggle;
    public ScaleToggle scaleToggle;
    public float scaleSpeed = 1.0f;
    public float maxScale = 3.0f;
    public float minScale = 0.5f;

    private float curScale = 1.0f;

    public void Manipulate(float amount) {

        if (scaleToggle.ScaleEnabled) {
            Scale(amount);
        } else {
            Rotate(amount);
        }
    }

    private void Rotate(float amount) {
        Axis axis = axisToggle.ActiveAxis;

        if (axis == Axis.X) {
            transform.Rotate(amount, 0.0f, 0.0f, Space.Self);
        } else if (axis == Axis.Y) {
            transform.Rotate(0.0f, amount, 0.0f, Space.Self);
        } else {
            transform.Rotate(0.0f, 0.0f, amount, Space.Self);
        }
    }

    private void Scale(float amount) {
        curScale += amount * (scaleSpeed * Time.deltaTime);
        curScale = Mathf.Clamp(curScale, minScale, maxScale);

        transform.localScale = new Vector3(curScale, curScale, curScale);
    }
}