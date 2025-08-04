using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "CardMatch/Card Data")]
public class CardData : ScriptableObject
{
    public int id;         // Unique ID for matching logic
    public Sprite icon;    // Visual representation
}
