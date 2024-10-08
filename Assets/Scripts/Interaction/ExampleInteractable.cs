using UnityEngine;

public class ExampleInteractable : Interactable
{
    protected override void OnInteract(Player player)
    {
        Debug.Log("Hello!");
    }
}