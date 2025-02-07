using UnityEngine;

public class BattleResolver : MonoBehaviour
{
    public void ResolveBattle(DiceFace attackerRoll, DiceFace defenderRoll, int attackerWorkers, int attackerSoldiers, int defenderWorkers, int defenderSoldiers)
    {
        int attackerLosses = Mathf.Max(defenderRoll.swords - attackerRoll.shields, 0);
        int defenderLosses = Mathf.Max(attackerRoll.swords - defenderRoll.shields, 0);

        (int finalAttackerWorkers, int finalAttackerSoldiers) = CalculateCasualties(attackerWorkers, attackerSoldiers, attackerLosses);
        (int finalDefenderWorkers, int finalDefenderSoldiers) = CalculateCasualties(defenderWorkers, defenderSoldiers, defenderLosses);

        Debug.Log($"Battle outcome: Attacker lost {attackerLosses} troops (Workers: {attackerWorkers - finalAttackerWorkers}, Soldiers: {attackerSoldiers - finalAttackerSoldiers}), " +
                  $"Defender lost {defenderLosses} troops (Workers: {defenderWorkers - finalDefenderWorkers}, Soldiers: {defenderSoldiers - finalDefenderSoldiers}).");
    }

    private (int, int) CalculateCasualties(int workers, int soldiers, int losses)
    {
        int workerLosses = Mathf.Min(losses, workers);
        int remainingLosses = losses - workerLosses;
        int soldierLosses = Mathf.Min(remainingLosses, soldiers);

        return (workers - workerLosses, soldiers - soldierLosses);
    }
}
