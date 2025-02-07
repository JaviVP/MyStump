using UnityEngine;

public class DiceCollision : MonoBehaviour
{
    [SerializeField] private Vector3 pushD1;
    [SerializeField] private Vector3 pushD2;
    [SerializeField] private Vector3 torqueD1;
    [SerializeField] private Vector3 torqueD2;
    [SerializeField] private GameObject dice1;
    [SerializeField] private GameObject dice2;
    [SerializeField] private float minDelay = 0.00f;  // Tiempo m�nimo en segundos para el retraso
    [SerializeField] private float maxDelay = 0.60f;  // Tiempo m�ximo en segundos para el retraso

    private bool hasBeenLaunched = false; // Para evitar m�ltiples lanzamientos

    void Start()
    {
        // Asignar un tiempo aleatorio entre minDelay y maxDelay antes de lanzar los dados
        float randomDelay = Random.Range(minDelay, maxDelay);
        Invoke("LaunchDice", randomDelay);
    }

    void LaunchDice()
    {
        if (hasBeenLaunched) return; // Evita lanzamientos repetidos

        Rigidbody rb1 = dice1.GetComponent<Rigidbody>();
        Rigidbody rb2 = dice2.GetComponent<Rigidbody>();

        if (rb1 != null && rb2 != null)
        {
            rb1.AddForce(pushD1 * 50);
            rb2.AddForce(pushD2 * 50);

            rb1.AddTorque(torqueD1);
            rb2.AddTorque(torqueD2);
        }

        hasBeenLaunched = true;
    }
}
