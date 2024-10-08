using System.Collections;
using UnityEngine;


public class PlayerPreInteractState : BasePlayerState
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
        yield return null;
    }
}