using UnityEngine;
using TMPro;

public class BattleTesterUI : MonoBehaviour
{
    public TMP_InputField attackerWorkersInput;
    public TMP_InputField attackerSoldiersInput;
    public TMP_InputField defenderWorkersInput;
    public TMP_InputField defenderSoldiersInput;
    public TMP_Text resultText;
    [SerializeField] private GameObject ActivateCombat;
    [SerializeField] private GameObject Canvas;

    private BattleResolver battleResolver;

    private void Start()
    {
        battleResolver = FindFirstObjectByType<BattleResolver>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SimulateBattle();
            ActivateCombat.SetActive(true);
            Canvas.SetActive(false);
        }
    }

    public void SimulateBattle()
    {
        int attackerWorkers = int.TryParse(attackerWorkersInput.text, out int aW) ? aW : 0;
        int attackerSoldiers = int.TryParse(attackerSoldiersInput.text, out int aS) ? aS : 0;
        int defenderWorkers = int.TryParse(defenderWorkersInput.text, out int dW) ? dW : 0;
        int defenderSoldiers = int.TryParse(defenderSoldiersInput.text, out int dS) ? dS : 0;

        var result = battleResolver.ResolveBattle(attackerWorkers, attackerSoldiers, defenderWorkers, defenderSoldiers);

        resultText.text = $"Final Troops:\n" +
                          $"Attacker - Workers: {result.attackerWorkersLeft}, Soldiers: {result.attackerSoldiersLeft}\n" +
                          $"Defender - Workers: {result.defenderWorkersLeft}, Soldiers: {result.defenderSoldiersLeft}";
    }
}
