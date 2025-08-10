using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 3f;
    public KeyCode interactKey = KeyCode.H;

    void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            Vector3 rayOrigin = transform.position + new Vector3(0f, -0.9f, 0f);
            Vector3 rayDirection = transform.forward;

            Debug.DrawRay(rayOrigin, rayDirection * interactionDistance, Color.red, 5f);

            RaycastHit hit;
            if (Physics.Raycast(rayOrigin, rayDirection, out hit, interactionDistance))
            {
                ShadowScript shadow = hit.collider.GetComponentInParent<ShadowScript>();
                if (shadow != null)
                {
                    shadow.Interact();
                }
            }
        }
    }
}
