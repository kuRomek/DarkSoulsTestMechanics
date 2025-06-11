using UnityEngine;

public static class CharacterAnimatorData
{
    public static class Params
    {
        public static readonly int MovingMagnitude = Animator.StringToHash(nameof(MovingMagnitude));
        public static readonly int Dodge = Animator.StringToHash(nameof(Dodge));
        public static readonly int DodgeSpeed = Animator.StringToHash(nameof(DodgeSpeed));
        public static readonly int Attack = Animator.StringToHash(nameof(Attack));
    }
}