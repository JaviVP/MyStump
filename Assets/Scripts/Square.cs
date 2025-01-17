using UnityEngine;
using System.Collections.Generic;

public class Square : MonoBehaviour
{
    private int id;
    private GameObject squareObject;
    private SquareAdjacents squareAdjacents;
    private MeshRenderer meshRenderer;

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
        squareAdjacents = FindObjectOfType<SquareAdjacents>();

    }

    private void HighlightSquare()
    {
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.red;
        }
        else
        {
            Debug.LogError($"El MeshRenderer no está asignado en la casilla {gameObject.name}");
            return;
        }
/*      foreach (Square adjacent in squareAdjacents.Squares)
    {
        if (adjacent != null)
        {
            MeshRenderer adjacentRenderer = adjacent.GetComponent<MeshRenderer>();
            if (adjacentRenderer != null)
            {
                adjacentRenderer.material.color = Color.blue;
            }
            else
            {
                Debug.LogWarning($"El MeshRenderer no está asignado en la casilla adyacente {adjacent.gameObject.name}");
            }
        }
        else
        {
            Debug.LogWarning($"Casilla adyacente nula detectada en {gameObject.name}");
        }
    }*/
    }

    private void OnMouseDown()
    {
        Board.Instance.StateSquareColor();
        // Cambiar color de las casillas adyacentes
        squareAdjacents.HighlightAllAdjacents(Color.yellow);
        if (meshRenderer != null)
        {
            meshRenderer.material.color = Color.red;
        }
        else
        {
            Debug.LogError($"El MeshRenderer no está asignado en la casilla {gameObject.name}");
            return;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
