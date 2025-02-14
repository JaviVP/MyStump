using System.Collections;
using UnityEngine;

public class DiceMassIncrease : MonoBehaviour
{
    [SerializeField] private GameObject fxCollision;

    void OnCollisionEnter(Collision collision)
    {
       
           // Verificar si este dado choca con el otro dado
        if (collision.gameObject.CompareTag("Dice")) 
        {
            fxCollision.SetActive(true); // Activar el efecto visual
        }
    
      

    }
  
}
