using UnityEngine;
using Zenject;

public class PlayerCharacterView : AttackerView
{
    [Inject] private readonly PlayerInputController _input;

    public override float MovingMagnitude => _input.MovingDirection.magnitude;

    public override Vector3 DodgingDirection => 
        CameraMovement.Instance.HorizontalRotation * _input.MovingDirection.normalized;
}