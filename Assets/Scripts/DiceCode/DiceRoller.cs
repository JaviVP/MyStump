/*
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class DiceSimulator : MonoBehaviour
{
    public TMP_Dropdown attackerDropdown, defenderDropdown;
    public TMP_Text attackerResultText, defenderResultText, battleOutcomeText;
    public List<DiceThrower> availableDice;

    private DiceThrower attackerDice, defenderDice;
    private int attackerTroops, defenderTroops;

    public void SelectDice()
    {
        List<string> options = new List<string> { "1", "2", "4" }; // Can expand later
        attackerDropdown.AddOptions(options);
        defenderDropdown.AddOptions(options);

        attackerTroops = int.Parse(attackerDropdown.options[attackerDropdown.value].text);
        defenderTroops = int.Parse(defenderDropdown.options[defenderDropdown.value].text);

        attackerDice = GetDiceForTroops(attackerTroops);
        defenderDice = GetDiceForTroops(defenderTroops);

        Debug.Log($"Attacker selected {attackerTroops} troops, using dice: {attackerDice.name}");
        Debug.Log($"Defender selected {defenderTroops} troops, using dice: {defenderDice.name}");
    }

    private DiceThrower GetDiceForTroops(int troopCount)
    {
        if (troopCount < 2)
        {
            Debug.Log($"Troop count {troopCount}: Using first die.");
            return availableDice[0];
        }
        else if (troopCount < 4)
        {
            Debug.Log($"Troop count {troopCount}: Using second die.");
            return availableDice[1];
        }
        else
        {
            Debug.Log($"Troop count {troopCount}: Using third die.");
            return availableDice[2];
        }
    }

    public void RollDice()
    {
        if (attackerDice == null || defenderDice == null)
        {
            Debug.LogError("Invalid dice selection! Cannot roll.");
            battleOutcomeText.text = "Invalid dice selection!";
            return;
        }

        DiceFace attackerRoll = attackerDice.faces[Random.Range(0, attackerDice.faces.Count)];
        DiceFace defenderRoll = defenderDice.faces[Random.Range(0, defenderDice.faces.Count)];

        Debug.Log($"Attacker rolled: {attackerRoll.swords} swords, {attackerRoll.shields} shields.");
        Debug.Log($"Defender rolled: {defenderRoll.swords} swords, {defenderRoll.shields} shields.");

        attackerResultText.text = $"Attacker rolled: {attackerRoll.swords} swords, {attackerRoll.shields} shields.";
        defenderResultText.text = $"Defender rolled: {defenderRoll.swords} swords, {defenderRoll.shields} shields.";

        ResolveBattle(attackerRoll, defenderRoll);
    }

    void ResolveBattle(DiceFace attackerRoll, DiceFace defenderRoll)
    {
        int attackerSwords = attackerRoll.swords;
        int defenderShields = defenderRoll.shields;
        int defenderSwords = defenderRoll.swords;
        int attackerShields = attackerRoll.shields;

       
        

        int attackerLosses = Mathf.Max(defenderSwords - attackerShields, 0);
        int defenderLosses = Mathf.Max(attackerSwords - defenderShields, 0);

        attackerLosses = Mathf.Min(attackerLosses, attackerTroops);
        defenderLosses = Mathf.Min(defenderLosses, defenderTroops);

        Debug.Log($"Battle outcome: Attacker lost {attackerLosses} troops, Defender lost {defenderLosses} troops.");

        battleOutcomeText.text = $"Attacker lost {attackerLosses} troop(s). Defender lost {defenderLosses} troop(s).";
    }
}

[System.Serializable]
public class DiceThrower
{
    public string name;
    public List<DiceFace> faces;
}

[System.Serializable]
public class DiceFace
{
    public int swords;
    public int shields;
}

*/