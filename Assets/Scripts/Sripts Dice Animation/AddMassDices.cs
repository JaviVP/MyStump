using System.Collections;
using UnityEngine;

public class DiceMassIncrease : MonoBehaviour
{
    [SerializeField] private float massIncrease = 0.5f; // Cantidad de masa que se suma en cada colisión
    [SerializeField] private GameObject fxCollision;
    [SerializeField] private GameObject fxSmoke;
    [SerializeField] private GameObject dice;

    [SerializeField] private Vector3 torque;
    private Rigidbody rb;

    void OnCollisionEnter(Collision collision)
    {
        // Activar el efecto visual
        fxCollision.SetActive(true);

        // Verificar si este dado choca con el otro dado
        if (collision.gameObject.CompareTag("Dice")) // Asegúrate de que los dados tengan la etiqueta "Dice"
        {
             rb = GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Aumentar la masa del dado
                rb.mass += massIncrease;
                AddRotation();
                //Debug.Log("Colisión detectada: Masa del dado aumentada.");
            }
        }


            // Llamar a la corutina para desactivar el efecto después de 2 segundos
            StartCoroutine(DisableFxAfterTime(2f));
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Ground")) // Asegúrate de que los dados tengan la etiqueta "Dice"
        {
            fxSmoke.SetActive(true);
        }
    }

    public void AddRotation()
    {
        rb = GetComponent<Rigidbody>();


        if (rb != null)
        {
           
            rb.AddTorque(torque*50);
           
        }


    }
    // Corutina para desactivar el efecto después de un tiempo
    IEnumerator DisableFxAfterTime(float time)
    {
        yield return new WaitForSeconds(time);  // Espera durante 'time' segundos
        fxCollision.SetActive(false);           // Desactiva el efecto visual
    }
}
