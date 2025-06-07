using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Range(0f, 100f)] private float _smoothness;

    [Inject] private PlayerInputController _input;

    private Vector3 _offset;
    private float _verticalRotation;

    public Vector3 Forward { get; private set; }
    public Quaternion HorizontalRotation { get; private set; }

    private void Start()
    {
        _offset = transform.position;
        _verticalRotation = transform.localEulerAngles.x;
    }

    private void Update()
    {
        RotateVertical();
        RotateHorizontal();
    }

    private void RotateVertical()
    {
        Vector2 velocity = _input.LookingDirection * Configs.Character.RotationSensitivity * Time.deltaTime;

        _verticalRotation -= velocity.y;
        _verticalRotation = Mathf.Clamp(_verticalRotation, -89f, 89f);

        transform.localEulerAngles = new Vector3(
            _verticalRotation,
            transform.localEulerAngles.y,
            transform.localEulerAngles.z);

        transform.eulerAngles = new Vector3(
            transform.eulerAngles.x,
            transform.eulerAngles.y + velocity.x,
            transform.eulerAngles.z);
    }

    private void RotateHorizontal()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, 1 / (_smoothness + 1));
        Forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        HorizontalRotation = Quaternion.LookRotation(Forward);
    }
}
