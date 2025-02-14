using UnityEngine;

public class MatchController : MonoBehaviour
{
    [SerializeField] private GameObject rawCommbatScene;
    [SerializeField] private GameObject combatScene;
    private int maxSoldiersInGame = 3;
    private int maxSoldiersInSquare = 3;
    private int maxTroopsInSquare = 6;


    public enum TypeOfPlayers
    {
        Termite,
        Ant
    }
    public enum TypeofTroops
    {
        AntSoldier,
        AntWorker,
        TermiteSoldier,
        TermiteWorker
    }

    private TypeOfPlayers typePlayerTurn;
    private TypeofTroops typeTroops;
    private int turn = 0;
    private int actionsPerTurn = 100;
    private int actionsRemaining;
    private int numPlayers = 2;

    public static MatchController Instance { get; private set; }
    public int Turn { get => turn; set => turn = value; }
    public int ActionsPerTurn { get => actionsPerTurn; set => actionsPerTurn = value; }
    public int NumPlayers { get => numPlayers; set => numPlayers = value; }
    public int MaxSoldiersInGame { get => maxSoldiersInGame; set => maxSoldiersInGame = value; }
    public int ActionsRemaining { get => actionsRemaining; }
    public TypeOfPlayers TypePlayerTurn { get => typePlayerTurn;}


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
        actionsRemaining = actionsPerTurn;
        UIController.Instance.UpdateActionsText();
        typePlayerTurn = (TypeOfPlayers)turn;
        UIController.Instance.UpdateTurnUI();
        /*typePlayer = TypeOfPlayers.Termite;
        Debug.Log(typePlayer.ToString()+ " -- " + (int) typePlayer);
        typePlayer = (TypeOfPlayers) 1;
        Debug.Log(typePlayer.ToString() + " -- " + (int)typePlayer);*/
    }

    public void ChangeTurn()
    {
        CamerasController.Instance.SwitchCamera();
        turn++;
        if (turn >= numPlayers)
        {
            turn = 0;

        }
        typePlayerTurn = (TypeOfPlayers)turn;

        // Actualizar UI
        UIController.Instance.UpdateTurnUI();

        actionsRemaining = actionsPerTurn;
       
        BoardController.Instance.UnSelected();
        BoardController.Instance.MarkFactionsTurn();
        UIController.Instance.ActivatePanelMovement(false);
        UIController.Instance.UpdateActionsText();

        Debug.Log("Turn change. Now play: " + typePlayerTurn + ". Actions remaining: " + actionsRemaining);

    }

    public bool CanPerformAction()
    {
        return actionsRemaining > 0;
    }

    public void UseAction()
    {
        if (actionsRemaining > 0)
        {
            actionsRemaining--;
            UIController.Instance.UpdateActionsText();
            Debug.Log("Action completed. Remaining actions: " + actionsRemaining);
        }
        else
        {
            
            Debug.Log("No actions available");
        }

    }


    public void ActivateCombat()
    {
        
        rawCommbatScene.SetActive(true);
        combatScene.SetActive(true);
        

    }
}
