using R3;

public class Attacker : Character
{
    private readonly Subject<Unit> _attacked = new Subject<Unit>();

    public Observable<Unit> Attacking => _attacked;

    public void Attack()
    {
        if (IsInputEnabled)
        {
            ToggleInput(false);
            _attacked.OnNext(Unit.Default);
        }
    }

    public void OnAttackStopped()
    {
        ToggleInput(true);
    }
}