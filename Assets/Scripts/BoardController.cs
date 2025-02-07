
using UnityEngine;
using System.Collections.Generic;

using TMPro;


public class BoardController : MonoBehaviour
{

    [SerializeField] private GameObject termiteObject;
    [SerializeField] private GameObject antObject;

    [SerializeField] private List<Square> initialListAnts;
    [SerializeField] private List<Square> initialListTermites;
    public enum SquareState
    {
        Termite,
        Ant,
        Empty,
        Wood,
        TermiteWall,
        AntHill,
        NoWakable
    }
    [SerializeField]
    private Color[] playersColor;

    [SerializeField]
    private Color[] statesColor;


    private List<Square> myBoard = new List<Square>();

    private int squareSelected = -1;
    private int lastClickedSquare = -1;

    [SerializeField] private GameObject orderSquare;
    private GameObject orderSquareClon;
    public static BoardController Instance { get; private set; }
    public Color[] PlayersColor { get => playersColor; set => playersColor = value; }
    public Color[] StatesColor { get => statesColor; set => statesColor = value; }
    public int SquareSelected { get => squareSelected; set => squareSelected = value; }
    public List<Square> MyBoard { get => myBoard; set => myBoard = value; }
    public int LastClickedSquare { get => lastClickedSquare; set => lastClickedSquare = value; }

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
    /// <summary>
    /// Define the initial faction positions
    /// </summary>
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
                antG.QuantitySoldier = 3;
                antG.QuantityWorker = 2;
                antG.Type = MatchController.TypeOfPlayers.Ant;
                antG.objectFaction = (GameObject)Instantiate(antObject, initialListAnts[i].transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                //  initialListAnts[i].Faction = antG;
                antG.objectFaction.transform.SetParent(MyBoard[initialListAnts[i].Id].SquareObject.transform);
                MyBoard[initialListAnts[i].Id].Faction = antG;
                MyBoard[initialListAnts[i].Id].SquareObject.GetComponent<Square>().Faction = antG;
                MyBoard[initialListAnts[i].Id].State = BoardController.SquareState.Ant;
                MyBoard[initialListAnts[i].Id].SquareObject.GetComponent<Square>().State = BoardController.SquareState.Ant;
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
                termiteG.objectFaction.transform.SetParent(MyBoard[initialListAnts[i].Id].SquareObject.transform);

                MyBoard[initialListTermites[i].Id].Faction = termiteG;
                MyBoard[initialListAnts[i].Id].SquareObject.GetComponent<Square>().Faction = termiteG;
                MyBoard[initialListAnts[i].Id].State = BoardController.SquareState.Termite;
                MyBoard[initialListAnts[i].Id].SquareObject.GetComponent<Square>().State = BoardController.SquareState.Termite;
            }

        }
        else
        {
            Debug.Log("There aren't any termite assigned");
        }
        MarkFactionsTurn();
        UpdateTraces();

    }

    public void UpdateTraces()
    {
        string trace = "";
        UIController.Instance.ResetAllTraces();
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {

            trace = MyBoard[i].Id.ToString() + ") St: " + MyBoard[i].State;
            if (MyBoard[i].Faction != null)
            {
                trace += " #F:" + MyBoard[i].Faction.Type.ToString().Substring(0, 3) + "- S:" + MyBoard[i].Faction.QuantitySoldier + " T:" + MyBoard[i].Faction.QuantityWorker;
                UIController.Instance.WriteTrace(trace);
            }




        }
    }

    /// <summary>
    /// Initial assignation of squares
    /// </summary>
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
            MyBoard.Add(sq);
        }
    }
    /// <summary>
    /// Enabled or disabled all squares in the board
    /// </summary>
    /// <param name="state"></param>
    public void EnableSquareCollider(bool state)
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            MyBoard[i].SquareObject.GetComponent<MeshCollider>().enabled = state;
        }
    }

    public void MarkFactionsTurn()
    {
        EnableSquareCollider(false);
        for (int i = 0; i < MyBoard.Count; i++)
        {
            if (MyBoard[i].Faction != null)
            {
                MyBoard[i].Faction.objectFaction.transform.GetChild(0).gameObject.SetActive(false);

            }
            else
            {
                //Debug.Log("Null");
            }
        }


        if (MatchController.Instance.Turn == (int)MatchController.TypeOfPlayers.Ant)
        {




            //Debug.Log("Ant turn");
            for (int i = 0; i < MyBoard.Count; i++)
            {
                if (MyBoard[i].Faction != null && MyBoard[i].Faction.Type == MatchController.TypeOfPlayers.Ant)
                {
                    MyBoard[i].State = SquareState.Ant;
                    MyBoard[i].SquareObject.GetComponent<Square>().State = SquareState.Ant;
                    MyBoard[i].Faction.objectFaction.transform.GetChild(0).gameObject.SetActive(true);
                    MyBoard[i].SquareObject.GetComponent<MeshCollider>().enabled = true;
                }

            }
        }
        else if (MatchController.Instance.Turn == (int)MatchController.TypeOfPlayers.Termite)
        {
            for (int i = 0; i < MyBoard.Count; i++)
            {
                if (MyBoard[i].Faction != null && MyBoard[i].Faction.Type == MatchController.TypeOfPlayers.Termite)
                {
                    MyBoard[i].State = SquareState.Termite;
                    MyBoard[i].SquareObject.GetComponent<Square>().State = SquareState.Termite;
                    MyBoard[i].Faction.objectFaction.transform.GetChild(0).gameObject.SetActive(true);
                    MyBoard[i].SquareObject.GetComponent<MeshCollider>().enabled = true;
                }

            }
        }
        UpdateTraces();
    }

    public void ResetStateSquareColor()
    {
        //Debug.Log("Reseteamos");
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            MyBoard[i].SquareObject.GetComponent<MeshRenderer>().material.color = Color.white;
            MyBoard[i].SquareObject.GetComponent<Square>().Clics = 0;
            MyBoard[i].Clics = 0;
            //Debug.Log("Estadooo"+ myBoard[i].State);
            if (MyBoard[i].State == BoardController.SquareState.NoWakable)
            {

                MyBoard[i].SquareObject.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.NoWakable]; ;
                MyBoard[i].SquareObject.GetComponent<MeshCollider>().enabled = false;
                MyBoard[i].SquareObject.GetComponent<MeshRenderer>().enabled = false;
            }
            else if (MyBoard[i].State == BoardController.SquareState.Wood)
            {

                MyBoard[i].SquareObject.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.Wood]; ;
            }
            else if (MyBoard[i].State == BoardController.SquareState.TermiteWall)
            {
                MyBoard[i].SquareObject.GetComponent<MeshRenderer>().material.color = BoardController.Instance.StatesColor[(int)BoardController.SquareState.TermiteWall]; ;
            }
        }
    }
    public void UnSelected()
    {
        BoardController.Instance.SquareSelected = -1;
        ResetStateSquareColor();
        MarkFactionsTurn();
    }



    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown()
    {
        

        UnSelected();
    }


    ///////////////////////////////////////////// Actions  ////////////////////////////////////////////////////////////

    public void ActionMovingToEmptySquare(int i, MatchController.TypeOfPlayers type)
    {

        if (!MatchController.Instance.CanPerformAction())
        {
            Debug.Log("No actions available this turn");
            return;
        }

        int qs = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantitySoldier;
        int qw = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantityWorker;

        int qsUI = UIController.Instance.GetQuantitySoldiersMovingUI();
        int qwUI = UIController.Instance.GetQuantityWorkersMovingUI();


        Debug.Log("---QWUI" + qwUI);

        qs = qs - qsUI;
        qw = qw - qwUI;

        FactionAbstract faction = null;
        GameObject obj = null;

        if (type == MatchController.TypeOfPlayers.Termite)
        {
            faction = new TermiteGroup();
            obj = termiteObject;
        }
        else
        {
            faction = new AntGroup();
            obj = antObject;
        }

        faction.QuantitySoldier = qsUI;
        faction.QuantityWorker = qwUI;
        faction.Type = type;
        faction.objectFaction = (GameObject)Instantiate(obj, MyBoard[i].SquareObject.transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        faction.objectFaction.transform.SetParent(MyBoard[i].SquareObject.transform);

        MyBoard[i].Faction = faction;
        MyBoard[i].SquareObject.GetComponent<Square>().Faction = faction;
        MyBoard[i].State = BoardController.SquareState.Termite;
        MyBoard[i].SquareObject.GetComponent<Square>().State = BoardController.SquareState.Termite;

        //Update selected Square
        MyBoard[squareSelected].Faction.QuantitySoldier = qs;
        MyBoard[squareSelected].Faction.QuantityWorker = qw;

        //Update the number of units are in the selected square
        /*MyBoard[squareSelected].UpdateUnitCountText();
        MyBoard[i].UpdateUnitCountText();*/

        if (qs == 0 && qw == 0)
        {
            //Destroy faction
            Destroy(MyBoard[squareSelected].Faction.objectFaction);
            MyBoard[squareSelected].Faction = null;
            MyBoard[squareSelected].State = BoardController.SquareState.Empty;
            MyBoard[squareSelected].SquareObject.GetComponent<Square>().State = BoardController.SquareState.Empty;
        }

        Debug.Log("Llamando a UseAction()...");
        MatchController.Instance.UseAction();
        UIController.Instance.UpdateActionsText();

        UpdateTraces();
        UnSelected();


    }

}

