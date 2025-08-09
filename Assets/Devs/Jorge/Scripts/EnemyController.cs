using UnityEngine;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    [Header("Objects lists")]
    public List<GameObject> listBook;
    public List<GameObject> listChair;
    public List<GameObject> listTorch;
    public List<GameObject> listHandcuffs;
    public List<GameObject> listSkull;
    public List<GameObject> listChest;
    public List<GameObject> listBoot;
    public List<GameObject> listLira;
    public List<GameObject> listArmour;
    public List<GameObject> listCrystalBall;
    public List<GameObject> listLock;
    public List<GameObject> listBarrel;
    public List<GameObject> listSword;
    public List<GameObject> listDrum;
    public List<GameObject> listAnvil;
    public List<GameObject> listRope;

    private Dictionary<string, List<GameObject>> DictionaryObjects;

    [Header("Enemy personality")]
    public List<GameObject> EnemyCarnivoro;
    public List<GameObject> EnemyHidden;
    public List<GameObject> EnemyMelodicos;
    public List<GameObject> EnemyShadow;

    private Dictionary<string, List<GameObject>> DictionaryEnemies;

    void Awake()
    {
        // Enlazar palabra clave con la lista
        DictionaryObjects = new Dictionary<string, List<GameObject>>()
        {
            { "Book", listBook },
            { "Chair", listChair },
            { "Torch", listTorch },
            { "Handcuffs", listHandcuffs },
            { "Skull", listSkull },
            { "Chest", listChest },
            { "Boot", listBoot },
            { "Lira", listLira },
            { "Armour", listArmour },
            { "CrystalBall", listCrystalBall },
            { "Lock", listLock },
            { "Barrel", listBarrel },
            { "Sword", listSword },
            { "Drum", listDrum },
            { "Anvil", listAnvil },
            { "Rope", listRope }
        };

        // Enlazar palabra clave con tipo de personalidad
        DictionaryEnemies = new Dictionary<string, List<GameObject>>()
        {
            { "Carnivoro", EnemyCarnivoro },
            { "Hidden", EnemyHidden },
            { "Melodicos", EnemyMelodicos },
            { "Shadow", EnemyShadow }
        };
    }

    void Start()
    {
        SpawnRandomEnemyInRandomObject();
    }

    void SpawnRandomEnemyInRandomObject()
    {
        // Elige personalidad random
        var keys = new List<string>(DictionaryEnemies.Keys);
        string RandomEnemyPersonality = keys[Random.Range(0, keys.Count)];

        // Elige prefab de esa personalidad (objeto que es)
        List<GameObject> enemyList = DictionaryEnemies[RandomEnemyPersonality];
        if (enemyList == null || enemyList.Count == 0)
        {
            Debug.LogWarning("No hay enemigos en la lista de personalidad: " + RandomEnemyPersonality);
            return;
        }

        GameObject enemyPrefab = enemyList[Random.Range(0, enemyList.Count)];
        EnemyInfo enemyInfoPrefab = enemyPrefab.GetComponent<EnemyInfo>();
        if (enemyInfoPrefab == null)
        {
            Debug.LogWarning("No tiene el script de EnemyInfo");
            return;
        }

        // Se guarda el tipo de objeto que es el enemigo
        string NameObject = enemyInfoPrefab.ObjectName;

        // Se busca la lista qeu coincide con la clave
        if (!DictionaryObjects.ContainsKey(NameObject))
        {
            Debug.LogWarning("La lista no existe para este objeto: " + NameObject);
            return;
        }

        List<GameObject> objectList = DictionaryObjects[NameObject];
        if (objectList == null || objectList.Count == 0)
        {
            Debug.LogWarning("Esta vacia la lista para este objeto: " + NameObject);
            return;
        }

        // Se elige uno de los objetos de esa lista
        GameObject RoomObject = objectList[Random.Range(0, objectList.Count)];

        if (RoomObject == null || !RoomObject.activeSelf)
        {
            Debug.LogWarning("Objeto del room ya esta desactivado");
            return;
        }

        // Se desactiva el objeto elegido
        RoomObject.SetActive(false);

        // Se instancia el enemigo
        GameObject enemyInstance = Instantiate(enemyPrefab, RoomObject.transform.position, Quaternion.identity);

        Debug.Log($"El enemigo '{RandomEnemyPersonality}' que es un objeto '{NameObject}' fue instanciado");
    }
}