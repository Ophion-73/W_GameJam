using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class EnemyController : MonoBehaviour
{
    [Header("Enemy Personality")]
    public GameObject CarnivoroPrefab;
    public GameObject HiddenPrefab;
    public GameObject MelodicoPrefab;
    public GameObject ShadowPrefab;

    [Header("Number of enemies per personality")]
    public int NuCarnivoros = 0;
    public int NuHidden = 0;
    public int NuMelodicos = 0;
    public int NuShadow = 0;

    [Header("Objects list")]
    public List<GameObject> RoomObjects;

    void Start()
    {
        // Filtramos objetos activos y hacemos copia para modificar sin afectar original
        List<GameObject> AvailableObjects = RoomObjects.Where(o => o != null && o.activeSelf).ToList();

        SpawnEnemiesOfType(CarnivoroPrefab, NuCarnivoros, ref AvailableObjects);
        SpawnEnemiesOfType(HiddenPrefab, NuHidden, ref AvailableObjects);
        SpawnEnemiesOfType(MelodicoPrefab, NuMelodicos, ref AvailableObjects);
        SpawnEnemiesOfType(ShadowPrefab, NuShadow, ref AvailableObjects);
    }

    void SpawnEnemiesOfType(GameObject PersonalityPrefab, int Number, ref List<GameObject> AvailableObjects)
    {
        if (PersonalityPrefab == null)
        {
            Debug.LogWarning("No se asigno u prefab de personalidad");
            return;
        }

        if (AvailableObjects == null || AvailableObjects.Count == 0)
        {
            Debug.LogWarning("No hay objetos para reemplazar");
            return;
        }

        // La lista se revuelve
        AvailableObjects = AvailableObjects.OrderBy(x => Random.value).ToList();

        int spawnCount = Mathf.Min(Number, AvailableObjects.Count);

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject RoomObject = AvailableObjects[i];

            GameObject enemyInstance = Instantiate(PersonalityPrefab, RoomObject.transform.position, RoomObject.transform.rotation);
            enemyInstance.transform.localScale = RoomObject.transform.localScale;

            CopyMeshAndTransform(RoomObject, enemyInstance);

            RoomObject.SetActive(false);
        }

        // Se eliminan de la lista los objetos que ya se desactivaron
        AvailableObjects.RemoveRange(0, spawnCount);
    }

    void CopyMeshAndTransform(GameObject source, GameObject target)
    {
        MeshFilter sourceMF = source.GetComponent<MeshFilter>();
        MeshRenderer sourceMR = source.GetComponent<MeshRenderer>();

        MeshFilter targetMF = target.GetComponent<MeshFilter>();
        MeshRenderer targetMR = target.GetComponent<MeshRenderer>();

        if (sourceMF != null && targetMF != null)
        {
            targetMF.mesh = sourceMF.mesh;
        }
    }
}