using UnityEngine;
using System.Collections.Generic;


[System.Serializable]
public class Dice
{
    public string name;
    public List<DiceFace> faces;

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

    public (DiceFace AttackerResult, DiceFace DefenderResult) RollDice(int attackerWorkers, int attackerSoldiers, int defenderWorkers, int defenderSoldiers)
    {
        attackerDice = GetDiceType(attackerWorkers + attackerSoldiers);
        defenderDice = GetDiceType(defenderWorkers + defenderSoldiers);

        attackerRoll = attackerDice.Roll();
        defenderRoll = defenderDice.Roll();

        //Consola
        Debug.Log($"Attacker rolled: {attackerRoll.swords} swords, {attackerRoll.shields} shields.");
        Debug.Log($"Defender rolled: {defenderRoll.swords} swords, {defenderRoll.shields} shields.");

        //FindFirstObjectByType<BattleResolver>().ResolveBattle(attackerRoll, defenderRoll, attackerWorkers, attackerSoldiers, defenderWorkers, defenderSoldiers);
        return (attackerRoll,defenderRoll);
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

    private Dice GetDiceType(int troopCount)
    {
        return availableDice[ Mathf.FloorToInt(Mathf.Log(troopCount, 2)) + 1];
    }



}


