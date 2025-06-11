using DG.Tweening;
using R3;
using UnityEngine;

public class CharacterView : View, ISubscribable
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterAnimationEventHandler _animationEventHandler;
    [SerializeField, Range(0f, 100f)] private float _rotationSmoothness;

    private Dodge _currentDodge = null;

    private readonly Subject<Unit> _startedDodging = new Subject<Unit>();
    private readonly Subject<Unit> _stoppedDodging = new Subject<Unit>();

    public Observable<Unit> StartedDodging => _startedDodging;
    public Observable<Unit> StoppedDodging => _stoppedDodging;

    public virtual float MovingMagnitude => 1f;
    public virtual Vector3 LookingDirection => transform.forward;
    protected float RotationSmoothness => _rotationSmoothness;
    protected Animator Animator => _animator;
    protected CharacterAnimationEventHandler AnimationEventHandler => _animationEventHandler;

    private void FixedUpdate()
    {
        _characterController.Move(Physics.gravity * Time.fixedDeltaTime);
        OnPositionChanged();
    }

    public virtual void Subscribe()
    {
        AddSubscription(_animationEventHandler.StartedDodging.Subscribe(_ => Dodge()));
    }

    public void Move(Vector3 direction)
    {
        _characterController.Move(direction * Time.deltaTime);
        _animator.SetFloat(CharacterAnimatorData.Params.MovingMagnitude, MovingMagnitude);
        OnPositionChanged();
    }

    public void QueueDodgeAnimation()
    {
        _animator.SetTrigger(CharacterAnimatorData.Params.Dodge);
    }

    public void Dodge()
    {
        _currentDodge?.Interrupt();

        _startedDodging.OnNext(Unit.Default);

        Animator.SetFloat(
            CharacterAnimatorData.Params.DodgeSpeed,
            Configs.Character.DodgeAnimationDuration / Configs.Character.DodgeDuration);

        Vector3 dodgeDirection = LookingDirection;
        dodgeDirection = dodgeDirection == Vector3.zero ? transform.forward : dodgeDirection.normalized;

        _currentDodge = new Dodge(dodgeDirection, _characterController);
        _currentDodge.PositionChanged += OnPositionChanged;
        _currentDodge.DodgeFinished += OnStoppedDodging;

        transform.DORotateQuaternion(
            Quaternion.LookRotation(dodgeDirection),
            0.3f);
    }

    public void OnStoppedDodging()
    {
        _currentDodge.PositionChanged -= OnPositionChanged;
        _currentDodge.DodgeFinished -= OnStoppedDodging;
        _currentDodge = null;
        _stoppedDodging.OnNext(Unit.Default);
    }
}
