using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interacciones")]
    public float interactionRange = 3f; // Rango de interacci�n
    public LayerMask interactableLayer; // Capa que define los objetos interactuables

    void Update()
    {
        // Detectar si el jugador presiona el bot�n de interacci�n
        if (Input.GetKeyDown(KeyCode.E)) // Aqu� puedes usar cualquier tecla
        {
            InteractWithObject();
        }
    }

    // M�todo para interactuar con objetos
    void InteractWithObject()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRange, interactableLayer);

        foreach (var hitCollider in hitColliders)
        {
            SceneTriggerZone triggerZone = hitCollider.GetComponent<SceneTriggerZone>();
            if (triggerZone != null)
            {
                triggerZone.Interact(); // Llamamos al m�todo Interact() del trigger
            }
        }
    }
}
