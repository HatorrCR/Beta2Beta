using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interacciones")]
    public float interactionRange = 3f; // Rango de interacción
    public LayerMask interactableLayer; // Capa que define los objetos interactuables

    void Update()
    {
        // Detectar si el jugador presiona el botón de interacción
        if (Input.GetKeyDown(KeyCode.E)) // Aquí puedes usar cualquier tecla
        {
            InteractWithObject();
        }
    }

    // Método para interactuar con objetos
    void InteractWithObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);

        foreach (var hitCollider in hitColliders)
        {
            SceneTriggerZone triggerZone = hitCollider.GetComponent<SceneTriggerZone>();
            if (triggerZone != null)
            {
                triggerZone.Interact(); // Llamamos al método Interact() del trigger
            }
        }
    }
}
