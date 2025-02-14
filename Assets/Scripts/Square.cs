using UnityEngine;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine.UIElements;
using TMPro;
using Unity.VisualScripting;

public class Square : MonoBehaviour
{
    
    private int id;
    private GameObject squareObject;
    private List<Square> squareAdjacents;
    private MeshRenderer meshRenderer;
    private int clics;
    private FactionAbstract faction;
    private BattleResolver battleResolver;
    [SerializeField]
    private BoardController.SquareState state;
    /*[SerializeField] 
    private TMP_Text unitCountText;*/
    public GameObject SquareObject { get => squareObject; set => squareObject = value; }
    public BoardController.SquareState State { get => state; set => state = value; }
    public FactionAbstract Faction { get => faction; set => faction = value; }
    public int Id { get => id; set => id = value; }
    public int Clics { get => clics; set => clics = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        clics = 0;
        //Cambiar de color el objeto
        meshRenderer = GetComponent<MeshRenderer>();
        battleResolver = FindFirstObjectByType<BattleResolver>();
        //Volver al color anterior


        //Poner de un color el clicado y otro color los adjacentes.
        //HighlightSquare();
        //Crear un método en la clase board que pongar todos los square a white(estado normal).
        ChangeColorSquare(this.gameObject);
        //UpdateUnitCountText();



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


    public void MovingFaction()
    {
        BoardController.Instance.MyBoard[this.id].Faction = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction;
        BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.objectFaction.transform.SetParent(BoardController.Instance.MyBoard[this.id].SquareObject.transform);

        BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction = null;
        BoardController.Instance.MyBoard[this.id].Faction.objectFaction.transform.position = BoardController.Instance.MyBoard[this.id].SquareObject.transform.position + new Vector3(0, 1, 0);

        Debug.Log("Selected:" + BoardController.Instance.SquareSelected);
        

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
        BoardController.Instance.SquareSelected = -4;
        BoardController.Instance.ResetStateSquareColor();
        BoardController.Instance.MarkFactionsTurn();
    }
    /// <summary>
    /// List of Actions
    /// </summary>
    private void HighlightSquare()
    {

        GetComponent<MeshCollider>().enabled = false;

     
        if (GetComponent<Square>().State == BoardController.SquareState.Ant && BoardController.Instance.SquareSelected <=0)
        {
                Debug.Log("Posicion: "+ this.id);
                BoardController.Instance.SquareSelected = this.id;
            //Debug.Log("Soy una hormiga ID:" + BoardController.Instance.SquareSelected);
            BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].SquareObject.GetComponent<MeshCollider>().enabled = false;



            BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.objectFaction.transform.GetChild(1).gameObject.SetActive(true);


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
        else if (GetComponent<Square>().State == BoardController.SquareState.Termite && BoardController.Instance.SquareSelected <=0)
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

              //  Debug.Log("Moving a empty square SELECTED: "+ BoardController.Instance.SquareSelected);

                int sum = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantitySoldier + BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantityWorker;
                Debug.Log("la suma es: " + sum);
                if (sum>1)
                {
                   
                    clics++;
                    if (clics==1)
                    {
                       
                        BoardController.Instance.LastClickedSquare = this.id;
                        UIController.Instance.ActivatePanelMovement(true);
                        BoardController.Instance.ResetStateSquareColor();
                        BoardController.Instance.EnableSquareCollider(false);
                        ChangeColorSquare(this.gameObject);
                        GetComponent<MeshCollider>().enabled = true;
                        clics++;
                    }
                    else
                    {
                       
                        MovingFaction();
                        UIController.Instance.ActivatePanelMovement(false);
                        clics =0;
                        MatchController.Instance.UseAction();
                        UIController.Instance.UpdateActionsText();
                    }

                }
                else
                {
                    MovingFaction();
                    UIController.Instance.ActivatePanelMovement(false);
                }





            }
            //Group 
            else if ((int) GetComponent<Square>().State == (int)  MatchController.Instance.Turn )
            {
                Debug.Log("Agrupooo");


                //Error: check max soldiers =2, max workes= 4  (max sum=4)

                int qw1 = BoardController.Instance.MyBoard[this.id].Faction.QuantityWorker;
                int qw2 = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantityWorker;
                int qs1= BoardController.Instance.MyBoard[this.id].Faction.QuantitySoldier;
                int qs2 = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantitySoldier;

                int sum = qw1 + qw2 + qs1 + qs2;


                if (sum <= 4)
                {
                    BoardController.Instance.MyBoard[this.id].Faction.QuantityWorker += BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantityWorker;
                    BoardController.Instance.MyBoard[this.id].Faction.QuantitySoldier += BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantitySoldier;
                    Destroy(BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.objectFaction);
                    BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction = null;

                    BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].state = BoardController.SquareState.Empty;
                    BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].squareObject.GetComponent<Square>().state = BoardController.SquareState.Empty;
                    //Moved and deselected
                    
                }
                BoardController.Instance.SquareSelected = -1;
                BoardController.Instance.ResetStateSquareColor();
                BoardController.Instance.MarkFactionsTurn();

                clics = 0;
            }
            //Attack
            else if ((int)GetComponent<Square>().State != (int)MatchController.Instance.Turn)
            {


                int currentAttackerWorkers = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantityWorker;
                int currentAttackerSoldiers = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantitySoldier;
                int currentDefenderWorkers = BoardController.Instance.MyBoard[this.id].Faction.QuantityWorker;
                int currentDefenderSoldiers = BoardController.Instance.MyBoard[this.id].Faction.QuantitySoldier;
               // Debug.Log("")


                Debug.Log("Ataco");
                (int remainingAtackerWorkers, int remainingAttackerSoldiers, int remainingDefenderWorkers, int remainingDefenderSoldiers) = battleResolver.ResolveBattle(currentAttackerWorkers, currentAttackerSoldiers, currentDefenderWorkers, currentDefenderSoldiers);
                remainingAtackerWorkers = BoardController.Instance.MyBoard[this.id].Faction.QuantityWorker;

                BoardController.Instance.MyBoard[this.id].Faction.QuantityWorker = remainingAtackerWorkers;
                BoardController.Instance.MyBoard[this.id].Faction.QuantitySoldier = remainingAttackerSoldiers;
                BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantityWorker = remainingDefenderWorkers;
                BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantitySoldier = remainingDefenderSoldiers;


                clics = 0;
            }
            else
            {
                Debug.Log("Otra casilla: " + GetComponent<Square>().State);
            }
               
        }
        
    }

   /*public void UpdateUnitCountText()
    {
        if (Faction != null)
        {
            int totalUnits = Faction.QuantitySoldier + Faction.QuantityWorker;
            unitCountText.text = totalUnits.ToString();
            unitCountText.gameObject.SetActive(totalUnits > 0); // Mostrar solo si hay unidades
        }
        else
        {
            unitCountText.text = "0"; // Si no hay facción, mostrar 0
            unitCountText.gameObject.SetActive(false); // Ocultar el texto
        }
    }*/


    private void OnMouseDown()
    {

        HighlightSquare();

    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
