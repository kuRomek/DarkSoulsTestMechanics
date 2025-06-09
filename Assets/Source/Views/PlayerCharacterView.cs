using Zenject;

public class PlayerCharacterView : AttackerView
{
    [Inject] private readonly PlayerInputController _input;

    public override float MovingMagnitude => _input.MovingDirection.magnitude;
}