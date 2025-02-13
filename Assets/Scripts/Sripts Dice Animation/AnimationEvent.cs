using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private DiceMeshController dice;

    public void OnAnimationEnd()
    {
        
        Debug.Log("Acaba animacion");

    }
    
}
