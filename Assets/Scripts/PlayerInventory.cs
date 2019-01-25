using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public Dictionary<IngredientKind, int> slots = new Dictionary<IngredientKind, int>();

    private void Awake()
    {
        The.playerInventory = this;
    }

    public void CollectItem(IngredientKind kind, int count)
    {
        slots[kind] = slots.ContainsKey(kind) ? slots[kind] += count : count;
    }
    
    public bool CanAfford(IngredientKind kind, int count)
    {
        return slots.ContainsKey(kind) && slots[kind] >= count;
    }

    public bool Consume(IngredientKind kind, int count)
    {
        if(slots.ContainsKey(kind) && slots[kind] >= count)
        {
            slots[kind] -= count;
            return true;
        }
        return false;
    }
    
}

