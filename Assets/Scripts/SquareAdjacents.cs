using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class SquareAdjacents : MonoBehaviour
{

    List<Square> squares = new List<Square>();
    [SerializeField] private GameObject[] squaresObjects;

    public List<Square> Squares { get => squares; set => squares = value; }

    private void InitializeSquares()
    {
        foreach (GameObject squareObject in squaresObjects)
        {
            Square square = squareObject.GetComponent<Square>();

            if (square != null)
            {
                squares.Add(square);
            }
            else
            {
                Debug.LogWarning($"El GameObject {squareObject.name} no tiene un componente Square.");
            }
        }

        Debug.Log($"Se inicializó la lista con {squares.Count} elementos.");
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
