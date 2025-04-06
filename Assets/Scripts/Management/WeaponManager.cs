using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    [Header("Armas")]
    public GameObject infiniteWeapon;
    public GameObject limitedWeapon;

    private WeaponInfiniteAmmo infiniteWeaponScript;
    private WeaponWithLimitedAmmo limitedWeaponScript;

    private bool isInfiniteWeaponActive = true; // Comenzamos con el arma infinita activa

    void Start()
    {
        if (infiniteWeapon != null)
            infiniteWeaponScript = infiniteWeapon.GetComponent<WeaponInfiniteAmmo>();
        else
            Debug.LogError("Infinite Weapon no asignada en el Inspector.");

        if (limitedWeapon != null)
            limitedWeaponScript = limitedWeapon.GetComponent<WeaponWithLimitedAmmo>();
        else
            Debug.LogError("Limited Weapon no asignada en el Inspector.");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipInfiniteWeapon(); // Cambiar a la arma infinita
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipLimitedWeapon(); // Cambiar a la arma limitada
        }
    }


    void EquipInfiniteWeapon()
    {
        limitedWeapon.SetActive(false);
        infiniteWeapon.SetActive(true);

        if (limitedWeaponScript != null)
            limitedWeaponScript.enabled = false;

        if (infiniteWeaponScript != null)
            infiniteWeaponScript.enabled = true;

        isInfiniteWeaponActive = true;

        Debug.Log("Arma infinita activada. Arma especial desactivada.");
    }

    void EquipLimitedWeapon()
    {
        infiniteWeapon.SetActive(false);
        limitedWeapon.SetActive(true);

        if (infiniteWeaponScript != null)
            infiniteWeaponScript.enabled = false;

        if (limitedWeaponScript != null)
            limitedWeaponScript.enabled = true;

        isInfiniteWeaponActive = false;

        Debug.Log("Arma especial activada. Arma infinita desactivada.");
    }

    public bool IsInfiniteWeaponActive()
    {
        return isInfiniteWeaponActive;
    }
}
