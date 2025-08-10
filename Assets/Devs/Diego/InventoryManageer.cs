using System;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InventoryManageer : MonoBehaviour
{
    public Carne carne;
    public int itemIndex;
    public GameObject[] items;
    [Header("General")]
    public int puntuacion;

    [Header("Detector de mimics")]
    public int monsterQ;
    public Material[] numTex;


    [Header("Cuchillo")]
    public Transform cameraPosition;
    public float rayLength;
    public LayerMask objects;
    public string objectTag;
    public string mimicTag;

    private void Start()
    {
        items[0].GetComponent<MeshRenderer>().material = numTex[0];
    }
    private void Update()
    {
        UseItem();
    }

    public void ChangeActiveItem(int index)
    {
        if (index < 0 || index >= items.Length) return;
        itemIndex = index;

        for(int i = 0; i < items.Length; i++)
        {
            items[i].SetActive(false);
        }

        items[itemIndex].SetActive(true);
    }

    public void UseItem()
    {
        if(Input.GetMouseButtonDown(0))
        {
            switch(itemIndex)
            {
                //Detector de numero de mimics
                case 0:

                    UseMimicDetector();
                    break;
                //Cuchillo
                case 1:
                    UseKnife();
                    break;
                case 2:
                    UseMeat();
                    break;
                case 3:
                    UseCandle();
                    break;

                case 4:
                    UseFlute();
                    break;

            }
        }
    }
    public void UseFlute()
    {
        //funcionalidad de flauta
    }
    private void UseCandle()
    {
        if (Physics.Raycast(new Ray(cameraPosition.position, cameraPosition.forward), out RaycastHit hit, rayLength, objects))
        {
            GameObject hitGameObject = hit.collider.gameObject;
            if (hitGameObject.CompareTag(mimicTag) && hitGameObject.GetComponent<ShadowScript>() != null)
            {
                //hitGameObject.GetComponent<ShadowScript>()."aqui va el metodo para dacudir a los bros";
            }
        }
    }

    private void UseMeat()
    {
        carne.ThrowMeat();
    }

    public void UseKnife()
    {
        if(Physics.Raycast(new Ray(cameraPosition.position,cameraPosition.forward), out RaycastHit hit,  rayLength, objects))
        {
            GameObject hitGameObject = hit.collider.gameObject;
            if (hitGameObject.CompareTag(mimicTag))
            {
                hitGameObject.SetActive(false);
                Debug.Log("Mataste un mmimic");
            }
            if(hitGameObject.CompareTag(objectTag))
            {
                hitGameObject.SetActive(false);
                Debug.Log("Lacagaste");
            }
        }
    }

    public void UseMimicDetector()
    {
        items[0].GetComponent<MeshRenderer>().material = numTex[monsterQ];
        Debug.Log("Detectando");
    }
}
