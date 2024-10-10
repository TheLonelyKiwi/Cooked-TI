using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOnTable : MonoBehaviour
{
    public Transform PutOntoTable;
    private bool isPickedUp;

    public void PuttingOnTable(Transform OnTablePosition)
    {
        isPickedUp = true;
        PutOntoTable = OnTablePosition;
        GetComponent<Rigidbody>().isKinematic = true; // Disable physics so the item doesn't fall
        transform.position = OnTablePosition.position;
        transform.parent = OnTablePosition; // Parent the item to the player's hold position
    }
}
