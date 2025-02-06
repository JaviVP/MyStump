using UnityEngine;

public class DicesController : MonoBehaviour
{

    private int dice1;// Numero del dado del jugador 1 : 1 = dado1, 2 = dado2, 3 = dado3
    private int dice2;// Numero del dado del jugador 2 : 1 = dado1, 2 = dado2, 3 = dado3
    public static DicesController Instance { get; private set; }
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


    public void ThrowDices(int antS, int antW, int terS, int terW)
    {


        
    }

    public string GetDice1() { 
    

        return "2 shield";
    }
    public string GetDice2() { 
    

        return "3 swords";
    }
    





}
