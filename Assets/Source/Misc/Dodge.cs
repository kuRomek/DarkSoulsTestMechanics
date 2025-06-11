using System;
using DG.Tweening;
using UnityEngine;

public class Dodge
{
    private Vector3 _lastPosition;

    private Tween _tween;

    public Dodge(Vector3 lookingDirection, CharacterController characterController)
    {
        Vector3 dodgingDirection = lookingDirection;

        _tween = DOVirtual.Vector3(
            Vector3.zero,
            dodgingDirection * Configs.Character.DodgeRange,
            Configs.Character.DodgeDuration,
            result =>
            {
                characterController.Move(result - _lastPosition);
                PositionChanged?.Invoke();
                _lastPosition = result;
            }).
            SetRelative().OnComplete(() => DodgeFinished?.Invoke());
    }

    public void Interrupt()
    {
        _tween?.Kill();
        _tween = null;
        DodgeFinished?.Invoke();
    }

    public event Action PositionChanged;
    public event Action DodgeFinished;
}