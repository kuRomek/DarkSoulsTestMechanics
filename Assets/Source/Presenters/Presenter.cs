using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Presenter : IActivatable, IUpdatable
{
    private readonly Model _model;
    private readonly View _view;
    private readonly IActivatable _activatable = null;
    private readonly IUpdatable _updatable = null;
    private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

    public Presenter(Model model, View view)
    {
        _model = model;
        _view = view;

        if (model is IActivatable subscribable)
            _activatable = subscribable;

        if (model is IUpdatable updatable)
            _updatable = updatable;
    }

    protected Model Model => _model;
    protected View View => _view;
    protected List<IDisposable> Subscriptions => _subscriptions;

    public void AddSubscription(IDisposable subscription)
    {
        _subscriptions.Add(subscription);
    }

    public void Enable()
    {
        _activatable?.Enable();

        if (this is ISubscribable subscribable)
            subscribable.Subscribe();
    }

    public void Disable()
    {
        _activatable?.Disable();

        foreach (var subscription in _subscriptions)
            subscription.Dispose();
    }

    public void Update(float deltaTime)
    {
        _updatable?.Update(Time.deltaTime);

        if (this is ITickable tickable)
            tickable.Tick();
    }
}