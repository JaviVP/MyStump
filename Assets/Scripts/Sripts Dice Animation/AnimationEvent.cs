using UnityEngine;
using System.Collections;
public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private DiceMeshController dice;
    [SerializeField] private GameObject CombatScene;
    [SerializeField] private GameObject RawCombatScene;

    public void OnAnimationEnd()
    {
        
        Debug.Log("Acaba animacion");
        CombatScene.SetActive(false);
        StartCoroutine(DelayActions());
    }
    private IEnumerator DelayActions()
    {
        // Esperamos 2 segundos
        yield return new WaitForSeconds(2f);
      
        RawCombatScene.SetActive(false);
    }
}
