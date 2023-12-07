using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Default Card", menuName = "Inventory System/Cards/Greek")]
public class GreekCard : Card
{
    private void Awake()
    {
        mythology = CardType.Greek;
    }
}
