using System.Collections;
using UnityEngine;

public class LogicController : MonoBehaviour
{
    private string resultDice1;
    private string resultDice2;

    private string[] dice1Split;
    private string[] dice2Split;

    private int dice1Qty;
    private int dice2Qty;

    private string dice1Type;
    private string dice2Type;

    private int antDieS;
    private int antDieW;
    private int terDieS;
    private int terDieW;

    private int dice1Result;
    private int dice2Result;
   
    public static LogicController Instance { get; private set; }
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
    private void Start()
    {
        StartCombat(1, 3, 2, 2);
    }
    public void StartCombat(int antS, int antW, int terS, int terW)
    {
        StartCoroutine(CombatSequence(antS,antW,terS,terW));

    }
    IEnumerator CombatSequence(int antS, int antW, int terS, int terW)
    {
        // Lanzar dados
        DicesController.Instance.ThrowDices(1,1,0,2);
        yield return new WaitForSeconds(3);
        // Datos devueltos de los dados
        resultDice1 = DicesController.Instance.GetDice1();
        resultDice2 = DicesController.Instance.GetDice2();
        
        dice1Split = resultDice1.Split(" ");
        dice2Split = resultDice2.Split(" ");
        Debug.Log(dice1Split[0] + "-" + dice1Split[1]);
        Debug.Log(dice2Split[0] + "-" + dice2Split[1]);
        dice1Qty = int.Parse(dice1Split[0]);
        dice2Qty = int.Parse(dice2Split[0]);

        dice1Type = dice1Split[1];
        dice2Type = dice2Split[1];


        /*
        if(dice1Type == "sword" && dice2Type == "sword") //Este caso dice1Result y dice2Result son las tropas a restar a cada facción
        {
           dice1Result = dice1Qty + antS;
           dice2Result = dice2Qty + terS; 

        }
        else if (dice1Type == "shield" && dice2Type == "shield")
        {
            dice1Result = dice1Qty + antS;
            dice2Result = dice2Qty + terS;
            

        }
        else if (dice1Type == "skull" && dice2Type == "skull")
        {


        }
        else if (dice1Type == "sword" && dice2Type == "shield")
        {
            dice1Result = dice1Qty + antS;
            dice2Result = dice2Qty + terS;

        }
        else if (dice1Type == "sword" && dice2Type == "skull")
        {



        }
        else if (dice1Type == "shield" && dice2Type == "sword")
        {


        }
        else if (dice1Type == "shield" && dice2Type == "skull")
        {


        }
        else if (dice1Type == "skull" && dice2Type == "sword")
        {


        }
        else if (dice1Type == "skull" && dice2Type == "shield")
        {


        }
        */

    }
   
}
