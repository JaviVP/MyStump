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
        
        StartCoroutine(DelayActions());
    }
    private IEnumerator DelayActions()
    {
        //1.5s delay
        yield return new WaitForSeconds(1.5f);
        CombatScene.SetActive(false);
        RawCombatScene.SetActive(false);
     

    }
}
