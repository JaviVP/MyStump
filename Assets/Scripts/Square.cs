using UnityEngine;
using System.Collections.Generic;
using System.Drawing;

public class Square : MonoBehaviour
{
    
    private int id;
    private GameObject squareObject;
    private List<Square> squareAdjacents;
    private MeshRenderer meshRenderer;

    private FactionAbstract faction;
    [SerializeField]
    private BoardController.SquareState state;
    public GameObject SquareObject { get => squareObject; set => squareObject = value; }
    public BoardController.SquareState State { get => state; set => state = value; }
    public FactionAbstract Faction { get => faction; set => faction = value; }
    public int Id { get => id; set => id = value; }

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
            obj.GetComponent<MeshRenderer>().enabled = false;
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

    /// <summary>
    /// List of Actions
    /// </summary>
    private void HighlightSquare()
    {
        if (GetComponent<Square>().State == BoardController.SquareState.Ant && BoardController.Instance.SquareSelected == -1)
        {
                Debug.Log("Posicion: "+ this.id);
                BoardController.Instance.SquareSelected = this.id;
                Debug.Log("Soy una hormiga");
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
        else if (GetComponent<Square>().State == BoardController.SquareState.Termite && BoardController.Instance.SquareSelected ==-1)
        {
            Debug.Log("Posicion: " + this.id);
            BoardController.Instance.SquareSelected = this.id;
            Debug.Log("Soy una Termite");
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
        else 
        {
            //Moving
            if (GetComponent<Square>().State == BoardController.SquareState.Empty)
            {
                Debug.Log("Moving a empty square");


                
                BoardController.Instance.MyBoard[this.id].Faction=BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction;
                BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction = null;
                BoardController.Instance.MyBoard[this.id].Faction.objectFaction.transform.position= BoardController.Instance.MyBoard[this.id].SquareObject.transform.position + new Vector3(0, 1, 0);

                //Current turn
                if (MatchController.Instance.Turn == (int)MatchController.TypeOfPlayers.Termite)
                {
                    BoardController.Instance.MyBoard[this.id].state = BoardController.SquareState.Termite;
                }
                else if (MatchController.Instance.Turn == (int)MatchController.TypeOfPlayers.Ant)
                {
                    BoardController.Instance.MyBoard[this.id].state = BoardController.SquareState.Ant;
                }

                BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].state = BoardController.SquareState.Empty;
                BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].squareObject.GetComponent<Square>().state = BoardController.SquareState.Empty;
                //Moved and deselected
                BoardController.Instance.SquareSelected = -1;
                BoardController.Instance.ResetStateSquareColor();
                BoardController.Instance.MarkFactionsTurn();




            }
            //Group
            else if (GetComponent<Square>().State == BoardController.SquareState.Ant)
            {
                
                BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction = null;
                BoardController.Instance.MyBoard[this.id].Faction.objectFaction.transform.position = BoardController.Instance.MyBoard[this.id].SquareObject.transform.position + new Vector3(0, 1, 0);

                //Current turn
                if (MatchController.Instance.Turn == (int)MatchController.TypeOfPlayers.Termite)
                {
                    BoardController.Instance.MyBoard[this.id].state = BoardController.SquareState.Termite;
                }
                else if (MatchController.Instance.Turn == (int)MatchController.TypeOfPlayers.Ant)
                {
                    BoardController.Instance.MyBoard[this.id].state = BoardController.SquareState.Ant;
                }

                BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].state = BoardController.SquareState.Empty;
                BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].squareObject.GetComponent<Square>().state = BoardController.SquareState.Empty;
                //Moved and deselected
                BoardController.Instance.SquareSelected = -1;
                BoardController.Instance.ResetStateSquareColor();
                BoardController.Instance.MarkFactionsTurn();


            }
            //Attack
            else if (GetComponent<Square>().State == BoardController.SquareState.Termite)
            {
                Debug.Log("Attacking");
            }
            else
            {
                Debug.Log("Otra casilla: " + GetComponent<Square>().State);
            }
               
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
