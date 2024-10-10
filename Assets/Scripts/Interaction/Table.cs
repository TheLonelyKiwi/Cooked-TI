using UnityEngine;


public class Table : Interactable
{
    [SerializeField] private Inventory _inventory;
    
    protected override void OnInteract(Player player)
    {
    }
}