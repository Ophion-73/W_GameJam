using UnityEngine;
using UnityEngine.UI;

public class InventoryBar : MonoBehaviour
{
    public RawImage[] Slots;
    private int CurrentSlot = 0;

    public InventoryManageer IM;

    void Start()
    {
        UpdateSlot();
    }

    void Update()
    {
        float Scroll = Input.GetAxis("Mouse ScrollWheel");

        if (Scroll < 0f)
        {
            CurrentSlot--;
            if (CurrentSlot < 0)
                CurrentSlot = Slots.Length - 1;
            UpdateSlot();
        }
        else if (Scroll > 0f)
        {
            CurrentSlot++;
            if (CurrentSlot >= Slots.Length)
                CurrentSlot = 0;
            UpdateSlot();
        }
    }

    void UpdateSlot()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            if (i == CurrentSlot)
            {
                Slots[i].color = Color.yellow;
                IM.ChangeActiveItem(CurrentSlot);
            }
            else
                Slots[i].color = Color.white;
        }
    }
}
