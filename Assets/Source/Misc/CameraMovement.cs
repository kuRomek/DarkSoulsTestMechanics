using UnityEngine;
using Zenject;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField, Range(0f, 100f)] private float _smoothness;

    [Inject] private PlayerInputController _input;

    private static CameraMovement _instance = null;

    private Vector3 _offset;
    private float _verticalRotation;

    public static CameraMovement Instance => _instance;
    public Vector3 Forward { get; private set; }
    public Quaternion HorizontalRotation { get; private set; }

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

        Forward = new Vector3(transform.forward.x, 0f, transform.forward.z).normalized;
        HorizontalRotation = Quaternion.LookRotation(Forward);
    }

    private void Move()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position + _offset, 1 / (_smoothness + 1));
    }
}
