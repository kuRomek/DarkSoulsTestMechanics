using R3;
using UnityEngine;

public class CharacterAnimationEventHandler : MonoBehaviour
{
    private readonly Subject<Unit> _stoppedDodging = new Subject<Unit>();
    private readonly Subject<Unit> _startedDodging = new Subject<Unit>();

    public Observable<Unit> StoppedDodging => _stoppedDodging;
    public Observable<Unit> StartedDodging => _startedDodging;

    public void StartDodging()
    {
        _startedDodging.OnNext(Unit.Default);
    }

    public void StopDodging()
    {
        _stoppedDodging.OnNext(Unit.Default);
    }
}
