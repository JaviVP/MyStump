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
    public BoardController.SquareState State { get => state; set => state = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Cambiar de color el objeto
        meshRenderer = GetComponent<MeshRenderer>();

        //Volver al color anterior
       

        //Poner de un color el clicado y otro color los adjacentes.
        //HighlightSquare();
        //Crear un método en la clase board que pongar todos los square a white(estado normal).
        
        if (State== BoardController.SquareState.Ant)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (State == BoardController.SquareState.Termite)
        {
            GetComponent<MeshRenderer>().material.color = Color.yellow;
        }
        else if (State == BoardController.SquareState.NoWakable)
        {
            GetComponent<MeshRenderer>().material.color = Color.black;
        }
        else if (State == BoardController.SquareState.Wood)
        {
            GetComponent<MeshRenderer>().material.color = Color.cyan;
        }
        else if (State == BoardController.SquareState.TermiteWall)
        {
            GetComponent<MeshRenderer>().material.color = Color.blue;
        }
        else
        {
            GetComponent<MeshRenderer>().material.color = Color.white;
        }


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
