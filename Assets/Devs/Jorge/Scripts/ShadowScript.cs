using UnityEngine;
using System.Collections.Generic;

public class ShadowScript : MonoBehaviour
{
    public EnemySpawner spawner;
    private int interactionCount = 0;
    private GameObject currentObject;

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
            Die();
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

        Debug.Log($"ShadowScript se transformó en nuevo objeto {nuevoObjeto.name}");
    }

    private void Die()
    {
        if (currentObject != null && spawner != null)
        {
            spawner.ReleaseObject(currentObject);
        }
        Destroy(gameObject);
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
