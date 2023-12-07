using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]
public class InventoryObj : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();
    public void AddItem(Card _card, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; ++i)
        {
            if (Container[i].card == _card)
            {
                Container[i].AddAmount(_amount);
                hasItem = true;
                break;
            }
        
        }
        if (!hasItem) 
        {
            Container.Add(new InventorySlot(_card, _amount)); 
        }
    }
}

[System.Serializable]
public class InventorySlot
{
    public Card card;
    public int amount;
    public InventorySlot(Card _card, int _amount)
    {
        card = _card;
        amount = _amount;
    }
    public void AddAmount(int value)
    {
        amount += value;
    }
}
