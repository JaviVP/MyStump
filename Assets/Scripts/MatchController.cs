using UnityEngine;

public class MatchController : MonoBehaviour
{

    private int maxSoldiersInGame = 3;
    private int maxSoldiersInSquare = 3;
    private int maxFactionsInSquare = 6;


    public enum TypeOfPlayers
    {
        Termite,
        Ant
    }
    public enum TypeofTroppes
    {
        AntSoldier,
        AntWorker,
        TermiteSoldier,
        TermiteWorker
    }
    private TypeOfPlayers typePlayerTurn;
    private TypeofTroppes typeTroppes;
    private int turn = 0;
    private int actionsPerTurn = 3;
    private int numPlayers = 2;

    public static MatchController Instance { get; private set; }
    public int Turn { get => turn; set => turn = value; }
    public int ActionsPerTurn { get => actionsPerTurn; set => actionsPerTurn = value; }
    public int NumPlayers { get => numPlayers; set => numPlayers = value; }
    public int MaxSoldiersInGame { get => maxSoldiersInGame; set => maxSoldiersInGame = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
    void Start()
    {
        turn = 1;
        /*typePlayer = TypeOfPlayers.Termite;
        Debug.Log(typePlayer.ToString()+ " -- " + (int) typePlayer);
        typePlayer = (TypeOfPlayers) 1;
        Debug.Log(typePlayer.ToString() + " -- " + (int)typePlayer);*/
    }
    public void ChangeTurn()
    {
        turn++;
        if (turn>=numPlayers)
        {
            turn = 0;
           
        }
        typePlayerTurn = (TypeOfPlayers)turn;
        BoardController.Instance.MarkFactionsTurn();


    }
}
