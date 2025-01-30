using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    [SerializeField]
    private TMP_Text tracesUI;
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

    public void WriteTrace(string t)
    {
        tracesUI.text += t+ "\n";
    }
    public void ResetAllTraces()
    {
        tracesUI.text = "";
    }
    public void EndTurnButton()
    {
        BoardController.Instance.EnableSquareCollider(false);
        BoardController.Instance.ResetStateSquareColor();
        MatchController.Instance.ChangeTurn();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
