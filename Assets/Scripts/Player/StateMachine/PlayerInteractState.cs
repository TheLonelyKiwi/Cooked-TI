using System.Collections;
using JUtils;
using UnityEngine;


// Meant as a state that happens before interacting, moving & rotating the player when required
public class PlayerInteractState : BasePlayerState
{
    protected override void OnActivate()
    {
        base.OnActivate();
        StartCoroutine(WalkToInteractableRoutine(stateData.Get<Interactable>(0)));
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
    }

    private IEnumerator WalkToInteractableRoutine(Interactable interactable)
    {
        Quaternion targetRotation = Quaternion.LookRotation((interactable.transform.position - player.transform.position).With(y: 0));

        while (Quaternion.Angle(targetRotation, player.rotatingTransform.rotation) > 1) {
            if (!interactable.CanInteract(player)) {
                stateMachine.ContinueQueue();
                yield break;
            }
            
            Quaternion newRotation = Quaternion.Slerp(player.rotatingTransform.rotation, targetRotation, 1 - Mathf.Exp(-20 * Time.deltaTime));
            player.rotatingTransform.rotation = newRotation;
            yield return null;
        }
        
        if (!interactable.CanInteract(player)) {
            stateMachine.ContinueQueue();
        } else {
            interactable.Interact(player);
            if (isActive) {
                stateMachine.ContinueQueue();
            }
        }
    }
}