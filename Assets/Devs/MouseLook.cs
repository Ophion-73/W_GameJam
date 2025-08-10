using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float SensibilidadMouse = 100f;
    private float rotacionX = 0f;
    public float rotacionY = 0f;
    public Transform torso;
    public float distanciaInteraccion;
    public bool isInteracting;

    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        isInteracting = false;
    }
    void Update()
    {
        if (!isInteracting)
        {
            Cursor.lockState = CursorLockMode.Locked;
            RotacionMouse();
        }
        else
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
        
        
    }

    public void RotacionMouse()
    {
        float mouseX = Input.GetAxis("Mouse X") * SensibilidadMouse * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * SensibilidadMouse * Time.deltaTime;

        rotacionY += mouseX;
        rotacionX -= mouseY;
        rotacionX = Mathf.Clamp(rotacionX, -90f, 90f);

        if (torso != null)
            torso.localRotation = Quaternion.Euler(0f, rotacionY, 0f);

        transform.localRotation = Quaternion.Euler(rotacionX, 0f, 0f);
    }

    
       
    
}
