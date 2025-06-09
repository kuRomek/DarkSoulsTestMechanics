using R3;
using UnityEngine;

public class CharacterAnimationEventHandler : MonoBehaviour
{
    private readonly Subject<Unit> _stoppedDodging = new Subject<Unit>();

    public Observable<Unit> StoppedDodging => _stoppedDodging;

    public void StopDodging()
    {
        _stoppedDodging.OnNext(Unit.Default);
    }
}
