using UnityEngine;

[CreateAssetMenu(fileName = nameof(Character), menuName = "Configs/Character")]
public class CharacterConfig : ScriptableObject
{
    [field: SerializeField, Min(0f)] public float Speed { get; private set; }
    [field: SerializeField, Range(0f, 500f)] public float RotationSensitivity { get; private set; }
    [field: SerializeField, Range(0f, 5f)] public float DodgeRange { get; private set; }
    [field: SerializeField, Range(0f, 5f)] public float DodgeDuration { get; private set; }
}
