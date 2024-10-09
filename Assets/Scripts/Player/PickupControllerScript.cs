using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class PlayerPickupController : MonoBehaviour
{
    public Transform holdPosition; // Position in front of the player where the item will be held
    public float pickupRange; // Range within which the player can pick up items
    public LayerMask pickupLayer;  // Layer mask for detecting items that can be picked up
    private PickupItem currentItem; // Currently held item

    private bool NearTable;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Press 'E' to pick up or drop the item
        {
            if (currentItem == null)
            {
                TryPickupItem();
            }
            else if (NearTable)
            {
                TransferItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("this is logging");
        if (other.tag == "Placeable on")
        {

            Debug.Log("testing if collision works");
        }
        else
        {
            Debug.Log("uh oh things went wrong");
        }
    }

    void TryPickupItem()
    {
        Collider[] itemsInRange = Physics.OverlapSphere(transform.position, pickupRange, pickupLayer);

        if (itemsInRange.Length > 0)
        {
            PickupItem item = itemsInRange[0].GetComponent<PickupItem>();

            if (item != null)
            {
                currentItem = item;
                currentItem.PickUp(holdPosition); // Pick up the item and attach it to the hold position
            }
        }
    }

    void DropItem()
    {
        if (currentItem != null)
        {
            currentItem.Drop(); // Drop the currently held item
            currentItem = null;
        }
    }

    void TransferItem()
    {

    }
}
