using R3;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : IActivatable
{
    private readonly PlayerInput _input;

    private readonly Subject<Unit> _clickedDodgingButton = new Subject<Unit>();
    private readonly Subject<Unit> _clickedAttackButton = new Subject<Unit>();
    
    public PlayerInputController()
    {
        _input = new PlayerInput();
    }

    public Observable<Unit> ClickedDodgingButton => _clickedDodgingButton;
    public Observable<Unit> ClickedAttackButton => _clickedAttackButton;

    public Vector3 MovingDirection { get; private set; }
    public Vector2 LookingDirection { get; private set; }

    public void Enable()
    {
        _input.Enable();

        _input.Player.Move.performed += OnMoved;
        _input.Player.Move.canceled += OnMoved;
        _input.Player.Look.performed += OnLooked;
        _input.Player.Look.canceled += OnLooked;
        _input.Player.Dodge.performed += OnDodged;
        _input.Player.Attack.performed += OnAttacked;
    }

    public void Disable()
    {
        _input.Disable();

        _input.Player.Move.performed -= OnMoved;
        _input.Player.Move.canceled -= OnMoved;
        _input.Player.Look.performed -= OnLooked;
        _input.Player.Look.canceled -= OnLooked;
        _input.Player.Dodge.performed -= OnDodged;
        _input.Player.Attack.performed -= OnAttacked;
    }

    private void OnMoved(InputAction.CallbackContext context)
    {
        Vector3 direction = context.ReadValue<Vector2>();
        MovingDirection = new Vector3(direction.x, 0f, direction.y);
    }

    private void OnLooked(InputAction.CallbackContext context)
    {
        LookingDirection = context.ReadValue<Vector2>();
    }

    private void OnDodged(InputAction.CallbackContext context)
    {
        _clickedDodgingButton.OnNext(Unit.Default);
    }

    private void OnAttacked(InputAction.CallbackContext context)
    {
        _clickedAttackButton.OnNext(Unit.Default);
    }
}
