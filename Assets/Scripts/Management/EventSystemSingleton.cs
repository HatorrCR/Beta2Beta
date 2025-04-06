using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemSingleton : MonoBehaviour
{
    public static EventSystemSingleton Instance { get; private set; }  // Propiedad estática para acceder a la instancia

    private EventSystem eventSystem;  // Instancia del EventSystem

    void Awake()
    {
        // Si ya existe una instancia, destruir este objeto
        if (Instance == null)
        {
            Instance = this;  // Asignar la instancia
            eventSystem = GetComponent<EventSystem>();  // Obtener el EventSystem
            DontDestroyOnLoad(gameObject);  // Mantener este objeto entre escenas
        }
        else
        {
            Destroy(gameObject);  // Destruir este objeto si ya existe una instancia
        }
    }

    // Método para acceder al EventSystem
    public EventSystem GetEventSystem()
    {
        return eventSystem;
    }
}
