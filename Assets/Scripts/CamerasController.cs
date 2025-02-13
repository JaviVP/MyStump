using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.Cinemachine;

public class CamerasController : MonoBehaviour
{
    [SerializeField] private CinemachineCamera camera1;          // Primera c�mara en perspectiva
    [SerializeField] private CinemachineCamera camera2;          // Segunda c�mara en perspectiva
    [SerializeField] private CinemachineCamera topDownCamera;    // C�mara cenital en perspectiva
    [SerializeField] private Button resetButton;      // Bot�n de la UI para resetear la c�mara

    [SerializeField] private float perspectiveThreshold = 60f;  // Umbral para activar la c�mara cenital desde la perspectiva
    [SerializeField] private float topDownThreshold = 20f;     // Umbral para volver a la c�mara en perspectiva desde la c�mara cenital

    private CinemachineCamera activeCamera;
    private CinemachineCamera lastUsedCamera;

    private bool isTopDownActive = false;
    private bool zoomingToTopDown = false;

    private float lastTapTime = 0f;
    private float doubleTapThreshold = 0.3f;

    private float currentZoom;

    private Vector3 initialPosition1, initialPosition2, initialPositionTop;
    private Quaternion initialRotation1, initialRotation2, initialRotationTop;

    [SerializeField] private GameObject endTurnButton;

    public static CamerasController Instance { get; private set; }

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
    void Start()
    {

       
        // Guardar posici�n y rotaci�n inicial de las c�maras
        initialPosition1 = camera1.transform.position;
        initialRotation1 = camera1.transform.rotation;

        initialPosition2 = camera2.transform.position;
        initialRotation2 = camera2.transform.rotation;

        initialPositionTop = topDownCamera.transform.position;
        initialRotationTop = topDownCamera.transform.rotation;

        //topDownCamera.transform.rotation = Quaternion.Euler(90, 0, 180);

        // Comienza solo con la c�mara 1 activa
        //ActivateCamera(camera1);

        // Asignar el evento del bot�n de reset
        if (resetButton != null)
        {
            resetButton.onClick.AddListener(ResetCamera);
        }
    }

    void Update()
    {
        // Cambiar entre las c�maras en perspectiva con la tecla de espacio
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            if (activeCamera == camera1)
            {
                ActivateCamera(camera2);
                topDownCamera.transform.rotation = Quaternion.Euler(90, 0, 180);
            }
            else if (activeCamera == camera2)
            {
                ActivateCamera(camera1);
                topDownCamera.transform.rotation = Quaternion.Euler(90, 0, -180);
            }
        }
        */
        //Debug para cambiar camaras
        /*
        if (Input.touchCount == 1 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            float currentTime = Time.time;
            if (currentTime - lastTapTime < doubleTapThreshold)
            {
                SwitchCamera();
            }
            lastTapTime = currentTime;
        }
       */
        CheckTopDownCamera();
    }

    public void ActivateCamera(CinemachineCamera newCamera)
    {
        // Si la c�mara ya est� activa, no hacer nada
        if (activeCamera == newCamera) return;

        // Desactivar la c�mara actual si est� activa
        if (activeCamera != null)
        {
            activeCamera.gameObject.SetActive(false);
        }

        // Activar la nueva c�mara
        activeCamera = newCamera;
        activeCamera.gameObject.SetActive(true);

        // Si no es la c�mara cenital, guardar la �ltima c�mara usada
        if (newCamera != topDownCamera)
        {
            lastUsedCamera = newCamera;
            isTopDownActive = false;
        }
    }

    public void CheckTopDownCamera()
    {
        if (activeCamera == null) return;

        currentZoom = activeCamera.Lens.FieldOfView;

        // Si la c�mara activa es una c�mara en perspectiva y el zoom alcanza el umbral de perspectiva, cambia a la c�mara cenital
        if (activeCamera != topDownCamera && currentZoom == perspectiveThreshold && !isTopDownActive && !zoomingToTopDown)
        {
            lastUsedCamera = activeCamera;
            lastUsedCamera.Lens.FieldOfView = 60.0f;
            ActivateCamera(topDownCamera);
            isTopDownActive = true;
            zoomingToTopDown = true;
            endTurnButton.SetActive(false);
        }
        else if (activeCamera == topDownCamera && currentZoom == topDownThreshold && isTopDownActive && zoomingToTopDown)
        {
            ActivateCamera(lastUsedCamera);
            isTopDownActive = false;
            zoomingToTopDown = false;
            topDownCamera.Lens.FieldOfView = 26.0f;
            endTurnButton.SetActive(true);
        }
    }

    // Funci�n para resetear las c�maras a sus estados iniciales
    public void ResetCamera()
    {
        camera1.transform.position = initialPosition1;
        camera1.transform.rotation = initialRotation1;
        camera1.Lens.FieldOfView = 60.0f;

        camera2.transform.position = initialPosition2;
        camera2.transform.rotation = initialRotation2;
        camera2.Lens.FieldOfView = 60.0f;

        topDownCamera.transform.position = initialPositionTop;
        topDownCamera.transform.rotation = initialRotationTop;
        topDownCamera.Lens.FieldOfView = 26.0f;



    }

    public void SwitchCamera()
    {
       
            if (activeCamera == camera1)
            {
                Debug.Log("Activo Cenital p1");
                ActivateCamera(camera2);
                topDownCamera.transform.rotation = Quaternion.Euler(90, 0, 0);
            }
            else if (activeCamera == camera2)
            {
                Debug.Log("Activo Cenital p2");
           
                ActivateCamera(camera1);
                topDownCamera.transform.rotation = Quaternion.Euler(90, 0, 180);
            }

    }

    public CinemachineCamera GetActiveCamera()
    {
        return activeCamera;
    }

}


