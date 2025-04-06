using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount = 1;  // Cantidad de munici�n que otorga
    public WeaponWithLimitedAmmo weaponScript;  // Referencia al arma especial
    private RespawnObject respawnScript;  // Referencia al script de resurrecci�n

    void Start()
    {
        // Obtener el componente RespawnObject del mismo objeto
        respawnScript = GetComponent<RespawnObject>();
        if (respawnScript == null)
        {
            Debug.LogError("No se encontr� el script RespawnObject en el objeto de munici�n.");
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
                    // Llamar al m�todo Disappear en lugar de destruir el objeto
                    respawnScript.Disappear();
                }
                else
                {
                    Debug.LogError("RespawnObject no est� asignado en AmmoPickup.");
                }
            }
            else
            {
                Debug.LogError("WeaponWithLimitedAmmo no est� asignado en AmmoPickup.");
            }
        }
    }
}
