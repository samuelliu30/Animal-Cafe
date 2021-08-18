using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "New MoneyData", menuName = "Money Data")]

public class InventoryManager : ScriptableObject
{
    private int money = 0;
    public void ChangeMoney(int amount)
    {
        money += amount;
    }

    public int Money
    {
        get
        {
            return money;
        }
    }
    
}
