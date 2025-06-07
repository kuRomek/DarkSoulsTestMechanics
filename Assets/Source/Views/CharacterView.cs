using UnityEngine;
using Zenject;

public class CharacterView : View
{
    private readonly int _movingMagnitude = Animator.StringToHash(nameof(MovingMagnitude));

    [SerializeField] private Animator _animator;
    [SerializeField] private CameraMovement _cameraMovement;

    [Inject] private PlayerInputController _input;

    public int MovingMagnitude => _movingMagnitude;

    public void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 direction3d =
            _cameraMovement.HorizontalRotation *
            new Vector3(_input.MovingDirection.x, 0f, _input.MovingDirection.y);

        if (direction3d.x != 0f || direction3d.z != 0f)
            transform.forward = direction3d;

        transform.Translate(Configs.Character.Speed * Time.deltaTime * direction3d, Space.World);

        _animator.SetFloat(MovingMagnitude, _input.MovingDirection.magnitude);
    }
}
