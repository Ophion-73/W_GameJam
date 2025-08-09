using UnityEngine;

public class HiddenScript : MonoBehaviour
{
    public float shakeMagnitude = 0.1f;  // cu�nto se mueve
    public float shakeFrequency = 25f;   // qu� tan r�pido cambia la posici�n

    private Vector3 startPos;
    private float shakeTimer = 0f;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        shakeTimer += Time.deltaTime * shakeFrequency;

        // Movimiento aleatorio basado en seno y coseno para suavidad
        float offsetX = Mathf.PerlinNoise(shakeTimer, 0f) * 2 - 1;
        float offsetY = Mathf.PerlinNoise(0f, shakeTimer) * 2 - 1;

        Vector3 shakeOffset = new Vector3(offsetX, offsetY, 0f) * shakeMagnitude;

        transform.position = startPos + shakeOffset;
    }
}
