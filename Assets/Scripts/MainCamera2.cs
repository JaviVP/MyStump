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

    private CinemachineCamera virtualCamera2;          // Referencia a la c�mara

    void Start()
    {
        virtualCamera2 = GetComponent<CinemachineCamera>();
    }

    void Update()
    {
        MoveCamera();
        ZoomCamera();
    }

    void MoveCamera()
    {


        Vector3 newPosition = transform.position;

        // Movimiento en el eje Z con el clic izquierdo
        if (Input.GetMouseButton(0)) // Bot�n izquierdo del rat�n
        {
            float mouseX = Input.GetAxis("Mouse Y") * moveSpeed;
            newPosition.z += -mouseX; // Invertir movimiento en el eje Z
        }

        // Movimiento en el eje X con el clic derecho
        if (Input.GetMouseButton(1)) // Bot�n derecho del rat�n
        {
            float mouseY = Input.GetAxis("Mouse X") * moveSpeed;
            newPosition.x += -mouseY; // Invertir movimiento en el eje X
        }

        // Aplicar los l�mites a la posici�n
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        newPosition.z = Mathf.Clamp(newPosition.z, minZ, maxZ);

        // Actualizar la posici�n de la c�mara
        transform.position = newPosition;
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
        Debug.Log(Input.touchCount);
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
