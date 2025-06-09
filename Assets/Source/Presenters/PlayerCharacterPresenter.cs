using UnityEngine;
using Zenject;
using R3;

public class PlayerCharacterPresenter : AttackerPresenter, ITickable
{
    private readonly PlayerInputController _input;

    public PlayerCharacterPresenter(PlayerCharacter model, PlayerCharacterView view, PlayerInputController input) :
        base(model, view)
    {
        _input = input;
    }

    protected new PlayerCharacter Model => base.Model as PlayerCharacter;

    public override void Subscribe()
    {
        base.Subscribe();

        AddSubscription(_input.ClickedDodgingButton.Subscribe(_ => Model.QueueDodge()));
        AddSubscription(_input.ClickedAttackButton.Subscribe(_ => Model.QueueAttack()));
    }

    public void Tick()
    {
        Model.Move(
            _input.MovingDirection,
            CameraMovement.Instance.HorizontalRotation,
            Configs.Character.Speed * Time.deltaTime);

        View.OnMoved(Model.Position.Value);
        View.OnRotated(Model.Rotation.Value);
    }
}