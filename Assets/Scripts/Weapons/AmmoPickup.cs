using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 1;  // Cantidad de munición que otorga
    public WeaponWithLimitedAmmo weaponScript;  // Referencia al arma especial
    private RespawnObject respawnScript;  // Referencia al script de resurrección

    void Start()
    {
        // Obtener el componente RespawnObject del mismo objeto
        respawnScript = GetComponent<RespawnObject>();
        if (respawnScript == null)
        {
            Debug.LogError("No se encontró el script RespawnObject en el objeto de munición.");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (weaponScript != null)
            {
                weaponScript.AddAmmo(ammoAmount);

                if (respawnScript != null)
                {
                    // Llamar al método Disappear en lugar de destruir el objeto
                    respawnScript.Disappear();
                }
                else
                {
                    Debug.LogError("RespawnObject no está asignado en AmmoPickup.");
                }
            }
            else
            {
                Debug.LogError("WeaponWithLimitedAmmo no está asignado en AmmoPickup.");
            }
        }
    }
}
