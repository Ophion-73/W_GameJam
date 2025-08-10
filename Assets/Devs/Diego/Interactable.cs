using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform cameraPos;
    public float limitRot;
    public Transform initialRotation;
    [Range(0f, 0.5f)]
    public float speed;

    [Header("Threshold")]
    public float threshHoldAngle; // Can be positive or negative
    private bool hasPassedThreshold = false;

    [Header("Corrections")]
    public bool flipSign;
    public bool xAxis; // true = X axis, false = Y axis

    [Header("Mimic Behavior")]
    public bool isMimic;

    void Start()
    {
        initialRotation = transform;
    }

    public void Open(float axisModifier)
    {
        // Clamp movement
        float modifiedAxisModifier = flipSign
            ? Mathf.Clamp(-axisModifier * speed, limitRot, 0)
            : Mathf.Clamp(axisModifier * speed, 0, limitRot);

        // Apply rotation on chosen axis
        Vector3 finalEuler = transform.localEulerAngles;
        if (xAxis)
            finalEuler.x = modifiedAxisModifier;
        else
            finalEuler.y = modifiedAxisModifier;

        transform.localEulerAngles = finalEuler;

        // Check threshold after rotation
        CheckThreshold();
    }

    public void Close()
    {
        transform.localEulerAngles = Vector3.zero;
        hasPassedThreshold = false; // reset
    }

    private void CheckThreshold()
    {
        float currentAngle = xAxis ? transform.localEulerAngles.x : transform.localEulerAngles.y;
        float normalizedAngle = NormalizeAngle(currentAngle);

        if (!hasPassedThreshold)
        {
            if (threshHoldAngle >= 0 && normalizedAngle >= threshHoldAngle)
            {
                TriggerThreshold();
            }
            else if (threshHoldAngle < 0 && normalizedAngle <= threshHoldAngle)
            {
                TriggerThreshold();
            }
        }
    }

    private void TriggerThreshold()
    {
        hasPassedThreshold = true;
        if (isMimic)
        {
            // jumpscare logic here
            Debug.Log("espantamiento");
        }
    }

    // Converts 0–360° to -180–180° range
    private float NormalizeAngle(float angle)
    {
        if (angle > 180f) angle -= 360f;
        return angle;
    }
}
