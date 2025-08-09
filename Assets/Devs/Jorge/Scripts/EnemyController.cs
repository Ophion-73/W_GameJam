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

    public void SpawnEnemy(string EnemyPersonality, string NameObject, Vector3 position)
    {
        // Revisa si existe la lista de la palabra clave
        if (!DictionaryEnemies.ContainsKey(EnemyPersonality))
        {
            Debug.LogWarning($"No existe el tipo de enemigo: {EnemyPersonality}");
            return;
        }

        List<GameObject> listaTipo = DictionaryEnemies[EnemyPersonality];

        // Elige enemigo random y lo pone en escena
        GameObject enemyPrefab = listaTipo[Random.Range(0, listaTipo.Count)];
        GameObject enemy = Instantiate(enemyPrefab, position, Quaternion.identity);

        // Se le indica al enemigo que objeto imita
        EnemyInfo info = enemy.GetComponent<EnemyInfo>();
        if (info != null)
        {
            info.ObjectName = NameObject;
        }

        // Remplaza el objeto por el enemigo
        ReemplazarObjeto(enemy);
    }

    public void ReemplazarObjeto(GameObject Enemy)
    {
        EnemyInfo info = Enemy.GetComponent<EnemyInfo>();

        if (info != null && DictionaryObjects.ContainsKey(info.ObjectName))
        {
            List<GameObject> list = DictionaryObjects[info.ObjectName];
            GameObject NewObject = list[Random.Range(0, list.Count)];

            // Instanciar nuevo objeto como hijo del enemigo
            GameObject instancia = Instantiate(NewObject, Enemy.transform);
            instancia.transform.localPosition = Vector3.zero; // Lo centra
        }
        else
        {
            Debug.LogWarning("No se encontró lista para: " + info.ObjectName);
        }
    }
}