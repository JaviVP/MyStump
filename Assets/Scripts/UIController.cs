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
        if (v >= 3) //ERROR ONLY CURRENT SQUARE SOLDIERS
        {
            v = 3;
        }
        panelMovement.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        panelMovement.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = v.ToString();
    }
    public void LessButtonSoldierMoving()
    {
        int v = int.Parse(panelMovement.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text);
        v = v - 1;
        if (v <= 0)
        {
            v = 0;
        }
        panelMovement.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        panelMovement.transform.GetChild(1).GetChild(2).GetComponent<TMP_Text>().text = v.ToString();
    }
    public void MoreButtonWorkerMoving()
    {
        int v = int.Parse(panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text);
        v = v + 1;
        if (v >=3) //ERROR ONLY CURRENT SQUARE WORKERS
        {
            v = 3;
        }
        panelMovement.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = v.ToString();
    }
    public void LessButtonWorkerMoving()
    {
        int v = int.Parse(panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text);
        v = v - 1;
        if (v<=0)
        {
            v = 0;
        }
        panelMovement.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
        panelMovement.transform.GetChild(2).GetChild(2).GetComponent<TMP_Text>().text = v.ToString();
    }

    public void MovingActionButton()
    {
        panelMovement.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        panelMovement.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
        ActivatePanelMovement(false);
        MatchController.TypeOfPlayers type;
        if ((int) MatchController.TypeOfPlayers.Ant==  MatchController.Instance.Turn)
        {
            type = MatchController.TypeOfPlayers.Ant;
        }
        else
        {
            type = MatchController.TypeOfPlayers.Termite;
        }
        BoardController.Instance.CreateFactionInEmptySquare(BoardController.Instance.LastClickedSquare, type, 2, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
