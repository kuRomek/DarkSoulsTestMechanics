using UnityEngine;
using Zenject;

public class PlayerCharacterView : AttackerView
{
    [Inject] private readonly PlayerInputController _input;

    public override float MovingMagnitude => _input.MovingDirection.magnitude;
    public override Vector3 LookingDirection => CameraMovement.HorizontalRotation * _input.MovingDirection;

    public void Move()
    {
        Vector3 lookingDirection = LookingDirection;
        Vector3 calculatedMove = lookingDirection * Configs.Character.Speed;

        Move(calculatedMove);

        if (_input.MovingDirection != Vector3.zero)
            transform.rotation =
                Quaternion.Lerp(
                    transform.rotation,
                    Quaternion.LookRotation(lookingDirection), 1f / (1f + RotationSmoothness));
    }
}