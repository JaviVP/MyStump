using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private TMP_Text tracesUI;
    [SerializeField]
    private GameObject panelMovement;



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
        tracesUI.text += t+ "\n";
    }
    public void ActivatePanelMovement(bool v)
    {
        panelMovement.SetActive(v);
        //Soldiers
        if (BoardController.Instance.SquareSelected != -1)
        {
            panelMovement.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantitySoldier.ToString();
            //Workers
            panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = BoardController.Instance.MyBoard[BoardController.Instance.SquareSelected].Faction.QuantityWorker.ToString();
        }
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
        if (v<=0)
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

        Debug.Log("UIQW: "+GetQuantityWorkersMovingUI());
        panelMovement.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        panelMovement.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);

        

        
        MatchController.TypeOfPlayers type;
        if ((int) MatchController.TypeOfPlayers.Ant==  MatchController.Instance.Turn)
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
