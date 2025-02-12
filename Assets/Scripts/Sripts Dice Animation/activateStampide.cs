using UnityEngine;

public class activateStampide : MonoBehaviour
{
    [SerializeField] private GameObject antStampide;
    [SerializeField] private float velocity;
    [SerializeField] private float stampideTime;
    [SerializeField] private GameObject dadosFinal;
    private float stampideTimeLeft;
    private bool isMoving = false; // Variable para controlar si el objeto debe moverse.
    private Animator animator;
    private void Start()
    {
        stampideTimeLeft = stampideTime;
        animator = dadosFinal.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Entró al trigger");
            isMoving = true; // Iniciamos el movimiento cuando entra en el trigger.
        }
    }

    private void Update()
    {
        if (isMoving && stampideTimeLeft > 0)
        {
            // Mueve el objeto en el eje X durante el tiempo que queda.
            antStampide.transform.Translate(Vector3.right * velocity * Time.deltaTime);

            // Reducimos el tiempo restante.
            stampideTimeLeft -= Time.deltaTime;

            // Si el tiempo se acaba, paramos el movimiento.
            if (stampideTimeLeft <= 0)
            {
                isMoving = false;
                dadosFinal.SetActive(true);
                ActivateAnimation();
                Debug.Log("Movimiento finalizado");
            }
        }
    }

    private void ActivateAnimation()
    {
        // Asegúrate de que el Animator tiene un Trigger configurado en el controlador de animación
        animator.SetTrigger("FinalizarMovimiento"); // "FinalizarMovimiento" es el nombre del Trigger en el Animator
    }


    //rotacion de los dados despues de la animación 

    /*
    [SerializeField] private float velocidadRotacion = 100f;
    [SerializeField] private float tiempoDeRotacion = 3f; // Tiempo en segundos que el dado rota
    private float tiempoRestante;

    void Start()
    {
        tiempoRestante = tiempoDeRotacion; // Inicializamos el tiempo restante
    }

    void Update()
    {
        if (tiempoRestante > 0)
        {
            // Rotación en los tres ejes
            transform.Rotate(Vector3.right * velocidadRotacion * Time.deltaTime);
            transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
            transform.Rotate(Vector3.forward * velocidadRotacion * Time.deltaTime);

            // Reducir el tiempo restante
            tiempoRestante -= Time.deltaTime;
        }
    }
    */
}