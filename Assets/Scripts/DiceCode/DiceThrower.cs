using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Dice
{
    [SerializeField]
    private string name;
    [SerializeField]
    private List<DiceFace> faces;

    public DiceFace Roll()
    {
        return faces[Random.Range(0, faces.Count)];
    }
}

[System.Serializable]
public class DiceFace
{
    public int swords;
    public int shields;
}




public class DiceThrower : MonoBehaviour
{
    public List<Dice> availableDice;

    private Dice attackerDice;
    private Dice defenderDice;
    private DiceFace attackerRoll;
    private DiceFace defenderRoll;

    public void RollDice(int attackerWorkers, int attackerSoldiers, int defenderWorkers, int defenderSoldiers)
    {
        attackerDice = GetDicePosition(attackerWorkers + attackerSoldiers);
        defenderDice = GetDicePosition(defenderWorkers + defenderSoldiers);

        attackerRoll = attackerDice.Roll();
        defenderRoll = defenderDice.Roll();

        Debug.Log($"Attacker rolled: {attackerRoll.swords} swords, {attackerRoll.shields} shields.");
        Debug.Log($"Defender rolled: {defenderRoll.swords} swords, {defenderRoll.shields} shields.");

        FindFirstObjectByType<BattleResolver>().ResolveBattle(attackerRoll, defenderRoll, attackerWorkers, attackerSoldiers, defenderWorkers, defenderSoldiers);
    }


    /*
    private Dice GetDiceForTroops(int troopCount)
    {
        if (troopCount < 2) return availableDice[0];
        else if (troopCount < 4) return availableDice[1];
        else return availableDice[2];
    }
    */

    // Mas escalable que lo de arriba ↑↑↑, pero usa mas recursos. Si se necesita se quita lo de abajo

    private Dice GetDicePosition(int troopCount)
    {
        return availableDice[ Mathf.FloorToInt(Mathf.Log(troopCount, 2)) + 1];
    }



}


