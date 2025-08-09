using UnityEngine;

public class SelectShader : MonoBehaviour
{
    private GameObject lastHitObject;
    public float distancia = 10f;
    void Start()
    {
        
    }

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, distancia))
        {
            if (hit.collider.CompareTag("Selected"))
            {
                GameObject hitObject = hit.collider.gameObject;


                if (lastHitObject != hitObject)
                {
                    if (lastHitObject != null)
                    {
                        SetChildrenActive(lastHitObject, false);
                    }

                    SetChildrenActive(hitObject, true);
                    lastHitObject = hitObject;
                }
            }
            else
            {

                if (lastHitObject != null)
                {
                    SetChildrenActive(lastHitObject, false);
                    lastHitObject = null;
                }
            }
        }
        else
        {

            if (lastHitObject != null)
            {
                SetChildrenActive(lastHitObject, false);
                lastHitObject = null;
            }
        }
    }

    void SetChildrenActive(GameObject obj, bool state)
    {
        for (int i = 0; i < obj.transform.childCount; i++)
        {
            obj.transform.GetChild(i).gameObject.SetActive(state);
        }
    }
}

