using UnityEngine;
using UnityEngine.Rendering;

public class FlashBangEffect : MonoBehaviour
{
    public Volume volume;
    public CanvasGroup alphaController;  
    public AudioSource bang;

    public GameObject flashVisual;   
    public GameObject imageVisual;   

    private bool on = false;
    private bool fadingInFlash = true;
    private bool fadingOutFlash = false;
    private bool fadingInImage = false;

    public float fadeSpeed = 2f;

    void Start()
    {
        alphaController.alpha = 0;
        volume.weight = 0;

        flashVisual.SetActive(false);
        imageVisual.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            FlashBanged();
        }

        if (on)
        {
            if (fadingInFlash)
            {
                flashVisual.SetActive(true);
                imageVisual.SetActive(false);

                alphaController.alpha += Time.deltaTime * fadeSpeed;
                volume.weight += Time.deltaTime * fadeSpeed;

                if (alphaController.alpha >= 1)
                {
                    alphaController.alpha = 1;
                    volume.weight = 1;

                    fadingInFlash = false;
                    fadingOutFlash = true;
                }
            }
            else if (fadingOutFlash)
            {
                alphaController.alpha -= Time.deltaTime * fadeSpeed;
                volume.weight -= Time.deltaTime * fadeSpeed;

                if (alphaController.alpha <= 0)
                {
                    alphaController.alpha = 0;
                    volume.weight = 0;
                    fadingOutFlash = false;


                    flashVisual.SetActive(false);
                    imageVisual.SetActive(true);

                    fadingInImage = true;
                }
            }
            else if (fadingInImage)
            {
                alphaController.alpha += Time.deltaTime * fadeSpeed;

                if (alphaController.alpha >= 1)
                {
                    alphaController.alpha = 1;
                    fadingInImage = false;
                    on = false; 
                }
            }
        }
    }

    public void FlashBanged()
    {
        if (on) return;

        on = true;
        fadingInFlash = true;
        fadingOutFlash = false;
        fadingInImage = false;

        alphaController.alpha = 0;
        volume.weight = 0;
        bang.Play();
    }
}
