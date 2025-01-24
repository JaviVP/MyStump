using UnityEngine;

public class TermiteGroup : FactionAbstract
{
    public override void Attack()
    {
        throw new System.NotImplementedException();
    }

    public override void Build()
    {
        throw new System.NotImplementedException();
    }

    public override void Consume()
    {
        throw new System.NotImplementedException();
    }

    public override bool Move()
    {
        throw new System.NotImplementedException();
    }

    public override void Recolect()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        Type = MatchController.TypeOfPlayers.Termite;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created

}
