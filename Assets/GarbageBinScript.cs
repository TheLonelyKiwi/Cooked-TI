using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class GarbageBinScript : MonoBehaviour
{
    public Transform holdPosition; // Position in front of the player where the item will be held
    public float pickupRange; // Range within which the player can pick up items
    public LayerMask pickupLayer;  // Layer mask for detecting items that can be picked up
    private PickupItem currentItem; // Currently held item
    public bool NearPlayer = false;
    public GameObject DestroyPlane;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && NearPlayer == true) // Press 'E' to pick up or drop the item
        {
            if (currentItem == null)
            {
                TryTablePickupItem();
            }
            else
            {
                DropItem();
            }
        }
    }

    public void OnTriggerStay(Collider other)
    {
        //Debug.Log("this is logging");
        if (other.tag == "Player")
        {
            NearPlayer = true;
            Debug.Log("NearPlayer has been set to TRUE");
        }
    }

    public void OnTriggerExit()
    {
        if (NearPlayer == true)
        {
            NearPlayer = false;
            Debug.Log("NearPlayer has been set to FALSE");
        }
    }

    void TryTablePickupItem()
    {
        Collider[] itemsInRange = Physics.OverlapSphere(transform.position, pickupRange, pickupLayer);

        if (itemsInRange.Length > 0)
        {
            PickupItem item = itemsInRange[0].GetComponent<PickupItem>();

            if (item != null)
            {
                currentItem = item;
                currentItem.TablePickUp(holdPosition); // Pick up the item and attach it to the hold position
                DropItem();
            }
        }
    }
    void DropItem()
    {
        if (currentItem != null)
        {
            currentItem.Drop(); // Pick up the item and attach it to the hold position
            currentItem = null;
        }
    }
}