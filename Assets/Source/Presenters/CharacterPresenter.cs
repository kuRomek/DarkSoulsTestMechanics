public class CharacterPresenter : Presenter
{
    public CharacterPresenter(Character model, CharacterView view)
        : base(model, view) { }

    protected new Character Model => base.Model as Character;
    protected new CharacterView View => base.View as CharacterView;
}