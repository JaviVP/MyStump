using UnityEngine;
using System.Collections.Generic;

public class Square : MonoBehaviour
{
    private int id;
    private GameObject squareObject;
    private List<Square> squareAdjacents;
    private MeshRenderer meshRenderer;
    [SerializeField]
    private BoardController.SquareState state;
    public GameObject SquareObject { get => squareObject; set => squareObject = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cambiar de color el objeto
        meshRenderer = GetComponent<MeshRenderer>();

        //Volver al color anterior
        GetComponent<MeshRenderer>().material.color = Color.white;

        //Poner de un color el clicado y otro color los adjacentes.
        //HighlightSquare();
        //Crear un método en la clase board que pongar todos los square a white(estado normal).
        

    }

    private void HighlightSquare()
    {
        BoardController.Instance.ResetStateSquareColor();
        // Cambiar color de las casillas adyacentes
        //squareAdjacents.HighlightAllAdjacents(Color.yellow);
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.red;
            GetComponent<SquareAdjacents>().HighlightAllAdjacents(Color.green);
        }
        else
        {
            Debug.LogError($"El MeshRenderer no está asignado en la casilla {gameObject.name}");
            return;
        }
    }

    private void OnMouseDown()
    {
        HighlightSquare();

    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
