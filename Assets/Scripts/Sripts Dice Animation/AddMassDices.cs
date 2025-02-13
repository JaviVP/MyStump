using System.Collections;
using UnityEngine;

public class DiceMassIncrease : MonoBehaviour
{
    [SerializeField] private float massIncrease = 0.5f; // Cantidad de masa que se suma en cada colisión
    [SerializeField] private GameObject fxCollision;
    [SerializeField] private GameObject dice;
    [SerializeField] private Vector3 torque;
    [SerializeField] private Vector3 pD1;

    private Rigidbody rb;

    void OnCollisionEnter(Collision collision)
    {
        // Activar el efecto visual
        fxCollision.SetActive(true);

        // Verificar si este dado choca con el otro dado
        if (collision.gameObject.CompareTag("Dice")) 
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

       
        if (collision.gameObject.CompareTag("Stampide"))
        {
                rb.AddForce(pD1 * 50);

                Debug.Log("Sumamos colision");
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
   
}
