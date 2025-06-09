using R3;

public class AttackerAnimationEventHandler : CharacterAnimationEventHandler
{
    private readonly Subject<Unit> _stoppedAttacking = new Subject<Unit>();

    public Observable<Unit> StoppedAttacking => _stoppedAttacking;

    public void StopAttacking()
    {
        _stoppedAttacking.OnNext(Unit.Default);
    }
}
