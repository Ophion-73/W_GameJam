using UnityEngine;

public class RandomVariations : MonoBehaviour
{
    [Header("Variations Scripts")]
    public MonoBehaviour[] Variations;

    void Start()
    {
        if (Variations == null || Variations.Length == 0)
        {
            Debug.LogWarning("No hay scripts");
            return;
        }

        foreach (var v in Variations)
        {
            if (v != null)
                v.enabled = false;
        }

        int RandomScript = Random.Range(0, Variations.Length);
        if (Variations[RandomScript] != null)
        {
            Variations[RandomScript].enabled = true;
        }
    }
}
