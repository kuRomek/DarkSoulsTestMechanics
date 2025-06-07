using UnityEngine;

public static class Configs
{
    public static CharacterConfig Character => Resources.Load(nameof(Character)) as CharacterConfig;
}