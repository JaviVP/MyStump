using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SquareAdjacents : MonoBehaviour
{

    private List<Square> squares = new List<Square>();
    [SerializeField] private GameObject[] squaresObjects;

    public List<Square> Squares { get => squares; set => squares = value; }

    // Inicializa la lista de Squares desde el array de GameObjects
    private void InitializeSquares()
    {
        squares.Clear();

        foreach (GameObject obj in squaresObjects)
        {
            Square square = obj.GetComponent<Square>();
            if (square != null)
            {
                squares.Add(square);
                // Asignar la referencia de SquareAdjacents a cada Square
                //square.SquareAdjacents = this;
            }
            else
            {
                Debug.LogWarning($"El GameObject {obj.name} no tiene un componente Square.");
            }
        }

        //Debug.Log($"Inicializados {squares.Count} cuadrados.");
    }

    public void HighlightAllAdjacents()
    {
        foreach (GameObject obj in squaresObjects)
        {
            if (obj != null)
            {
                //Check state square
                if (obj.GetComponent<Square>().State == BoardController.SquareState.Empty)
                {
                    obj.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.Empty];
                }
                else if (obj.GetComponent<Square>().State == BoardController.SquareState.NoWakable)
                {
                    obj.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int) BoardController.SquareState.NoWakable];
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
        
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeSquares();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
