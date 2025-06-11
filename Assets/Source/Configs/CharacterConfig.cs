using UnityEngine;

[CreateAssetMenu(fileName = nameof(Character), menuName = "Configs/Character")]
public class CharacterConfig : ScriptableObject
{
    [SerializeField] private AnimationClip _dodgeAnimationClip;

    [field: SerializeField, Min(0f)] public float Speed { get; private set; }
    [field: SerializeField, Range(0f, 500f)] public float RotationSensitivity { get; private set; }
    [field: SerializeField, Range(0f, 15f)] public float DodgeRange { get; private set; }
    [field: SerializeField, Range(0f, 5f)] public float DodgeDuration { get; private set; }

    public float DodgeAnimationDuration => _dodgeAnimationClip.length;
}
