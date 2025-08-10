using UnityEngine;
using UnityEngine.Rendering;

public class FlashBangEffect : MonoBehaviour
{
    public Volume volume;
    public CanvasGroup AlphaController;
    public AudioSource bang;

    private bool on = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            FlashBanged();
        }

        if (on)
        {
            AlphaController.alpha -= Time.deltaTime * 2;
            volume.weight -= Time.deltaTime * 2;

            if (AlphaController.alpha <= 0)
            {
                volume.weight = 0;
                AlphaController.alpha = 0;
                on = false;
            }
        }
    }


    public void FlashBanged()
    {
        volume.GetComponent<Volume>().weight = 1;
        on = true;
        bang.Play();
        AlphaController.alpha = 1;
    }
}
