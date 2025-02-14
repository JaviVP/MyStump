using UnityEngine;

public class DiceMeshController : MonoBehaviour
{
    [SerializeField] private GameObject antStampide;
    [SerializeField] private float velocity;
    [SerializeField] private float stampideTime;
    [SerializeField] private GameObject dadosFinal;
    [SerializeField] private GameObject dadosI;
    [SerializeField] private GameObject dadosD;
    private Vector3 stampidePos;
    private float tiempoRestante;
    private float stampideTimeLeft;

    private bool isMoving = false; // Variable para controlar si el objeto debe moverse.
    //private bool animEnd = false; // Variable para controlar si la animación ha terminado.
    private Animator animator;

    private void Start()
    {
        stampideTimeLeft = stampideTime;
        Debug.Log("ejecuto hormiga");
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
                AttackerMeshChanger();
                DefenderMeshChanger();
                Debug.Log("Movimiento finalizado");

            }
        }else
        {



        }
     
    }

    public void UpdateStampide()
    {
        

    }

    private void ActivateAnimation()
    {
        animator.SetTrigger("FinalizarMovimiento"); // "FinalizarMovimiento" es el nombre del Trigger en el Animator
    }

    public void AttackerMeshChanger()
    {
        if (DiceThrower.Instance.AttackerDiceType() == 0)
        {

            //Codigo Cambio de mesh de dado
            if (DiceThrower.Instance.RawAttackerSwords() == 0 && DiceThrower.Instance.RawAttackerShields() == 0)
            {
                //Codigos Cambio mesh Caras
                Debug.Log("Mostrar Calavera mesh");
            }
            else if (DiceThrower.Instance.RawAttackerSwords() == 1 && DiceThrower.Instance.RawAttackerShields() == 0)
            {
                Debug.Log("Mostrar 1 Lanza mesh");


            }
            else if (DiceThrower.Instance.RawAttackerSwords() == 0 && DiceThrower.Instance.RawAttackerShields() == 1)
            {
                Debug.Log("Mostrar 1 Escudo mesh");


            }

        }else if (DiceThrower.Instance.AttackerDiceType() == 1)
        {
            if (DiceThrower.Instance.RawAttackerSwords() == 0 && DiceThrower.Instance.RawAttackerShields() == 0)
            {
                //Codigos Cambio mesh Caras
                Debug.Log("Mostrar Calavera mesh");
            }
            else if (DiceThrower.Instance.RawAttackerSwords() == 1 && DiceThrower.Instance.RawAttackerShields() == 0)
            {
                Debug.Log("Mostrar 1 Lanza mesh");


            }
            else if (DiceThrower.Instance.RawAttackerSwords() == 0 && DiceThrower.Instance.RawAttackerShields() == 1)
            {
                Debug.Log("Mostrar 1 Escudo mesh");


            }
            else if (DiceThrower.Instance.RawAttackerSwords() == 2 && DiceThrower.Instance.RawAttackerShields() == 2)
            {
                Debug.Log("Mostrar 2 Lanzas mesh");


            }


        }
        else if(DiceThrower.Instance.AttackerDiceType() == 2)
        {
            if (DiceThrower.Instance.RawAttackerSwords() == 2 && DiceThrower.Instance.RawAttackerShields() == 0)
            {
                //Codigos Cambio mesh Caras
                Debug.Log("Mostrar 2 Lanzas mesh");
            }
            else if (DiceThrower.Instance.RawAttackerSwords() == 1 && DiceThrower.Instance.RawAttackerShields() == 0)
            {
                Debug.Log("Mostrar 1 Lanza mesh");


            }
            else if (DiceThrower.Instance.RawAttackerSwords() == 0 && DiceThrower.Instance.RawAttackerShields() == 1)
            {
                Debug.Log("Mostrar 1 Escudo mesh");


            }
            else if (DiceThrower.Instance.RawAttackerSwords() == 3 && DiceThrower.Instance.RawAttackerShields() == 3)
            {
                Debug.Log("Mostrar 3 Lanzas mesh");


            }


        }


    }

    public void DefenderMeshChanger()
    {
        if (DiceThrower.Instance.DefenderDiceType() == 0)
        {

            //Codigo Cambio de mesh de dado
            if (DiceThrower.Instance.RawDefenderSwords() == 0 && DiceThrower.Instance.RawDefenderShields() == 0)
            {
                //Codigos Cambio mesh Caras
                Debug.Log("Mostrar Calavera mesh");
            }
            else if (DiceThrower.Instance.RawDefenderSwords() == 1 && DiceThrower.Instance.RawDefenderShields() == 0)
            {
                Debug.Log("Mostrar 1 Lanza mesh");


            }
            else if (DiceThrower.Instance.RawDefenderSwords() == 0 && DiceThrower.Instance.RawDefenderShields() == 1)
            {
                Debug.Log("Mostrar 1 Escudo mesh");


            }

        }
        else if (DiceThrower.Instance.DefenderDiceType() == 1)
        {
            if (DiceThrower.Instance.RawDefenderSwords() == 0 && DiceThrower.Instance.RawDefenderShields() == 0)
            {
                //Codigos Cambio mesh Caras
                Debug.Log("Mostrar Calavera mesh");
            }
            else if (DiceThrower.Instance.RawDefenderSwords() == 1 && DiceThrower.Instance.RawDefenderShields() == 0)
            {
                Debug.Log("Mostrar 1 Lanza mesh");


            }
            else if (DiceThrower.Instance.RawDefenderSwords() == 0 && DiceThrower.Instance.RawDefenderShields() == 1)
            {
                Debug.Log("Mostrar 1 Escudo mesh");


            }
            else if (DiceThrower.Instance.RawDefenderSwords() == 2 && DiceThrower.Instance.RawDefenderShields() == 2)
            {
                Debug.Log("Mostrar 2 Escudos mesh");


            }


        }
        else if (DiceThrower.Instance.DefenderDiceType() == 2)
        {
            if (DiceThrower.Instance.RawDefenderSwords() == 2 && DiceThrower.Instance.RawDefenderShields() == 0)
            {
                //Codigos Cambio mesh Caras
                Debug.Log("Mostrar 2 Lanzas mesh");
            }
            else if (DiceThrower.Instance.RawDefenderSwords() == 1 && DiceThrower.Instance.RawDefenderShields() == 0)
            {
                Debug.Log("Mostrar 1 Lanza mesh");


            }
            else if (DiceThrower.Instance.RawDefenderSwords() == 0 && DiceThrower.Instance.RawDefenderShields() == 1)
            {
                Debug.Log("Mostrar 1 Escudo mesh");


            }
            else if (DiceThrower.Instance.RawDefenderSwords() == 3 && DiceThrower.Instance.RawDefenderShields() == 3)
            {
                Debug.Log("Mostrar 3 Escudos mesh");


            }


        }




    }
   
}