using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public abstract class FactionAbstract 
{


    public GameObject objectFaction;
    [SerializeField]
    private int quantitySoldier;
    [SerializeField]
    private int quantityWorker;
    private MatchController.TypeOfPlayers type;


    public int QuantitySoldier { get => quantitySoldier; set => quantitySoldier = value; }
    public int QuantityWorker { get => quantityWorker; set => quantityWorker = value; }
    public MatchController.TypeOfPlayers Type { get => type; set => type = value; }

    public abstract bool Move();
    public abstract void Attack();

    public abstract void Build();

    public abstract void Recolect();

    public abstract void Consume();

    

}
