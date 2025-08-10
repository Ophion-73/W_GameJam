using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CarnivoroScript : MonoBehaviour
{
    public float Speed = 0.2f;
    public float obstacleDetectionDistance = 2f;
    public float avoidanceForce = 2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (MeatManager.Instance == null || !MeatManager.Instance.meatIsActive || MeatManager.Instance.meatTransform == null)
            return;

        Vector3 targetPos = MeatManager.Instance.meatTransform.position;
        float distanceToMeat = Vector3.Distance(transform.position, targetPos);
        if (distanceToMeat < 0.1f)
            return;

        Vector3 direction = (targetPos - transform.position).normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, obstacleDetectionDistance))
        {
            if (hit.collider.CompareTag("Obstacle"))
            {
                Vector3 avoidDir = Vector3.Cross(Vector3.up, hit.normal).normalized;
                direction += avoidDir * avoidanceForce;
            }
        }

        rb.MovePosition(transform.position + direction.normalized * Speed * Time.fixedDeltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            rb.MoveRotation(lookRotation);
        }
    }
}
