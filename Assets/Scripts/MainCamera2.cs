using UnityEngine;
using Unity.Cinemachine;
public class MainCamera2 : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.1f;  // Sensibilidad del movimiento
    [SerializeField] private float zoomSpeed = 10f;   // Velocidad de zoom
    [SerializeField] private float minZoom = 10f;     // Zoom m�nimo
    [SerializeField] private float maxZoom = 60f;     // Zoom m�ximo

    [SerializeField] private float minX = -10f;       // L�mite m�nimo del eje X
    [SerializeField] private float maxX = 10f;        // L�mite m�ximo del eje X
    [SerializeField] private float minZ = -10f;       // L�mite m�nimo del eje Z
    [SerializeField] private float maxZ = 10f;        // L�mite m�ximo del eje Z

    private CinemachineCamera virtualCamera2;
    private Vector2 lastTouchPosition;
    private bool isTouching = false;// Referencia a la c�mara

    void Start()
    {
        virtualCamera2 = GetComponent<CinemachineCamera>();
    }

    void Update()
    {
        MoveCameraPC();
        MoveCameraTouch();
        ZoomCamera();
    }

    void MoveCameraPC()
    {
        Vector3 newPosition = transform.position;

        // Movimiento en el eje X (derecha e izquierda)
        if (Input.GetMouseButton(1)) // Botón derecho del ratón
        {
            float mouseX = Input.GetAxis("Mouse X") * moveSpeed;
            newPosition.x += -mouseX; // Invertir movimiento en el eje X
        }

        // Movimiento en el eje Z (arriba y abajo)
        if (Input.GetMouseButton(0)) // Botón izquierdo del ratón
        {
            float mouseY = Input.GetAxis("Mouse Y") * moveSpeed;
            newPosition.z += -mouseY; // Invertir movimiento en el eje Z
        }

        // Aplicar los límites a la posición
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // Actualizar la posición de la cámara
        transform.position = newPosition;
    }

    void MoveCameraTouch()
    {
        if (Input.touchCount == 1) // Solo un dedo en pantalla
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                lastTouchPosition = touch.position;
                isTouching = true;
            }
            else if (touch.phase == TouchPhase.Moved && isTouching)
            {
                Vector2 delta = touch.deltaPosition * moveSpeed;

                Vector3 newPosition = transform.position;
                newPosition.x -= delta.x * 0.01f; // Invertido para que se mueva correctamente
                newPosition.z -= delta.y * 0.01f; // Invertido para que se mueva correctamente

                // Aplicar límites
                newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
                newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

                transform.position = newPosition;

                lastTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isTouching = false;
            }
        }
    }

    void ZoomCamera()
    {


        // Control del zoom con la rueda del rat�n
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (scrollInput != 0f && virtualCamera2 != null)
        {
            LensSettings lens = virtualCamera2.Lens; // Obtener la estructura LensSettings
            lens.FieldOfView -= scrollInput * zoomSpeed; // Modificar el FOV
            lens.FieldOfView = Mathf.Clamp(lens.FieldOfView, minZoom, maxZoom); // Limitar el zoom
            virtualCamera2.Lens = lens; // Aplicar los cambios a la c�mara
        }
       
        if (Input.touchCount == 2)
        {
           
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Posición anterior de los dedos
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Distancia anterior y actual entre los dedos
            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            // Diferencia de distancia entre los dos frames
            float difference = currentMagnitude - prevMagnitude;

            // Aplicar zoom con sensibilidad ajustada
            LensSettings lens = virtualCamera2.Lens;
            lens.FieldOfView -= difference * zoomSpeed * 0.01f;
            lens.FieldOfView = Mathf.Clamp(lens.FieldOfView, minZoom, maxZoom);
            virtualCamera2.Lens = lens;
        }
    }
}
