using UnityEngine;
using R3;

public class Model
{
    public Model(Vector3 position = default, Quaternion rotation = default)
    {
        Position.Value = position;
        Rotation.Value = rotation;
    }

    public ReactiveProperty<Vector3> Position { get; } = new ReactiveProperty<Vector3>();
    public ReactiveProperty<Quaternion> Rotation { get; } = new ReactiveProperty<Quaternion>();
}