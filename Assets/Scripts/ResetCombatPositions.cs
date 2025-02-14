using UnityEngine;

public class ResetCombatPositions : MonoBehaviour
{
    [SerializeField] private GameObject stampide;
    [SerializeField] private GameObject dice1;
    [SerializeField] private GameObject dice2;
    [SerializeField] private float stampideTime = 3.7f;

    private Vector3 stampidePosIni;
    private Vector3 dice1PosIni;
    private Vector3 dice2PosIni;

    private Quaternion stampideRotIni;
    private Quaternion dice1RotIni;
    private Quaternion dice2RotIni;

    public static ResetCombatPositions Instance { get; private set; }

    private void Awake()
    {
        SaveInitialTransform();

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

    private void SaveInitialTransform()
    {
        if (stampide != null)
        {
            stampidePosIni = stampide.transform.localPosition;
            stampideRotIni = stampide.transform.localRotation;
        }
        if (dice1 != null)
        {
            dice1PosIni = dice1.transform.localPosition;
            dice1RotIni = dice1.transform.localRotation;
        }
        if (dice2 != null)
        {
            dice2PosIni = dice2.transform.localPosition;
            dice2RotIni = dice2.transform.localRotation;
        }
    }

    public void ResetTransform()
    {
        if (stampide != null)
        {
            stampide.transform.localPosition = stampidePosIni;
            stampide.transform.localRotation = stampideRotIni;
        }
        if (dice1 != null)
        {
            dice1.transform.localPosition = dice1PosIni;
            dice1.transform.localRotation = dice1RotIni;
        }
        if (dice2 != null)
        {
            dice2.transform.localPosition = dice2PosIni;
            dice2.transform.localRotation = dice2RotIni;
        }

        Debug.Log("Posiciones y rotaciones reseteadas.");
    }

    public float StampideTime()
    {
        return stampideTime;
    }
}

