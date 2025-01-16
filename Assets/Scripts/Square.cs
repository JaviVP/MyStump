using UnityEngine;
using System.Collections.Generic;

public class Square : MonoBehaviour
{
    protected int id;
    [SerializeField]
    protected List <Square> adjacents = new List<Square> ();
    [SerializeField]
    private GameObject squareObject;

    public GameObject SquareObject { get => squareObject; set => squareObject = value; }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }




}
