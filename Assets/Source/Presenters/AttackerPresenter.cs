using R3;

public class AttackerPresenter : CharacterPresenter
{
    public AttackerPresenter(Attacker model, AttackerView view)
        : base(model, view) { }

    protected new Attacker Model => base.Model as Attacker;
    protected new AttackerView View => base.View as AttackerView;

    public override void Subscribe()
    {
        base.Subscribe();

        AddSubscription(Model.AttackQueued.Subscribe(_ => View.QueueAttackingAnimation()));
        AddSubscription(View.AttackStarted.Subscribe(_ => Model.OnAttackStarted()));
        AddSubscription(View.AttackStopped.Subscribe(_ => Model.OnAttackStopped()));
    }
}