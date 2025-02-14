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
    [SerializeField] private GameObject stampide;
    
    [SerializeField] private float minDelay;  
    [SerializeField] private float maxDelay;

    private Rigidbody rb1;
    private Rigidbody rb2;
   // Para evitar múltiples lanzamientos
    private float randomDelay;

    public static DiceCollision Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void Start()
    {
        randomDelay = Random.Range(minDelay, maxDelay);
        // Asignar un tiempo aleatorio entre minDelay y maxDelay antes de lanzar los dados
        //Invoke("LaunchDice", randomDelay);
        rb1 = dice1.GetComponent<Rigidbody>();
        rb2 = dice2.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("Reset");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }*/
    }
   
    public void LaunchDice()
    {
        
    
        if (rb1 != null && rb2 != null)
        {
            rb1.AddForce(pushD1 * 50);
            rb2.AddForce(pushD2 * 50);

            rb1.AddTorque(torqueD1);
            rb2.AddTorque(torqueD2);
        }

        


    }
    
    public void LaunchDicesDelay()
    {
        randomDelay = Random.Range(minDelay, maxDelay);
        Invoke("LaunchDice", randomDelay);
    }

}
