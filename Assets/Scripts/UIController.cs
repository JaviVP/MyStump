using TMPro;
using UnityEngine;
using static MatchController;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private TMP_Text tracesUI;
    [SerializeField]
    private GameObject panelMovement;
    [SerializeField]
    private TMP_Text actionsRemainingUI;
    [SerializeField]
    private TMP_Text turnPlayerInfoUI;




    public static UIController Instance { get; private set; }



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

    public void WriteTrace(string t)
    {
        tracesUI.text += t + "\n";
    }
    public void ActivatePanelMovement(bool v)
    {
        panelMovement.SetActive(v);
        
       
    }


    public void ResetAllTraces()
    {
        tracesUI.text = "";
    }
    public void EndTurnButton()
    {
        BoardController.Instance.EnableSquareCollider(false);
        BoardController.Instance.ResetStateSquareColor();
        MatchController.Instance.ChangeTurn();
    }

    public void MoreButtonSoldierMoving()
    {
        int v = int.Parse(panelMovement.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text);
        v = v + 1;
        if (v >= BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantitySoldier) //ERROR ONLY CURRENT SQUARE SOLDIERS
        {
            v = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantitySoldier;
            panelMovement.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            panelMovement.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        }
        panelMovement.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        panelMovement.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = v.ToString();
    }

    public void LessButtonSoldierMoving()
    {
        int v = int.Parse(panelMovement.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text);
        v = v - 1;
        if (v <= 0)
        {
            panelMovement.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            v = 0;
        }
        else
        {
            panelMovement.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
        }
        panelMovement.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        panelMovement.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = v.ToString();
    }

    public void MoreButtonWorkerMoving()
    {
        int v = int.Parse(panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text);
        v = v + 1;
        if (v >= BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantityWorker) //ERROR ONLY CURRENT SQUARE WORKERS
        {
            panelMovement.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            v = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantityWorker;
        }
        else
        {
            panelMovement.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        }
        panelMovement.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = v.ToString();

        Debug.Log("UIQW: " + GetQuantityWorkersMovingUI());
    }
    public void LessButtonWorkerMoving()
    {
        int v = int.Parse(panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text);
        v = v - 1;
        if (v <= 0)
        {
            panelMovement.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
            v = 0;
        }
        else
        {
            panelMovement.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
        }
        panelMovement.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = v.ToString();

        Debug.Log("UIQW: " + GetQuantityWorkersMovingUI());

    }
    public int GetQuantitySoldiersMovingUI()
    {
        return int.Parse(panelMovement.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text);
    }
    public int GetQuantityWorkersMovingUI()
    {
        return int.Parse(panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text);
    }
    public void MovingActionButton()
    {

        Debug.Log("SelectedUI:" + BoardController.Instance.SquareSelected);
        MatchController.TypeOfPlayers type;
        if ((int)MatchController.TypeOfPlayers.Ant == MatchController.Instance.Turn)
        {
            type = MatchController.TypeOfPlayers.Ant;
        }
        else
        {
            type = MatchController.TypeOfPlayers.Termite;
        }


        BoardController.Instance.ActionMovingToEmptySquare(BoardController.Instance.LastClickedSquare, type);
        ActivatePanelMovement(false);
    }

    public void UpdateActionsText()
    {
        if (actionsRemainingUI != null)
        {
            //Debug.Log("Actualizando UI con acciones: " + MatchController.Instance.ActionsRemaining);
            actionsRemainingUI.text = MatchController.Instance.ActionsRemaining.ToString();
        }
        else
        {
           // Debug.LogWarning("actionsRemainingUI no est� asignado en el Inspector.");
        }
    }

    public void UpdateTurnUI()
    {
        if (MatchController.Instance.TypePlayerTurn == TypeOfPlayers.Termite)
        {
            turnPlayerInfoUI.text = "Termites";
        }
        else
        {
            turnPlayerInfoUI.text = "Ants";
        }

        //Debug.Log("Turn UI updated: " + turnPlayerInfoUI.text);
    }


    // Update is called once per frame
    void Update()
    {

    }
}
