using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Default Card", menuName = "Inventory System/Cards/Default")]
public class DefaultCard : Card
{
    public void Awake()
    {
       mythology = CardType.Default;
    }
}
