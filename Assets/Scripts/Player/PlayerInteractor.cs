using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;


public class PlayerInteractor : MonoBehaviour
{
    private Player _player;
    private List<Interactable> _interactables = new();
    
    [CanBeNull]
    public Interactable GetBestInteractable(Transform center = null)
    {
        if (center == null) center = _player.rotatingTransform;
        
        float bestScore = -1;
        Interactable interactable = null;

        foreach (Interactable interactable1 in _interactables) {
            
        }

        return null;
    }
    
    public void Interact(Interactable interactable)
    {
    }

    private void Awake()
    {
        _player = GetComponentInParent<Player>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent(out Interactable interactable)) return;
        _interactables.Add(interactable);
    }

    private void LateUpdate()
    {
        _interactables.Clear();
    }
}