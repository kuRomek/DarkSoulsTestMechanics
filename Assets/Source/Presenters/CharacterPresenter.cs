using R3;

public class CharacterPresenter : Presenter, ISubscribable
{
    public CharacterPresenter(Character model, CharacterView view)
        : base(model, view) { }

    protected new Character Model => base.Model as Character;
    protected new CharacterView View => base.View as CharacterView;

    public virtual void Subscribe()
    {
        AddSubscription(Model.Position.Subscribe(View.OnMoved));
        AddSubscription(Model.Rotation.Subscribe(View.OnRotated));
        AddSubscription(View.PositionChanged.Subscribe(position => Model.Position.Value = position));
        AddSubscription(View.RotationChanged.Subscribe(rotation => Model.Rotation.Value = rotation));
        AddSubscription(Model.Dodged.Subscribe(View.OnDodging));
        AddSubscription(View.StoppedDodging.Subscribe(_ => Model.OnStoppedDodging()));
    }
}
