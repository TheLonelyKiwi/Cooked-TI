using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SearchService;
using Vector2 = System.Numerics.Vector2;


public class PlayerInteractor : MonoBehaviour
{
    private Player _player;
    private List<Interactable> _interactables = new();
    
    [CanBeNull]
    public Interactable GetBestInteractable(Transform center = null)
    {
        if (center == null) center = _player.rotatingTransform;

        Vector3 myPosition = center.position;
        Quaternion rotation = center.rotation; 
        
        float bestScore = -1;
        Interactable bestInteractable = null;
        foreach (Interactable interactable in _interactables) {
            if (interactable.CanInteract(_player)) continue;
            
            Vector3 interactablePosition = interactable.targetTransform.position;

            float distanceScore = 1 - Mathf.Max((myPosition - interactablePosition).magnitude, 2);
            float finalScore = distanceScore;
            
            if (finalScore < bestScore) continue;
            bestScore = finalScore;
            bestInteractable = interactable;
        }

        return bestInteractable;
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