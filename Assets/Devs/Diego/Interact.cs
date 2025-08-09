using JetBrains.Annotations;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [Header("General Behaviour")]
    public bool isInteracting;
    public Controller controller;
    Ray ray;
    [Header("Interact Behaviour")]
    
    public float rayLengthInteract;
    public Transform originalCameraPos;
    public Transform cameraPos;
    public LayerMask interactableObj;
    public KeyCode interactKey;
    
    

    [Header("Rotation Behaviour")]
    
    public float rayLengthMove;
    public GameObject staticPart;
    public GameObject movablePart;
    public GameObject playerCamera;
    public Transform newCameraPos;
    public LayerMask movableObj;

    [Header("public mouse pos")]
    public Vector2 clickPos;
    public Vector2 continousMousePos;
    public float xDiff;
    public float yDiff;
    bool canClick;
    

    private void Start()
    {
        isInteracting = false;
        canClick = false;
    }

    private void Update()
    {
        float raylength;
       if(!isInteracting)
        {
            raylength = rayLengthInteract;
            ray = new Ray(cameraPos.position, cameraPos.forward);
            Debug.DrawRay(ray.origin, ray.direction * rayLengthInteract, Color.red);
            if (Physics.Raycast(ray , out RaycastHit hit, raylength, interactableObj))
            {
                if(Input.GetKeyDown(interactKey))
                {
                    InteractWithObject(hit.collider.gameObject);
                    isInteracting=true;
                    gameObject.GetComponent<MouseLook>().isInteracting = true;
                    controller.isInteracting=true;
                    
                }
            }
        }
       else
        {
            if (Input.GetKeyDown(interactKey))
            {
                GetComponent<Camera>().transform.position = originalCameraPos.position;
                GetComponent<Camera>().transform.rotation = originalCameraPos.rotation;
                isInteracting = false;
                gameObject.GetComponent<MouseLook>().isInteracting=false;
                controller.isInteracting = false;
            }
            
            raylength = rayLengthMove;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.DrawRay(ray.origin, ray.direction * rayLengthInteract, Color.red);
            
            if (Physics.Raycast(ray, out RaycastHit hit, raylength, movableObj))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    clickPos = Input.mousePosition;
                    Debug.Log("objetomovible");
                    canClick = true;
                    movablePart = hit.collider.gameObject;
                }
            }
            if (Input.GetMouseButton(0) && canClick)
            {
                continousMousePos = Input.mousePosition;
                xDiff = continousMousePos.x - clickPos.x;
                yDiff = continousMousePos.y - clickPos.y;
                movablePart.GetComponent<Interactable>().Open(yDiff);
            }
            if (Input.GetMouseButtonUp(0) && canClick)
            {
                
                clickPos = new Vector2(0, 0);
                continousMousePos = new Vector2(0, 0);
                xDiff = 0;
                yDiff = 0;
                
                
                canClick = false;
                movablePart.GetComponent<Interactable>().Close();
                movablePart = null;
            }
        }

       
    }

    public void InteractWithObject(GameObject target)
    {
        newCameraPos = target.GetComponentInChildren<Interactable>().cameraPos;
        GetComponent<Camera>().transform.position = newCameraPos.position;
        GetComponent<Camera>().transform.rotation = newCameraPos.rotation;
    }
}
