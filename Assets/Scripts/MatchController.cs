using UnityEngine;

public class MatchController : MonoBehaviour
{
    public enum TypeOfPlayers
    {
        Termite,
        Ant
    }
    private TypeOfPlayers typePlayerTurn;
    private int turn = 0;
    private int actionsPerTurn = 3;
    private int numPlayers = 2;

    public static MatchController Instance { get; private set; }
    public int Turn { get => turn; set => turn = value; }
    public int ActionsPerTurn { get => actionsPerTurn; set => actionsPerTurn = value; }
    public int NumPlayers { get => numPlayers; set => numPlayers = value; }

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
            typePlayerTurn = (TypeOfPlayers) turn;
        }



    }
}
