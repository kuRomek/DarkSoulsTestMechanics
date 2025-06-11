using R3;
using UnityEngine;

public class AttackerView : CharacterView
{
    private readonly Subject<Unit> _attackStarted = new Subject<Unit>();
    private readonly Subject<Unit> _attackStopped = new Subject<Unit>();
    
    public Observable<Unit> AttackStarted => _attackStarted;
    public Observable<Unit> AttackStopped => _attackStopped;

    protected new AttackerAnimationEventHandler AnimationEventHandler =>
        base.AnimationEventHandler as AttackerAnimationEventHandler;

    public override void Subscribe()
    {
        base.Subscribe();

        AddSubscription(AnimationEventHandler.StartedAttacking.Subscribe(_ => OnAttackStarted()));
        AddSubscription(AnimationEventHandler.StoppedAttacking.Subscribe(_ => OnAttackStopped()));
    }

    public void QueueAttackingAnimation()
    {
        Animator.SetTrigger(CharacterAnimatorData.Params.Attack);
    }

    public void OnAttackStarted()
    {
        _attackStarted.OnNext(Unit.Default);
    }

    public void OnAttackStopped()
    {
        _attackStopped.OnNext(Unit.Default);
    }
}
