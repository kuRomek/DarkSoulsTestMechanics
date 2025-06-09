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

        AddSubscription(Model.Attacking.Subscribe(_ => View.OnAttacking()));
        AddSubscription(View.AttackStopped.Subscribe(_ => Model.OnAttackStopped()));
    }
}