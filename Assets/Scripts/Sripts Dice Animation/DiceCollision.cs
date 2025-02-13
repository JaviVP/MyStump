using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DiceCollision : MonoBehaviour
{
    [SerializeField] private Vector3 pushD1;
    [SerializeField] private Vector3 pushD2;
    [SerializeField] private Vector3 torqueD1;
    [SerializeField] private Vector3 torqueD2;
    [SerializeField] private GameObject dice1;
    [SerializeField] private GameObject dice2;
    
    [SerializeField] private float minDelay = 0.00f;  
    [SerializeField] private float maxDelay = 0.60f;

    private Rigidbody rb1;
    private Rigidbody rb2;
    private bool hasBeenLaunched = false; // Para evitar múltiples lanzamientos

    void Start()
    {
        // Asignar un tiempo aleatorio entre minDelay y maxDelay antes de lanzar los dados
        float randomDelay = Random.Range(minDelay, maxDelay);
        Invoke("LaunchDice", randomDelay);
        rb1 = dice1.GetComponent<Rigidbody>();
        rb2 = dice2.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }
   
    void LaunchDice()
    {
        if (hasBeenLaunched) return; // Evita lanzamientos repetidos 

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
