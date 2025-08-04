using System;
using UnityEngine;

public class BoardView : View
{
    public RectTransform CardContainer => _cardContainer;
    [SerializeField]private RectTransform _cardContainer;
}
