using System.Collections;
using UnityEngine;

public class MelodicoScript : MonoBehaviour
{
    public Flauta flautascript;
    public GameObject musicNotes;
    private Coroutine respuestaCoroutine;
    public bool Notas;


    void Start()
    {
        Notas = false;
        if (flautascript == null)
        {
            GameObject Player = GameObject.FindWithTag("Player");
            if (Player != null)
            {
                flautascript = Player.GetComponent<Flauta>();
            }
        }
        if (musicNotes == null)
        {
            musicNotes = transform.Find("MusicNotesParticles").gameObject;
        }
        musicNotes.SetActive(false);
    }


    void OnTriggerStay(Collider other)
    {
        Debug.Log("Jugador Collisionado Con Rango Melodico: " + other.name);

        if (other.CompareTag("Player"))
        {
            if (flautascript.tocandoFlauta == true && flautascript.NotasMimic == true && !Notas)
            {
                Debug.Log("Jugador Toca Flauta");
                musicNotes.SetActive(true);
                StartCoroutine(Respuesta());
                Notas = true;


            }

        }
    }

    IEnumerator Respuesta()
    {
        yield return new WaitForSeconds(2);
        musicNotes.SetActive(false);
        yield return new WaitForSeconds(5);
        Notas = false;

    }
}
