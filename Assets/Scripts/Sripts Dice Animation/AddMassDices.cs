using System.Collections;
using UnityEngine;

public class DiceMassIncrease : MonoBehaviour
{
    [SerializeField] private float massIncrease = 0.5f; // Cantidad de masa que se suma en cada colisi�n
    [SerializeField] private GameObject fxCollision;    // Efecto visual de la colisi�n

    void OnCollisionEnter(Collision collision)
    {
        // Activar el efecto visual
        fxCollision.SetActive(true);

        // Verificar si este dado choca con el otro dado
        if (collision.gameObject.CompareTag("Dice")) // Aseg�rate de que los dados tengan la etiqueta "Dice"
        {
            Rigidbody rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Aumentar la masa del dado
                rb.mass += massIncrease;
                Debug.Log("Colisi�n detectada: Masa del dado aumentada.");
            }
        }

        // Llamar a la corutina para desactivar el efecto despu�s de 2 segundos
        StartCoroutine(DisableFxAfterTime(2f));
    }

    // Corutina para desactivar el efecto despu�s de un tiempo
    IEnumerator DisableFxAfterTime(float time)
    {
        yield return new WaitForSeconds(time);  // Espera durante 'time' segundos
        fxCollision.SetActive(false);           // Desactiva el efecto visual
    }
}
