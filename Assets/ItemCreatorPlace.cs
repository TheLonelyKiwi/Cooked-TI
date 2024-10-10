using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class ItemCreatorPlace : MonoBehaviour
{
    public Transform holdPosition; // Position in front of the player where the item will be held
    public float pickupRange; // Range within which the player can pick up items
    public LayerMask pickupLayer;  // Layer mask for detecting items that can be picked up
    private PickupItem currentItem; // Currently held item
    private PutOnTable PutOnTable;
    public bool NearPlayer = false;
    public GameObject CubePrefab;
    public bool HasCubeAlready;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && NearPlayer == true && HasCubeAlready == false) // Press 'E' to pick up or drop the item
        {
            Instantiate(CubePrefab, holdPosition.position, holdPosition.rotation);
           
        }
        else
        {
            Debug.Log("failed to create item");
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
        if (other.tag == "Item Tag")
        {
            HasCubeAlready = true;
            Debug.Log("Has Cube on top");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (NearPlayer == true)
        {
            NearPlayer = false;
            Debug.Log("NearPlayer has been set to FALSE");
        }
        if (HasCubeAlready == true)
        {
            HasCubeAlready = false;
            Debug.Log("Spawner No Longer has Cube");
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
