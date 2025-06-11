using R3;

public class Attacker : Character
{
    private readonly Subject<Unit> _attacked = new Subject<Unit>();

    public Observable<Unit> AttackQueued => _attacked;

    public void QueueAttack()
    {
        _attacked.OnNext(Unit.Default);
    }

    public void OnAttackStarted()
    {
        ToggleInput(false);
    }

    public void OnAttackStopped()
    {
        ToggleInput(true);
    }
}