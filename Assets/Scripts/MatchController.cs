using UnityEngine;

public class MatchController : MonoBehaviour
{

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
    public void ChangeTurn()
    {
        turn++;
        if (turn>=numPlayers)
        {
            turn = 0;
        }



    }
}