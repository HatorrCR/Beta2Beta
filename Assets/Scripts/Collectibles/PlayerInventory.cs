using UnityEngine;
using TMPro; // Para usar TextMeshPro

public class PlayerInventory : MonoBehaviour
{
    public int collectedCoins = 0; // Contador de objetos recogidos
    public TextMeshProUGUI coinsText; // Referencia al TextMeshPro para mostrar las monedas

    // M�todo para a�adir monedas al inventario
    public void AddCoins(int value)
    {
        collectedCoins += value;
        UpdateUI(); // Actualizar la UI cada vez que se a�ade una moneda
    }

    // M�todo para actualizar la UI
    void UpdateUI()
    {
        if (coinsText != null)
        {
            coinsText.text = "Minerals: " + collectedCoins; // Actualiza el texto de la UI
        }
    }
}
