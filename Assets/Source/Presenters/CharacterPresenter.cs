using R3;

public class CharacterPresenter : Presenter, ISubscribable
{
    public CharacterPresenter(Character model, CharacterView view)
        : base(model, view) { }

    protected new Character Model => base.Model as Character;
    protected new CharacterView View => base.View as CharacterView;

    public virtual void Subscribe()
    {
        AddSubscription(Model.DodgeQueued.Subscribe(_ => View.QueueDodgeAnimation()));
        AddSubscription(View.StartedDodging.Subscribe(_ => Model.OnDodgeStartd()));
        AddSubscription(View.StoppedDodging.Subscribe(_ => Model.OnDodgeStopped()));
    }
}
