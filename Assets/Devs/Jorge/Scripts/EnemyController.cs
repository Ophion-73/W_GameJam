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
        string randomEnemyType = keys[Random.Range(0, keys.Count)];

        // Elige prefab de esa personalidad (objeto que es)
        List<GameObject> enemyList = DictionaryEnemies[randomEnemyType];
        if (enemyList == null || enemyList.Count == 0)
        {
            Debug.LogWarning("Lista de enemigos vacía para tipo: " + randomEnemyType);
            return;
        }

        GameObject enemyPrefab = enemyList[Random.Range(0, enemyList.Count)];
        EnemyInfo enemyInfoPrefab = enemyPrefab.GetComponent<EnemyInfo>();
        if (enemyInfoPrefab == null)
        {
            Debug.LogWarning("El prefab enemigo no tiene EnemyInfo");
            return;
        }

        // Se guarda el tipo de objeto que es el enemigo
        string objetoNombre = enemyInfoPrefab.ObjectName;

        // Se busca la lista qeu coincide con la clave
        if (!DictionaryObjects.ContainsKey(objetoNombre))
        {
            Debug.LogWarning("No existe lista de objetos para: " + objetoNombre);
            return;
        }

        List<GameObject> objectList = DictionaryObjects[objetoNombre];
        if (objectList == null || objectList.Count == 0)
        {
            Debug.LogWarning("Lista de objetos vacía para: " + objetoNombre);
            return;
        }

        // Se elige uno de los objetos de esa lista
        GameObject objetoMapa = objectList[Random.Range(0, objectList.Count)];

        if (objetoMapa == null || !objetoMapa.activeSelf)
        {
            Debug.LogWarning("Objeto del mapa no válido o ya está inactivo");
            return;
        }

        // Se desactiva ya elegido
        objetoMapa.SetActive(false);

        // Se instancia el enemigo
        GameObject enemyInstance = Instantiate(enemyPrefab, objetoMapa.transform.position, Quaternion.identity);

        Debug.Log($"Enemigo '{randomEnemyType}' con objeto '{objetoNombre}' ha reemplazado un objeto en la posición {objetoMapa.transform.position}");
    }
}