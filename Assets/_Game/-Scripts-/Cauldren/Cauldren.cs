using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cauldren : GameUnit
{
    [SerializeField] private int size = 3;
    [SerializeField] private List<CauldrenSlot> slots = new List<CauldrenSlot>();
    [SerializeField] private List<Transform> queueingPositions = new();
    public void OnInit()
    {
        SpawnIngredient();
    }

    private void SpawnIngredient()
    {
        foreach  (var slot in slots) 
        {
            Ingredient ingredient=null;
            if(Random.Range(0, 2)==1) ingredient = SimplePool.Spawn<Ingredient>(PoolType.Ingredient, slot.transform.position, Quaternion.identity);
            slot.SetIngredientInstance(ingredient);
            slot.SetParent(this);
        }
        for (int i = 0; i < size; i++)
        {
            QueueIngredient ingredient = SimplePool.Spawn<QueueIngredient>(PoolType.QueueIngredient, queueingPositions[i].position, Quaternion.identity);
        }
    }
    public CauldrenSlot GetEmptySlot()
    {
        foreach(var slot in slots)
        {
            if(slot.GetIngredient() == null)
            {
                return slot;
            }
        }
        return null;
    }
    public bool CheckHaveSlot()
    {
        foreach(var slot in slots)
        {
            if(slot.GetIngredient() == null)
            {
                return true;
            }
        }
        return false;
    }
    public bool FindSlot(CauldrenSlot cauldrenSlot)
    {
        foreach(var slot in slots)
        {
            if (cauldrenSlot == slot) return true;
        }
        return false;
    }
    public CauldrenSlot GetClosesSlot(CauldrenSlot callSlot, Vector2 pos)
    {
        float min = Vector2.Distance(pos, callSlot.transform.position);
        CauldrenSlot tmslot = callSlot;
        foreach(CauldrenSlot slot in slots)
        {
            if (slot.GetIngredient() == null&&Vector2.Distance(slot.transform.position, pos) < min)
            {
                min = Vector2.Distance(slot.transform.position, pos);
                tmslot = slot;
            }
        }
        return tmslot;
    }
}
