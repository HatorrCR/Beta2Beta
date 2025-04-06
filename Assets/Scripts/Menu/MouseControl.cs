using UnityEngine;

public class MouseControl : MonoBehaviour
{
    void Start()
    {
        // Asegurarse de que el ratón esté visible y desbloqueado
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None; // Desbloquear el ratón (permitir que se mueva libremente)
    }
}
