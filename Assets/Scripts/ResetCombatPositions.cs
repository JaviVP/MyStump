using UnityEngine;

public class ResetCombatPositions : MonoBehaviour
{
    [SerializeField] private GameObject stampide;
    [SerializeField] private GameObject dice1;
    [SerializeField] private GameObject dice2;

    private Vector3 stampidePosIni;
    private Vector3 dice1PosIni;
    private Vector3 dice2PosIni;

    private void Start()
    {
        // Guardamos las posiciones iniciales de los objetos al inicio
        if (stampide != null) stampidePosIni = stampide.transform.localPosition;
        if (dice1 != null) dice1PosIni = dice1.transform.localPosition;
        if (dice2 != null) dice2PosIni = dice2.transform.localPosition;

        // Para ver en la consola las posiciones iniciales
        Debug.Log("Posición inicial objeto 1 (stampide): " + stampidePosIni);
        Debug.Log("Posición inicial objeto 2 (dice1): " + dice1PosIni);
        Debug.Log("Posición inicial objeto 3 (dice2): " + dice2PosIni);
    }

    // Función para resetear las posiciones locales de los objetos a su estado inicial
    public void ResetPositions()
    {
        if (stampide != null)
            stampide.transform.localPosition = stampidePosIni;
        if (dice1 != null)
            dice1.transform.localPosition = dice1PosIni;
        if (dice2 != null)
            dice2.transform.localPosition = dice2PosIni;

        // Mostrar un mensaje en la consola cuando se reseteen las posiciones
        Debug.Log("Posiciones reseteadas.");
    }
}

