using UnityEngine;
using R3;

public class Character : Model
{
    private readonly Subject<Vector3> _dodged = new Subject<Vector3>();

    public Observable<Vector3> Dodged => _dodged;

    public bool IsInputEnabled { get; private set; } = true;

    public void ToggleInput(bool isInputBlocked)
    {
        IsInputEnabled = isInputBlocked;
    }

    public void Move(Vector3 direction)
    {
        Position.Value += direction;
    }

    public void Dodge(Vector3 inputDirection, Quaternion additionalRotation)
    {
        if (IsInputEnabled)
        {
            ToggleInput(false);
            _dodged.OnNext(additionalRotation * inputDirection);
        }
    }

    public void OnStoppedDodging()
    {
        ToggleInput(true);
    }
}
