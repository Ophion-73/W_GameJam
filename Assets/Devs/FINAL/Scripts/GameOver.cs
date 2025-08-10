using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public InventoryManageer IM;
    public Controller controller;
    public MouseLook ML;
    public GameObject PanelGanaste;

    void Start()
    {
        PanelGanaste.SetActive(false);
    }

    void Update()
    {
        GanasteCheck();
    }

    public void GanasteCheck()
    {
        if(IM.ContMimics == 0)
        {
            PanelGanaste.SetActive(true);
            IM.canUse = false;
            controller.isInteracting = true;
            ML.isInteracting = true;
        }
    }

    public void RegresoInicio()
    {
        SceneManager.LoadScene("Inicio");
    }
}
