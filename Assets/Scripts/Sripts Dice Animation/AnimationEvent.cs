using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    [SerializeField] private activateStampide dice;

    public void OnAnimationEnd()
    {
        //dice.MeshChanger();
        Debug.Log("Acaba animacion");

    }
    
}
