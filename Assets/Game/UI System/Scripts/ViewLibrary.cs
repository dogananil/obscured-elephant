using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ViewLibrary", menuName = "CardMatch/View Library")]
public class ViewLibrary : ScriptableObject
{
    [Tooltip("Views")]
    public List<View> Views = new();
}
