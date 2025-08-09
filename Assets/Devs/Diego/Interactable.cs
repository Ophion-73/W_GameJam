using UnityEngine;

public class Interactable : MonoBehaviour
{
    public Transform cameraPos;
    public float limitRot;
    public Transform initialRotation;
    [Range(0f,0.5f)]
    public float speed;
    
    [Header("Corrections")]
    public bool flipSign;
    public bool xAxis;


    void Start()
    {
        initialRotation = transform;
    }


    public void Open(float axisModifier)
    {
        float modifiedAxisModifier = flipSign
            ? Mathf.Clamp(-axisModifier * speed , limitRot, 0)
            : Mathf.Clamp(axisModifier * speed, 0, limitRot);

        Vector3 finalEuler;

        if (xAxis)
        {
            finalEuler = new Vector3(modifiedAxisModifier, transform.localEulerAngles.y, transform.localEulerAngles.z);
        }
        else
        {
            finalEuler = new Vector3(transform.localEulerAngles.x, modifiedAxisModifier, transform.localEulerAngles.z);
        }

        transform.localEulerAngles = finalEuler;
    }


    public void Close()
    {
        transform.localEulerAngles = new Vector3(0,0,0);
    }
}
