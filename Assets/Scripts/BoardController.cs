using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class BoardController : MonoBehaviour
{

    public enum SquareState
    {
        Empty,
        Ant,
        Termite,
        Wood,
        TermiteWall,
        NoWakable
    }
    [SerializeField]
    private Color[] playersColor;

    [SerializeField]
    private Color[] statesColor;


    private List <Square> myBoard = new List<Square> ();
    [SerializeField] private GameObject orderSquare;
    private GameObject orderSquareClon;
    public static BoardController Instance { get; private set; }
    public Color[] PlayersColor { get => playersColor; set => playersColor = value; }
    public Color[] StatesColor { get => statesColor; set => statesColor = value; }

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



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AssignObjectsSquare();

    }
    public void AssignObjectsSquare()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Square sq = new Square();
            sq.SquareObject = this.gameObject.transform.GetChild(i).gameObject;
            sq.State = this.gameObject.transform.GetChild(i).GetComponent<Square>().State;
            orderSquare.SetActive(true);
            orderSquareClon = (GameObject)Instantiate(orderSquare);
            orderSquareClon.transform.SetParent(transform.GetChild(i).gameObject.transform);
            orderSquareClon.transform.localPosition = new Vector3(0, 0.256f, 0);
            orderSquareClon.transform.localRotation = Quaternion.Euler(90, -100.411f, -10.576f);
            orderSquareClon.GetComponent<TMP_Text>().text = "" + i;
            myBoard.Add(sq);
        }
    }

    public void EnableSquareCollider(bool state)
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            myBoard[i].SquareObject.GetComponent<MeshCollider>().enabled = state;
        }
    }

    public void ResetStateSquareColor()
    {
        Debug.Log("Reseteamos");
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            myBoard[i].SquareObject.GetComponent<MeshRenderer>().material.color= Color.white;
            Debug.Log("Estadooo"+ myBoard[i].State);
            if (myBoard[i].State == BoardController.SquareState.NoWakable)
            {
               
                myBoard[i].SquareObject.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.NoWakable]; ;
                myBoard[i].SquareObject.GetComponent<MeshCollider>().enabled= false;
            }
            else if  (myBoard[i].State == BoardController.SquareState.Wood)
            {

                myBoard[i].SquareObject.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.Wood]; ;
            }
            else if (myBoard[i].State == BoardController.SquareState.TermiteWall)
            {
                myBoard[i].SquareObject.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.TermiteWall]; ;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
