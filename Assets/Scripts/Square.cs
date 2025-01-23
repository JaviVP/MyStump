using UnityEngine;
using System.Collections.Generic;
using System.Drawing;

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
        ChangeColorSquare(this.gameObject);



    }

    public void ChangeColorSquare(GameObject obj)
    {

        if (obj.GetComponent<Square>().State == BoardController.SquareState.Empty)
        {
            obj.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.Empty];
        }
        else if (obj.GetComponent<Square>().State == BoardController.SquareState.NoWakable)
        {
            obj.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.NoWakable];
        }
        else if (obj.GetComponent<Square>().State == BoardController.SquareState.Ant)
        {
            obj.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.Ant];
        }
        else if (obj.GetComponent<Square>().State == BoardController.SquareState.Termite)
        {
            obj.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.Termite];
        }
        else if (obj.GetComponent<Square>().State == BoardController.SquareState.Wood)
        {
            obj.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.Wood];
        }
        else if (obj.GetComponent<Square>().State == BoardController.SquareState.TermiteWall)
        {
            obj.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.TermiteWall];
        }
    }


    private void HighlightSquare()
    {

        BoardController.Instance.ResetStateSquareColor();
        // Cambiar color de las casillas adyacentes
        //squareAdjacents.HighlightAllAdjacents(Color.yellow);
        if (meshRenderer != null)
        {
            GetComponent<MeshRenderer>().material.color = BoardController.Instance.PlayersColor[MatchController.Instance.Turn];
            GetComponent<SquareAdjacents>().HighlightAllAdjacents();
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
