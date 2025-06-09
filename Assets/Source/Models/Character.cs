using UnityEngine;
using R3;

public class Character : Model
{
    private readonly Subject<Unit> _dodged = new Subject<Unit>();

    public Observable<Unit> Dodged => _dodged;

    public bool IsInputEnabled { get; private set; } = true;

    public void ToggleInput(bool isInputBlocked)
    {
        IsInputEnabled = isInputBlocked;
    }

    public void Move(Vector3 direction)
    {
        Position.Value += direction;
    }

    public void QueueDodge()
    {
        _dodged.OnNext(Unit.Default);
    }

    public void OnDodgeStartd()
    {
        ToggleInput(false);
    }

    public void OnDodgeStopped()
    {
        ToggleInput(true);
    }
}
