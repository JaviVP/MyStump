using UnityEngine;

public class AntGroup : FactionAbstract
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Type = MatchController.TypeOfPlayers.Ant;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
