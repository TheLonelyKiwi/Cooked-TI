using UnityEngine;

namespace Interaction
{
    public class ExampleInteractable : Interactable
    {
        public override void OnInteract(Player player)
        {
            Debug.Log("Hello!");
        }
    }
}