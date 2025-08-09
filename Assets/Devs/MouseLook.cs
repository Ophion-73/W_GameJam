using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public float SensibilidadMouse = 100f;
    private float rotacionX = 0f;
    public float rotacionY = 0f;
    public Transform torso;
    public float distanciaInteraccion;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        RotacionMouse();
        RayCastInteraction();
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

    public void RayCastInteraction()
    {
        Vector3 origen =  transform.position;
        Vector3 direccion = transform.forward;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distanciaInteraccion))
        {
            Debug.Log("Objeto detectado: " + hit.collider.name);
            Debug.DrawRay(origen, direccion * distanciaInteraccion, Color.yellow);

            if (hit.collider.CompareTag("Interactuar"))
            {
                Debug.Log("Objeto Interactuable" + hit.collider.name);
                if (Input.GetKeyDown(KeyCode.E))
                {
                    Debug.Log("Interactuando con Objeto");
                }
            }
        }
       
    }
}
