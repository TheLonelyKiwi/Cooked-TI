using UnityEngine;


public abstract class Interactable : MonoBehaviour
{
    [field: SerializeField] public Transform playerPosition { get; private set; }
    public abstract void OnInteract(Player player);
}