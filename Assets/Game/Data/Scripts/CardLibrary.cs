using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "CardLibrary", menuName = "CardMatch/Card Library")]
public class CardLibrary : ScriptableObject
{
    [Tooltip("List of addressable CardData references")]
    public List<CardData> cards = new();
}
