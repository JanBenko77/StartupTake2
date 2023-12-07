using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Card", menuName = "Inventory System/Cards/Japanese")]
public class JapaneseCard : Card
{
    private void Awake()
    {
        mythology = CardType.Japanese;
    }
}
