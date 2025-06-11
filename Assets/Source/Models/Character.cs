using R3;

public class Character : Model
{
    private readonly Subject<Unit> _dodged = new Subject<Unit>();

    public Observable<Unit> DodgeQueued => _dodged;

    public bool IsInputEnabled { get; private set; } = true;

    public void ToggleInput(bool isInputEnabled)
    {
        IsInputEnabled = isInputEnabled;
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
