using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupController : MonoBehaviour
{
    public Transform holdPosition; // Position in front of the player where the item will be held
    public float pickupRange = 2f; // Range within which the player can pick up items
    public LayerMask pickupLayer;  // Layer mask for detecting items that can be picked up
    private PickupItem currentItem; // Currently held item

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) // Press 'E' to pick up or drop the item
        {
            if (currentItem == null)
            {
                TryPickupItem();
            }
            else
            {
                DropItem();
            }
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
}
