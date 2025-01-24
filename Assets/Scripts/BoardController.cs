using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;
using TMPro;

public class BoardController : MonoBehaviour
{

    [SerializeField] private GameObject termiteObject;
    [SerializeField] private GameObject antObject;

    [SerializeField] private List<Square> initialListAnts;
    [SerializeField] private List<Square> initialListTermites;
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

        Invoke("InitialFactions", 0.1f);
       

    }
    
    public void InitialFactions()
    {
       // int randomPoint = Random.Range(0, this.gameObject.transform.childCount);
       /* myBoard[70].SquareObject.GetComponent<MeshRenderer>().material.color = Color.white;
        myBoard[75].SquareObject.GetComponent<MeshRenderer>().material.color = Color.white;*/

        if (initialListAnts.Count > 0)
        {
            for (int i = 0; i < initialListAnts.Count; i++)
            {
                AntGroup antG = new AntGroup();
                antG.QuantitySoldier = 1;
                antG.QuantityWorker = 0;
                antG.Type = MatchController.TypeOfPlayers.Ant;
                antG.objectFaction = (GameObject)Instantiate(antObject, initialListAnts[i].transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                //  initialListAnts[i].Faction = antG;
                antG.objectFaction.transform.SetParent(myBoard[initialListAnts[i].Id].SquareObject.transform);
                myBoard[initialListAnts[i].Id].Faction = antG;

            }

        }
        else
        {
            Debug.Log("There aren't any ant assigned");
        }

        if (initialListTermites.Count > 0)
        {
            for (int i = 0; i < initialListTermites.Count; i++)
            {
                TermiteGroup termiteG = new TermiteGroup();
                termiteG.QuantitySoldier = 1;
                termiteG.QuantityWorker = 0;
                termiteG.Type = MatchController.TypeOfPlayers.Termite;
                termiteG.objectFaction = (GameObject)Instantiate(termiteObject, initialListTermites[i].transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                // initialListTermites[i].Faction = termiteG;
                termiteG.objectFaction.transform.SetParent(myBoard[initialListAnts[i].Id].SquareObject.transform);
                Debug.Log(initialListTermites[i].Id);
                myBoard[initialListTermites[i].Id].Faction = termiteG;
            }

        }
        else
        {
            Debug.Log("There aren't any termite assigned");
        }
        MarkFactionsTurn();


    }
    public void AssignObjectsSquare()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Square sq = new Square();
            sq.Id = i;
            sq.SquareObject = this.gameObject.transform.GetChild(i).gameObject;
            sq.SquareObject.GetComponent<Square>().Id = i;
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

    public void MarkFactionsTurn()
    {
        for (int i = 0; i < myBoard.Count; i++)
        {
            if (myBoard[i].Faction != null)
            {
                myBoard[i].Faction.objectFaction.transform.GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Null");
            }
        }


        if (MatchController.Instance.Turn == (int)MatchController.TypeOfPlayers.Ant)
        {




            Debug.Log("Ant turn");
            for (int i = 0; i < myBoard.Count; i++)
            {
                if (myBoard[i].Faction != null && myBoard[i].Faction.Type== MatchController.TypeOfPlayers.Ant)
                {
                    myBoard[i].Faction.objectFaction.transform.GetChild(0).gameObject.SetActive(true);
                }

            }
        }
        else if (MatchController.Instance.Turn == (int)MatchController.TypeOfPlayers.Termite)
        {
            for (int i = 0; i < myBoard.Count; i++)
            {
                if (myBoard[i].Faction != null && myBoard[i].Faction.Type == MatchController.TypeOfPlayers.Termite)
                {
                    myBoard[i].Faction.objectFaction.transform.GetChild(0).gameObject.SetActive(true);
                }

            }
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
                myBoard[i].SquareObject.GetComponent<MeshRenderer>().enabled= false;
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

