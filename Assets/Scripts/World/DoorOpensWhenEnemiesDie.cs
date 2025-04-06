using UnityEngine;

public class DoorOpensWhenEnemiesDie : MonoBehaviour
{
    [Header("Enemigos que deben ser eliminados")]
    public GameObject[] enemies;

    [Header("Puerta a desaparecer (u otro objeto)")]
    public GameObject doorObject;

    void Update()
    {
        if (AllEnemiesDefeated())
        {
            if (doorObject != null)
            {
                doorObject.SetActive(false); // Desactiva la puerta
                enabled = false; // Desactiva este script para no seguir comprobando
            }
        }
    }

    bool AllEnemiesDefeated()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy != null)
            {
                return false; // Al menos un enemigo sigue vivo
            }
        }
        return true; // Todos los enemigos han sido destruidos
    }
}
