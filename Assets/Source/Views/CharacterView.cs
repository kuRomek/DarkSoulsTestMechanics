using DG.Tweening;
using R3;
using UnityEngine;

public class CharacterView : View, ISubscribable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterAnimationEventHandler _animationEventHandler;
    [SerializeField, Range(0f, 100f)] private float _rotationSmoothness;

    private readonly Subject<Unit> _stoppedDodging = new Subject<Unit>();

    public Observable<Unit> StoppedDodging => _stoppedDodging;

    public Animator Animator => _animator;
    public CharacterAnimationEventHandler AnimationEventHandler => _animationEventHandler;
    public virtual float MovingMagnitude => 1f;

    public virtual void Subscribe()
    {
        AddSubscription(_animationEventHandler.StoppedDodging.Subscribe(_ => OnStoppedDodging()));
    }

    public void OnMoved(Vector3 position)
    {
        transform.position = position;
        _animator.SetFloat(CharacterAnimatorData.Params.MovingMagnitude, MovingMagnitude);
    }

    public void OnRotated(Quaternion rotation)
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 1f / (1f + _rotationSmoothness));
    }

    public void OnDodging(Vector3 direction)
    {
        _animator.SetTrigger(CharacterAnimatorData.Params.Dodge);

        transform.DOMove(
            direction * Configs.Character.DodgeRange,
            Configs.Character.DodgeDuration).
            SetRelative().
            OnUpdate(() => ChangePosition(transform.position));

        transform.DORotateQuaternion(
            Quaternion.LookRotation(direction),
            0.3f);
    }

    public void OnStoppedDodging()
    {
        _stoppedDodging.OnNext(Unit.Default);
    }
}
