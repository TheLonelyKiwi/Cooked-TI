using JUtils;
using UnityEngine;


public class Table : Interactable
{
    [Space]
    [SerializeField, Required] private Inventory _inventory;

    public override bool CanInteract(Player player)
    {
        return base.CanInteract(player) && (player.inventory.IsFull() == _inventory.IsFull());
    }

    protected override void OnInteract(Player player)
    {
    }
}