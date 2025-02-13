using UnityEngine;

public class FactionQuantity : MonoBehaviour
{
    [SerializeField] int quantitySoldier;
    [SerializeField] int quantityWorker;

    public int QuantitySoldier { get => quantitySoldier; set => quantitySoldier = value; }
    public int QuantityWorker { get => quantityWorker; set => quantityWorker = value; }
}
