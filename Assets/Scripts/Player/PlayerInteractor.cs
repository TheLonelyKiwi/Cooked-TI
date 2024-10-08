using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    private List<Collider> _triggers = new();

    public void Interact(Interactable interactable)
    {
    }

    private void OnTriggerStay(Collider other)
    {
        _triggers.Add(other);
    }

    private void LateUpdate()
    {
        _triggers.Clear();
    }
}