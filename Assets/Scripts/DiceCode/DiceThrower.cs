using UnityEngine;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Dice
{
    public string name;
    public List<DiceFace> faces;

    public DiceFace Roll()
    {
        return faces[UnityEngine.Random.Range(0, faces.Count)];
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

    // External modifier system ---> to change
    //public Func<int, int> ModifyResult = (x) => x;  // Default: No modification

    public static DiceThrower Instance { get; private set; }


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


    DiceFace rawAttackerRoll;
    DiceFace rawDefenderRoll;

    Dice attackerDice;
    Dice defenderDice;

    int attackerDiceNum;
    int defenderDiceNum;
    public (DiceFace AttackerResult, DiceFace DefenderResult) RollDice(int attackerWorkers, int attackerSoldiers, int defenderWorkers, int defenderSoldiers)
    {
        attackerDice = GetDiceType(attackerWorkers + attackerSoldiers);
        attackerDiceNum = Mathf.FloorToInt(Mathf.Log(attackerWorkers + attackerSoldiers, 2));
        defenderDice = GetDiceType(defenderWorkers + defenderSoldiers);
        defenderDiceNum = Mathf.FloorToInt(Mathf.Log(defenderWorkers + defenderSoldiers, 2));

        rawAttackerRoll = attackerDice.Roll();
        rawDefenderRoll = defenderDice.Roll();

      

        DiceFace finalAttackerRoll = ApplyBonuses(rawAttackerRoll, attackerSoldiers, isAttacker: true);
        DiceFace finalDefenderRoll = ApplyBonuses(rawDefenderRoll, defenderSoldiers, isAttacker: false);

        // Apply external modifications (e.g., event cards) ---> previous to change
        //finalAttackerRoll.swords = ModifyResult(finalAttackerRoll.swords);
        //finalAttackerRoll.shields = ModifyResult(finalAttackerRoll.shields);
        //finalDefenderRoll.swords = ModifyResult(finalDefenderRoll.swords);
        //finalDefenderRoll.shields = ModifyResult(finalDefenderRoll.shields);

        // Console logs for debugging
        Debug.Log($"Attacker rolled: {rawAttackerRoll.swords} swords, {rawAttackerRoll.shields} shields. Final: {finalAttackerRoll.swords} swords, {finalAttackerRoll.shields} shields");
        Debug.Log($"Defender rolled: {rawDefenderRoll.swords} swords, {rawDefenderRoll.shields} shields. Final: {finalDefenderRoll.swords} swords, {finalDefenderRoll.shields} shields");

        return (finalAttackerRoll, finalDefenderRoll);
    }

   
    /// TODO ---> implement external bonuses system
    
    /*
    public (DiceFace attacker, DiceFace defender) ModifyResults(DiceFace attackerFace, DiceFace defenderFace, int attSwordMod, int attShieldMod, int defSwordMod, int defShieldMod)
    {
        DiceFace modifiedAttacker = new DiceFace
        {
            swords = attackerFace.swords + attSwordMod,
            shields = attackerFace.shields + attShieldMod
        };

        DiceFace modifiedDefender = new DiceFace
        {
            swords = defenderFace.swords + defSwordMod,
            shields = defenderFace.shields + defShieldMod
        };

        return (modifiedAttacker, modifiedDefender);
    }
    */

    private Dice GetDiceType(int troopCount)
    {
        return availableDice[Mathf.FloorToInt(Mathf.Log(troopCount, 2))];
    }

    private DiceFace ApplyBonuses(DiceFace roll, int soldiers, bool isAttacker)
    {
        int swords = roll.swords;
        int shields = roll.shields;


        if (isAttacker)
        {
            if (swords > 0 && shields > 0)
            {
                swords += soldiers;
                shields = 0;
            }
            else if (swords > 0)
            {
                swords += soldiers;
            }
            else
            {
                shields += soldiers;
            }
        }
        else
        {
            if (swords > 0 && shields > 0)
            {
                shields += soldiers;
                swords = 0;
            }
            else if (swords > 0)
            {
                swords += soldiers;
            }
            else
            {
                shields += soldiers;
            }
        }


        return new DiceFace
        {
            swords = swords,
            shields = shields
        };
    }


    public int RawAttackerSwords()
    {

        return rawAttackerRoll.swords;

    }
    public int RawAttackerShields()
    {

        return rawAttackerRoll.shields;

    }
    public int RawDefenderSwords()
    {

        return rawDefenderRoll.swords;

    }
    public int RawDefenderShields()
    {

        return rawDefenderRoll.shields;

    }

    public int AttackerDiceType()
    {

        return attackerDiceNum;
        

    }

    public int DefenderDiceType()
    {

        return defenderDiceNum;

    }
}
