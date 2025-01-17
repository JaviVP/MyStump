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

        Debug.Log($"Inicializados {squares.Count} cuadrados.");
    }

    public void HighlightAllAdjacents(Color color)
    {
        foreach (GameObject obj in squaresObjects)
        {
            if (obj != null)
            {
                obj.GetComponent<MeshRenderer>().material.color = color;
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
