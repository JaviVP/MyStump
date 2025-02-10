using UnityEngine;

public class BattleResolver : MonoBehaviour
{
    public (int attackerWorkersLeft, int attackerSoldiersLeft, int defenderWorkersLeft, int defenderSoldiersLeft) ResolveBattle(int attackerWorkers, int attackerSoldiers, int defenderWorkers, int defenderSoldiers)
    {
        (DiceFace attackerResult, DiceFace defenderResult) = FindFirstObjectByType<DiceThrower>().RollDice(attackerWorkers, attackerSoldiers, defenderWorkers, defenderSoldiers);
        int attackerLosses = Mathf.Max(defenderResult.swords - attackerResult.shields, 0);
        int defenderLosses = Mathf.Max(attackerResult.swords - defenderResult.shields, 0);

        (int finalAttackerWorkers, int finalAttackerSoldiers) = CalculateCasualties(attackerWorkers, attackerSoldiers, attackerLosses);
        (int finalDefenderWorkers, int finalDefenderSoldiers) = CalculateCasualties(defenderWorkers, defenderSoldiers, defenderLosses);

        Debug.Log($"Battle outcome: Attacker lost {attackerLosses} troops (Workers: {attackerWorkers - finalAttackerWorkers}, Soldiers: {attackerSoldiers - finalAttackerSoldiers}), " +
                  $"Defender lost {defenderLosses} troops (Workers: {defenderWorkers - finalDefenderWorkers}, Soldiers: {defenderSoldiers - finalDefenderSoldiers}).");

        return (finalAttackerWorkers, finalAttackerSoldiers, finalDefenderWorkers, finalDefenderSoldiers);
    }

    private (int remainingWorkers, int remainingSoldiers) CalculateCasualties(int workers, int soldiers, int losses)
    {
        int workerLosses = Mathf.Min(losses, workers);
        int remainingLosses = losses - workerLosses;
        int soldierLosses = Mathf.Min(remainingLosses, soldiers);

        return (workers - workerLosses, soldiers - soldierLosses);
    }




    
}
