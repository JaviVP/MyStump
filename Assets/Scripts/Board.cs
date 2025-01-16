using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class Board : MonoBehaviour
{

    List <Square> myBoard = new List<Square> ();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Square sq = new Square ();
            //sq.SquareObject(this.gameObject.transform.GetChild(i).gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
