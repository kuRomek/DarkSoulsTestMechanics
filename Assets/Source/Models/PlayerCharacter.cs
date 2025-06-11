using R3;

public class PlayerCharacter : Attacker
{
    private Subject<Unit> _moveInputPerformed = new Subject<Unit>();

    public Observable<Unit> MoveInputPerformed => _moveInputPerformed;

    public void Move()
    {
        if (IsInputEnabled)
            _moveInputPerformed.OnNext(Unit.Default);
    }
}