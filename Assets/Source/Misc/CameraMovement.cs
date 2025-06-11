using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Range(0f, 100f)] private float _smoothness;

    [Inject] private PlayerInputController _input;

    private static CameraMovement _instance = null;

    private Vector3 _forward;
    private Quaternion _horizontalRotation;
    private Vector3 _offset;
    private float _verticalRotation;

    public static CameraMovement Instance => _instance;

    public static Vector3 Forward
    {
        get => _instance == null ? default : _instance._forward;
        private set => _instance._forward = value;
    }

    public static Quaternion HorizontalRotation
    {
        get => _instance == null ? default : _instance._horizontalRotation;
        private set => _instance._horizontalRotation = value;
    }

    private void Awake()
    {
        if (_instance == null)
            _instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        _offset = transform.position;
        _verticalRotation = transform.localEulerAngles.x;
    }

    private void LateUpdate()
    {
        Rotate();
        Move();
    }

    private void Rotate()
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

        _forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;

        _horizontalRotation = Quaternion.LookRotation(_forward);
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, 1 / (_smoothness + 1));
    }
}
