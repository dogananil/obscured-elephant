using UnityEngine;

/// <summary>
/// Stores grid configuration for a specific level.
/// </summary>
[CreateAssetMenu(fileName = "Level", menuName = "CardMatch/Level Config")]
public class LevelConfig : ScriptableObject
{
    public int rows = 2;
    public int columns = 2;
}
