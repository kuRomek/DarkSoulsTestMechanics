using UnityEngine;
using R3;
using System;
using System.Collections.Generic;

public class View : MonoBehaviour
{
    private List<IDisposable> _subscriptions = new List<IDisposable>();

    private readonly Subject<Vector3> _positionChanged = new Subject<Vector3>();
    private readonly Subject<Quaternion> _rotationChanged = new Subject<Quaternion>();

    public Observable<Vector3> PositionChanged => _positionChanged;
    public Observable<Quaternion> RotationChanged => _rotationChanged;

    private void OnEnable()
    {
        if (this is ISubscribable subscribable)
            subscribable.Subscribe();
    }

    private void OnDisable()
    {
        foreach (var subscription in _subscriptions)
            subscription.Dispose();
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;
        _positionChanged.OnNext(position);
    }

    public void ChangeRotation(Quaternion rotation)
    {
        transform.rotation = rotation;
        _rotationChanged.OnNext(rotation);
    }

    protected void AddSubscription(IDisposable disposable)
    {
        _subscriptions.Add(disposable);
    }
}
