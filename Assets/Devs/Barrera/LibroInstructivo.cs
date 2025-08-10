using UnityEngine;
using UnityEngine.UI;

public class LibroInstructivo : MonoBehaviour
{
    public Material[] paginas;
    public Renderer libroRenderer; // Renderer del libro
    public bool libroAbierto = false;
    private int indicePagina = 0;
    public Button[] botones; // Botones de UI

    void Start()
    {
        libroAbierto = false;
        if (paginas.Length > 0)
        {
            libroRenderer.material = paginas[0];
        }

        libroRenderer.gameObject.SetActive(false);

        foreach (Button boton in botones)
        {
            boton.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (!libroAbierto)
            {
                AbrirLibro();
            }
            else
            {
                CerrarLibro();
            }
        }
    }

    public void AbrirLibro()
    {
        libroRenderer.gameObject.SetActive(true);
        libroAbierto = true;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        foreach (Button boton in botones)
        {
            boton.gameObject.SetActive(true);
        }
    }

    public void Pasarpagina()
    {
        if (paginas.Length == 0) return;
        indicePagina++;
        if (indicePagina >= paginas.Length) indicePagina = 0;
        libroRenderer.material = paginas[indicePagina];
    }

    public void PaginaAnterior()
    {
        if (paginas.Length == 0) return;
        indicePagina--;
        if (indicePagina < 0) indicePagina = paginas.Length - 1;
        libroRenderer.material = paginas[indicePagina];
    }

    public void CerrarLibro()
    {
        libroRenderer.gameObject.SetActive(false);
        libroAbierto = false;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }
}
