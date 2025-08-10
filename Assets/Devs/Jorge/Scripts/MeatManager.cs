using UnityEngine;

public class MeatManager : MonoBehaviour
{
    public static MeatManager Instance;

    public Transform meatTransform;
    public bool meatIsActive = false;

    void Awake()
    {
        Instance = this;
    }

    public void SetMeat(Transform meat)
    {
        meatTransform = meat;
        meatIsActive = true;
    }

    public void ClearMeat()
    {
        meatTransform = null;
        meatIsActive = false;
    }
}
