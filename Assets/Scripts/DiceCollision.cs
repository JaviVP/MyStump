using UnityEngine;

public class DiceCollision : MonoBehaviour
{
    [SerializeField] private Vector3 pushD1;
    [SerializeField] private Vector3 pushD2;
    [SerializeField] private Vector3 torqueD1;
    [SerializeField] private Vector3 torqueD2;
    [SerializeField] private GameObject dice1;
    [SerializeField] private GameObject dice2;

    void Start()
    {
        dice1.GetComponent<Rigidbody>().AddForce(pushD1 * 50);
        dice2.GetComponent<Rigidbody>().AddForce(pushD2 * 50);

        dice1.GetComponent<Rigidbody>().AddTorque(torqueD1);
        dice2.GetComponent<Rigidbody>().AddTorque(torqueD2);
    }
}
