using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy personality")]
    public GameObject CarnivoroPrefab;
    public GameObject HiddenPrefab;
    public GameObject MelodicoPrefab;
    public GameObject ShadowPrefab;

    [Header("Number of enemies per personality")]
    public int NuCarnivoros = 0;
    public int NuHidden = 0;
    public int NuMelodicos = 0;
    public int NuShadow = 0;

    [Header("Lista global: todos los objetos del mapa")]
    public List<GameObject> RoomObjects;

    [Header("Objetos permitidos para Carnivoro")]
    public List<GameObject> CarnivoroObjects;

    [Header("Objetos permitidos para Hidden")]
    public List<GameObject> HiddenObjects;

    private HashSet<GameObject> UsedObjects = new HashSet<GameObject>();

    //public InventoryManageer inventoryM;

    void Start()
    {
        SpawnEnemigos();
        //inventoryM.monsterQ = (NuCarnivoros + NuHidden + NuMelodicos + NuShadow);
    }

    public void SpawnEnemigos()
    {
        UsedObjects.Clear();

        // Se crean listas de pares diferentes para cada personalidad
        List<(GameObject prefab, GameObject objet)> CarnivoroPar = ParGenerate(CarnivoroPrefab, CarnivoroObjects);
        List<(GameObject prefab, GameObject objet)> HiddenPar = ParGenerate(HiddenPrefab, HiddenObjects);
        List<(GameObject prefab, GameObject objet)> MelodicosPar = ParGenerate(MelodicoPrefab, RoomObjects);
        List<(GameObject prefab, GameObject objet)> ShadowPar = ParGenerate(ShadowPrefab, RoomObjects);

        SpawnPerQuantity(CarnivoroPar, NuCarnivoros);
        SpawnPerQuantity(HiddenPar, NuHidden);
        SpawnPerQuantity(MelodicosPar, NuMelodicos);
        SpawnPerQuantity(ShadowPar, NuShadow);
    }

    private List<(GameObject prefab, GameObject objet)> ParGenerate(GameObject prefab, List<GameObject> AllowedObjects)
    {
        List<(GameObject, GameObject)> pares = new List<(GameObject, GameObject)>();
        if (prefab == null)
        {
            Debug.LogWarning("No hay prefab");
            return pares;
        }
        if (AllowedObjects == null)
        {
            Debug.LogWarning("No hay objetos permitidos");
            return pares;
        }

        foreach (var obj in AllowedObjects)
        {
            if (obj != null && obj.activeSelf && !UsedObjects.Contains(obj))
            {
                pares.Add((prefab, obj));
            }
        }

        // Revuelve la lista
        for (int i = 0; i < pares.Count; i++)
        {
            var temp = pares[i];
            int randomIndex = Random.Range(i, pares.Count);
            pares[i] = pares[randomIndex];
            pares[randomIndex] = temp;
        }

        return pares;
    }

    private void SpawnPerQuantity(List<(GameObject prefab, GameObject objeto)> pares, int quantity)
    {
        int InstantiateEnemies = 0;

        foreach (var (prefab, objeto) in pares)
        {
            if (InstantiateEnemies >= quantity)
                break;

            if (UsedObjects.Contains(objeto))
                continue;

            UsedObjects.Add(objeto);

            objeto.SetActive(false);

            GameObject EnemyInstantiate = Instantiate(prefab);

            CopyMeshAndTransform(objeto, EnemyInstantiate);


            ShadowScript shadow = EnemyInstantiate.GetComponent<ShadowScript>();
            if (shadow != null)
            {
                shadow.spawner = this;
                shadow.SetCurrentObject(objeto);
            }


            Debug.Log($"Instanciado {prefab.name} en objeto {objeto.name}");

            InstantiateEnemies++;
        }
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

        // Aqui ira lo de materiales en un futuro
    }

    public bool IsObjectOccupied(GameObject obj)
    {
        return UsedObjects.Contains(obj);
    }

    public void OccupyObject(GameObject obj)
    {
        UsedObjects.Add(obj);
    }

    public void ReleaseObject(GameObject obj)
    {
        UsedObjects.Remove(obj);
    }

}
