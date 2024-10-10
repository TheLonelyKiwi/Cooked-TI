using UnityEngine;


public class PlayerCraftingState : BasePlayerState
{
    private float _timeLeft;
    private Recipe _recipe;
    private CraftingStation _craftingStation;
    
    protected override void OnActivate()
    {
        base.OnActivate();

        _craftingStation = stateData.Get<CraftingStation>(0);
        _craftingStation.isLocked = true;
        _recipe = stateData.Get<Recipe>(1);
        _timeLeft = _recipe.productionTime;
    }

    protected override void OnDeactivate()
    {
        base.OnDeactivate();
        _craftingStation.isLocked = false;
    }

    protected override void ActiveUpdate()
    {
        if (!player.input.isInteractPressed) {
            stateMachine.ContinueQueue();
            return;
        }

        _timeLeft -= Time.deltaTime;
        if (_timeLeft > 0) return;

        _craftingStation.FinishedCrafting();
        stateMachine.ContinueQueue();
    }
}