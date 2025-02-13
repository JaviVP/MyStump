using UnityEngine;

public class activateStampide : MonoBehaviour
{
    [SerializeField] private GameObject antStampide;
    [SerializeField] private float velocity;
    [SerializeField] private float stampideTime;
    [SerializeField] private GameObject dadosFinal;
    [SerializeField] private GameObject dadosI;
    [SerializeField] private GameObject dadosD;
    [SerializeField] private float velocidadRotacion = 100f;
    [SerializeField] private float tiempoDeRotacion = 3f; // Tiempo en segundos que el dado rota
    private float tiempoRestante;
    private float stampideTimeLeft;

    private bool isMoving = false; // Variable para controlar si el objeto debe moverse.
    private bool animEnd = false; // Variable para controlar si la animación ha terminado.
    private Animator animator;

    private void Start()
    {
        stampideTimeLeft = stampideTime;
        tiempoRestante = tiempoDeRotacion;
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

        // Verificamos si la animación ha terminado
        if (animEnd == false)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("FinalizarMovimiento") && stateInfo.normalizedTime >= 1)
            {
                // La animación ha terminado
                animEnd = true;
                Debug.Log("Animación completada, rotando dados.");
            }
        }

        // Si la animación ha terminado, empezar a rotar los dados
        if (animEnd && tiempoRestante > 0)
        {
            dadosI.transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
            dadosD.transform.Rotate(Vector3.up * velocidadRotacion * Time.deltaTime);
            tiempoRestante -= Time.deltaTime;
        }
    }

    private void ActivateAnimation()
    {
        // Asegúrate de que el Animator tiene un Trigger configurado en el controlador de animación
        animator.SetTrigger("FinalizarMovimiento"); // "FinalizarMovimiento" es el nombre del Trigger en el Animator
    }
}