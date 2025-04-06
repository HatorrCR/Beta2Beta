using UnityEngine;

public class MouseControl : MonoBehaviour
{
    void Start()
    {
        // Asegurarse de que el rat�n est� visible y desbloqueado
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Desbloquear el rat�n (permitir que se mueva libremente)
    }
}
