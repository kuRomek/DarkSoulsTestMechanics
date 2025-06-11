using Zenject;
using R3;

public class PlayerCharacterPresenter : AttackerPresenter, ITickable
{
    private readonly PlayerInputController _input;

    public PlayerCharacterPresenter(PlayerCharacter model, PlayerCharacterView view, PlayerInputController input)
        : base(model, view)
    {
        _input = input;
    }

    protected new PlayerCharacter Model => base.Model as PlayerCharacter;
    protected new PlayerCharacterView View => base.View as PlayerCharacterView;

    public override void Subscribe()
    {
        base.Subscribe();

        AddSubscription(Model.MoveInputPerformed.Subscribe(_ => View.Move()));
        AddSubscription(_input.ClickedDodgingButton.Subscribe(_ => Model.QueueDodge()));
        AddSubscription(_input.ClickedAttackButton.Subscribe(_ => Model.QueueAttack()));
    }

    public void Tick()
    {
        Model.Move();
    }
}