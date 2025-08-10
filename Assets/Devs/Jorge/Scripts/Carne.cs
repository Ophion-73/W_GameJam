using UnityEngine;

public class Carne : MonoBehaviour
{
    public GameObject meatPrefab;
    public Transform throwPoint;
    public float throwForce = 5f;

    public GameObject currentMeat;

    void Start()
    {
        MeatManager.Instance.ClearMeat();
    }

    void Update()
    {

    }

    public void ThrowMeat()
    {
        if (currentMeat != null) return;

        Vector3 spawnPos = throwPoint.position + transform.forward * 1.5f;
        currentMeat = Instantiate(meatPrefab, spawnPos, Quaternion.identity);

        Rigidbody rb = currentMeat.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(transform.forward * throwForce, ForceMode.Impulse);
        }

        MeatManager.Instance.SetMeat(currentMeat.transform);
    }

    public void RemoveMeat()
    {
        if (currentMeat != null)
        {
            Destroy(currentMeat);
            MeatManager.Instance.ClearMeat();
            currentMeat = null;
        }
    }
}
