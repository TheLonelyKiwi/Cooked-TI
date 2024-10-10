using JUtils;
using UnityEngine;


public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField, Required] public Transform targetTransform { get; private set; }
    [field: SerializeField] public float maxPositionOffset = 0.25f;
    
    public bool isLocked { get; set; }

    public void Interact(Player player)
    {
        if (isLocked) return;
        OnInteract(player);
    }
    
    protected abstract void OnInteract(Player player);
}