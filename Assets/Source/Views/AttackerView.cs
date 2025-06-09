using R3;
using UnityEngine;

public class AttackerView : CharacterView
{
    private readonly Subject<Unit> _attackStopped = new Subject<Unit>();

    public Observable<Unit> AttackStopped => _attackStopped;

    public new AttackerAnimationEventHandler AnimationEventHandler =>
        base.AnimationEventHandler as AttackerAnimationEventHandler;

    public override void Subscribe()
    {
        base.Subscribe();

        AddSubscription(AnimationEventHandler.StoppedAttacking.Subscribe(_ => OnAttackStopped()));
    }

    public void OnAttacking()
    {
        Animator.SetTrigger(CharacterAnimatorData.Params.Attack);
    }

    public void OnAttackStopped()
    {
        _attackStopped.OnNext(Unit.Default);
    }
}
