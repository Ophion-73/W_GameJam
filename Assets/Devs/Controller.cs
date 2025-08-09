using UnityEngine;

public class Controller : MonoBehaviour
{
    public float Velocidad;
    public float VelocidadAgachado;
    public float TiempoTransicion = 1;
    public float alturaNormal = 1.7f;
    public float alturaAgachado = 1f;
    private float alturaObjetivo;
    private float velocidadActualY;




    void Start()
    {
        alturaObjetivo = alturaNormal;

    }


    void Update()
    {
        Movimiento();


    }


    public void Movimiento()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movimiento =  (transform.right * horizontal + transform.forward * vertical) * Velocidad * Time.deltaTime;
        transform.Translate(movimiento, Space.World);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            alturaObjetivo =  alturaAgachado;
            Velocidad = VelocidadAgachado;
        }
        else
        {
            alturaObjetivo = alturaNormal;
            Velocidad = 5f;
        }

        float nuevaY = Mathf.SmoothDamp(transform.position.y, alturaObjetivo, ref velocidadActualY, TiempoTransicion);

        transform.position = new Vector3(transform.position.x, nuevaY, transform.position.z);
    }
    

}
