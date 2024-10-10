using JUtils;
using UnityEngine;


public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField, Required] public Transform targetTransform { get; private set; }
    [field: SerializeField] public float maxPositionOffset = 0.1f;
    
    public bool isLocked { get; set; }

    public void Interact(Player player)
    {
        if (!CanInteract(player)) return;
        OnInteract(player);
    }

    public virtual bool CanInteract(Player player)
    {
        return !isLocked;
    }
    
    protected abstract void OnInteract(Player player);
}