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
        Vector3 targetPosition = interactable.targetTransform.position.With(y: player.rigidbody.position.y);
        Quaternion targetRotation = Quaternion.Euler(interactable.targetTransform.eulerAngles.With(x:0,z:0));

        while (Vector3.Magnitude(player.rigidbody.position - targetPosition) > interactable.maxPositionOffset) {
            if (interactable.isLocked) {
                stateMachine.ContinueQueue();
                yield break;
            }

            Vector3 newPosition = Vector3.Lerp(player.rigidbody.position, targetPosition, 1 - Mathf.Exp(-10 * Time.deltaTime));
            
            Quaternion targetRot = Quaternion.LookRotation(newPosition - player.rigidbody.position);
            Quaternion newRotation = Quaternion.Slerp(player.rotatingTransform.rotation, targetRot, 1 - Mathf.Exp(-20 * Time.deltaTime));
                
            player.rigidbody.position = newPosition;
            player.rotatingTransform.rotation = newRotation;
            yield return null;
        }

        while (Quaternion.Angle(targetRotation, player.rotatingTransform.rotation) > 1) {
            if (interactable.isLocked) {
                stateMachine.ContinueQueue();
                yield break;
            }
            
            Quaternion newRotation = Quaternion.Slerp(player.rotatingTransform.rotation, targetRotation, 1 - Mathf.Exp(-20 * Time.deltaTime));
            player.rotatingTransform.rotation = newRotation;
            yield return null;
        }
        
        if (interactable.isLocked) {
            stateMachine.ContinueQueue();
        } else {
            interactable.Interact(player);
            if (isActive) {
                stateMachine.ContinueQueue();
            }
        }
    }
}