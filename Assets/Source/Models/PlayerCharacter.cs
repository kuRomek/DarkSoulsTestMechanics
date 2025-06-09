using UnityEngine;

public class PlayerCharacter : Attacker
{
    public void Move(Vector3 direction, Quaternion additionalRotation, float speed)
    {
        if (IsInputEnabled)
        {
            Vector3 lastPosition = Position.Value;
            Move(additionalRotation * direction * speed);

            direction = Position.Value - lastPosition;

            if (direction != Vector3.zero)
                Rotation.Value = Quaternion.LookRotation(direction);
        }
    }
}