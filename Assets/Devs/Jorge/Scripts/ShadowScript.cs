using UnityEngine;
using System.Collections.Generic;

public class ShadowScript : MonoBehaviour
{
    public EnemySpawner spawner;
    private int interactionCount = 0;
    private GameObject currentObject;


    private Vector3 originalPosition;

    private float shakeInterval = 40f;
    private float shakeDuration = 0.3f;
    private float shakeMagnitude = 0.02f;

    private float timer = 0f;
    private bool shaking = false;
    private float shakeElapsed = 0f;

    private bool shakeForever = false;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {
        if (shakeForever)
        {
            Shake();
            return;
        }

        timer += Time.deltaTime;

        if (!shaking && timer >= shakeInterval)
        {
            shaking = true;
            shakeElapsed = 0f;
            timer = 0f;
        }

        if (shaking)
        {
            if (shakeElapsed < shakeDuration)
            {
                Shake();
                shakeElapsed += Time.deltaTime;
            }
            else
            {
                shaking = false;
                transform.position = originalPosition;
            }
        }
    }

    public void SetCurrentObject(GameObject obj)
    {
        currentObject = obj;
    }

    public void Interact()
    {
        interactionCount++;

        if (interactionCount == 1)
        {
            TransformToAnotherObject();
        }
        else if (interactionCount == 2)
        {
            shakeForever = true;
            shaking = true;
            shakeElapsed = 0f;
        }
    }

    private void TransformToAnotherObject()
    {
        if (spawner == null)
        {
            Debug.LogWarning("Spawner no asignado en ShadowScript");
            return;
        }

        List<GameObject> posiblesObjetos = spawner.RoomObjects;

        List<GameObject> libres = new List<GameObject>();
        foreach (var obj in posiblesObjetos)
        {
            if (!spawner.IsObjectOccupied(obj) && obj != currentObject && obj.activeSelf)
            {
                libres.Add(obj);
            }
        }

        if (libres.Count == 0)
        {
            Debug.LogWarning("No hay objetos libres para transformar el ShadowScript");
            return;
        }

        GameObject nuevoObjeto = libres[Random.Range(0, libres.Count)];

        if (currentObject != null)
            spawner.ReleaseObject(currentObject);
        spawner.OccupyObject(nuevoObjeto);

        if (currentObject != null)
            currentObject.SetActive(true);

        nuevoObjeto.SetActive(false);

        CopyMeshAndTransform(nuevoObjeto, gameObject);

        currentObject = nuevoObjeto;

        originalPosition = transform.position;

        Debug.Log($"ShadowScript se transformó en nuevo objeto {nuevoObjeto.name}");
    }

    void Shake()
    {
        Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
        transform.position = originalPosition + randomOffset;
    }

    void CopyMeshAndTransform(GameObject source, GameObject target)
    {
        target.transform.position = source.transform.position;
        target.transform.rotation = source.transform.rotation;
        target.transform.localScale = source.transform.localScale;

        MeshFilter sourceMF = source.GetComponent<MeshFilter>();
        MeshFilter targetMF = target.GetComponent<MeshFilter>();

        if (sourceMF != null && targetMF != null)
        {
            targetMF.mesh = sourceMF.mesh;
        }
    }
}
