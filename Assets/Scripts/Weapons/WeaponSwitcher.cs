using UnityEngine;
using UnityEngine.UI;  // Asegúrate de usar el espacio de nombres adecuado para la UI

public class WeaponSwitcher : MonoBehaviour
{
    public RawImage infiniteWeaponIcon; // El icono para el arma infinita (RawImage)
    public RawImage specialWeaponIcon;  // El icono para el arma especial (RawImage)

    private bool isInfiniteWeaponActive = true; // Indica si el arma infinita está activa

    void Start()
    {
        UpdateWeaponIcons(); // Inicializa los iconos cuando empieza el juego
    }

    void Update()
    {
        // Detectar cambio de arma con el 1 y 2
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetInfiniteWeapon(); // Cambiar a la arma infinita
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetSpecialWeapon(); // Cambiar a la arma especial
        }
    }

    // Activa el icono para el arma infinita y desactiva el del arma especial
    void SetInfiniteWeapon()
    {
        isInfiniteWeaponActive = true;
        UpdateWeaponIcons();
    }

    // Activa el icono para el arma especial y desactiva el del arma infinita
    void SetSpecialWeapon()
    {
        isInfiniteWeaponActive = false;
        UpdateWeaponIcons();
    }

    // Actualiza los iconos de la UI dependiendo de qué arma está activa
    void UpdateWeaponIcons()
    {
        if (isInfiniteWeaponActive)
        {
            infiniteWeaponIcon.enabled = true;  // Muestra el icono del arma infinita
            specialWeaponIcon.enabled = false; // Oculta el icono del arma especial
        }
        else
        {
            infiniteWeaponIcon.enabled = false; // Oculta el icono del arma infinita
            specialWeaponIcon.enabled = true;   // Muestra el icono del arma especial
        }
    }
}
