using UnityEngine;

public class CarnivoroScript : MonoBehaviour
{
    public float rotationSpeed = 90f;      // grados por segundo para rotar
    public float floatAmplitude = 0.5f;    // altura máxima del movimiento vertical
    public float floatFrequency = 1f;      // velocidad del movimiento vertical

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;  // Guardamos la posición inicial
    }

    void Update()
    {
        // Girar hacia la derecha
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Movimiento arriba y abajo con Seno
        float newY = startPos.y + Mathf.Sin(Time.time * floatFrequency) * floatAmplitude;

        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}
