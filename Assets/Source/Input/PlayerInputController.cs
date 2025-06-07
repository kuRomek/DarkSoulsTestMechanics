using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : ISubscribable
{
    private PlayerInput _input;

    public Vector2 MovingDirection { get; private set; }
    public Vector2 LookingDirection { get; private set; }

    public PlayerInputController()
    {
        _input = new PlayerInput();
    }

    public void Subscribe()
    {
        _input.Enable();

        _input.Player.Move.performed += OnMoved;
        _input.Player.Move.canceled += OnMoved;
        _input.Player.Look.performed += OnLooked;
        _input.Player.Look.canceled += OnLooked;
    }

    public void Unsubscribe()
    {
        _input.Disable();

        _input.Player.Move.performed -= OnMoved;
        _input.Player.Move.canceled -= OnMoved;
        _input.Player.Look.performed -= OnLooked;
        _input.Player.Look.canceled -= OnLooked;
    }

    private void OnMoved(InputAction.CallbackContext context)
    {
        MovingDirection = context.ReadValue<Vector2>();
    }

    private void OnLooked(InputAction.CallbackContext context)
    {
        LookingDirection = context.ReadValue<Vector2>();
    }
}
