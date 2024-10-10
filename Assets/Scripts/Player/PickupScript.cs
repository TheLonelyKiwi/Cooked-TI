using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.iOS;

public class PickupItem : MonoBehaviour
{
    public Transform holdPosition; // Reference to where the item will be held in front of the player
    private bool isPickedUp = false; // Whether the item is currently held by the player


    // Call this function when the player presses the pickup button
    public void PickUp(Transform playerHoldPosition)
    {
        isPickedUp = true;
        holdPosition = playerHoldPosition;
        GetComponent<Rigidbody>().isKinematic = true; // Disable physics so the item doesn't fall
        transform.position = holdPosition.position;
        transform.parent = holdPosition; // Parent the item to the player's hold position
    }

    // Call this function when the player drops the item
    public void Drop()
    {
        isPickedUp = false;
        transform.parent = null; // Unparent the item from the player
        GetComponent<Rigidbody>().isKinematic = false; // Enable physics so the item falls naturally
    }
    public void TablePickUp(Transform playerHoldPosition)
    {
        isPickedUp = true;
        holdPosition = playerHoldPosition;
        GetComponent<Rigidbody>().isKinematic = true; // Disable physics so the item doesn't fall
        transform.position = holdPosition.position;
        transform.parent = holdPosition; // Parent the item to the player's hold position
    }

    // Call this function when the player drops the item
    public void TableDrop()
    {
        isPickedUp = false;
        transform.parent = null; // Unparent the item from the player
        GetComponent<Rigidbody>().isKinematic = false; // Enable physics so the item falls naturally
    }
}