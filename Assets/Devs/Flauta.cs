using System.Collections;
using UnityEngine;

public class Flauta : MonoBehaviour
{
    public bool tocandoFlauta;
    public GameObject musicNotes;
    public bool NotasMimic;

    void Start()
    {
        tocandoFlauta = false;
        musicNotes.SetActive(false);
    }


    void Update()
    {
        //FuncionFlauta();
    }


    public void FuncionFlauta()
    {
        if (Input.GetMouseButtonDown(0) && !tocandoFlauta && !NotasMimic)
        {
            tocandoFlauta = true;
            NotasMimic = true;
            musicNotes.SetActive(true);
            Debug.Log("Estoy tocando la flauta");
            StartCoroutine(FlautaActiva());
            StartCoroutine(RecargaFlauta());

        }

    }

    public IEnumerator FlautaActiva()
    {

        yield return new WaitForSeconds(2);
        musicNotes.SetActive(false);
        tocandoFlauta = false;
        
        Debug.Log("Deje de tocar la flauta");

    }

    public IEnumerator RecargaFlauta()
    {
        yield return new WaitForSeconds(7);
        NotasMimic = false;
    }
}
