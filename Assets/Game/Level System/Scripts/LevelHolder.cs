using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelHolder", menuName = "CardMatch/Level Holder")]
public class LevelHolder : ScriptableObject
{
    public List<LevelConfig> levels = new();
}
