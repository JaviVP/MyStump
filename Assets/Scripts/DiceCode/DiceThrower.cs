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

    public (DiceFace AttackerResult, DiceFace DefenderResult) RollDice(int attackerWorkers, int attackerSoldiers, int defenderWorkers, int defenderSoldiers)
    {
        Dice attackerDice = GetDiceType(attackerWorkers + attackerSoldiers);
        Dice defenderDice = GetDiceType(defenderWorkers + defenderSoldiers);

        DiceFace rawAttackerRoll = attackerDice.Roll();
        DiceFace rawDefenderRoll = defenderDice.Roll();

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


        /*

        // If both swords and shields exist, keep the relevant one
        if (swords > 0 && shields > 0)
        {
            if (isAttacker)
                shields = 0; // Attackers only use swords
            else
                swords = 0; // Defenders only use shields
        }

        
        // Apply soldier bonus
        swords += soldiers;
        shields += soldiers;

        // Apply special rule if initial result was 0
        if (roll.shields == 0 && roll.swords == 0)
        {
            if (isAttacker)
            {
                swords += soldiers;
            }
            else
            {
                shields += soldiers;
            }
             // If attacker rolled 0, boost swords
        }

        if (isAttacker && swords > 0)
        {
            swords += soldiers;
        }
        if (!isAttacker && shields > 0)
        {
            shields += soldiers;
        }
        */

        return new DiceFace
        {
            swords = swords,
            shields = shields
        };
    }
}
