using JUtils;

public class DefaultPlayerState : BasePlayerState
{
    protected override bool canMove => true;
    protected override bool canInteract => true;
}