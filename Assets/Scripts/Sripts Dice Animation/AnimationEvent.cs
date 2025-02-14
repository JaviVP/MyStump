using UnityEngine;
using System.Collections;
public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private DiceMeshController dice;
    [SerializeField] private GameObject combatScene;
    [SerializeField] private GameObject rawCombatScene;

   

 
    public void OnAnimationEnd()
    {
        
        Debug.Log("Acaba animacion");
        
        // Debug.Log(ResetCombatPositions.Instance.StampideTime());
        StartCoroutine(DelayActions());
    }
    private IEnumerator DelayActions()
    {
        //1.5s delay
        yield return new WaitForSeconds(1.5f);
        combatScene.SetActive(false);
        rawCombatScene.SetActive(false);
        ResetCombatPositions.Instance.ResetTransform();

    }
}
