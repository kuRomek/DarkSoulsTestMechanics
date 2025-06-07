public class Presenter : ISubscribable
{
    private readonly Model _model;
    private readonly View _view;
    private readonly ISubscribable _subscribable = null;

    public Presenter(Model model, View view)
    {
        _model = model;
        _view = view;

        if (model is ISubscribable subscribable)
            _subscribable = subscribable;
    }

    protected Model Model => _model;
    protected View View => _view;

    public virtual void Subscribe()
    {
        _subscribable?.Subscribe();
    }

    public virtual void Unsubscribe()
    {
        _subscribable?.Unsubscribe();
    }
}