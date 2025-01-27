using UnityEngine;

public class UIController : MonoBehaviour
{

    public static UIController Instance { get; private set; }

    

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    public void EndTurnButton()
    {
        MatchController.Instance.ChangeTurn();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
