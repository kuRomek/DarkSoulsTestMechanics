using R3;

public class AttackerAnimationEventHandler : CharacterAnimationEventHandler
{
    private readonly Subject<Unit> _stoppedAttacking = new Subject<Unit>();
    private readonly Subject<Unit> _startedAttacking = new Subject<Unit>();

    public Observable<Unit> StoppedAttacking => _stoppedAttacking;
    public Observable<Unit> StartedAttacking => _startedAttacking;

    public void StartAttacking()
    {
        _startedAttacking.OnNext(Unit.Default);
    }

    public void StopAttacking()
    {
        _stoppedAttacking.OnNext(Unit.Default);
    }
}
