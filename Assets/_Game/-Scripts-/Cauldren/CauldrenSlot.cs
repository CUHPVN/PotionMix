using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldrenSlot : MonoBehaviour
{
    private Cauldren cauldrenParent;
    private Ingredient ingredient;
    private int ingredientID;

    public Ingredient GetIngredient()
    {
        return ingredient;
    }
    public void SetIngredient(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        if (ingredient == null) return;
        ingredient.ChangePos(transform.position);
        
    }
    public bool HaveEmptySlotFormParent()
    {
        return cauldrenParent.GetEmptySlot()!=null;
    }
    public CauldrenSlot GetEmptySlotFormParent()
    {
        return cauldrenParent.GetEmptySlot();
    }
    public void SetIngredientInstance(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        if (ingredient == null) return;
        ingredient.ChangePosInstance(transform.position);
    }
    public void SetParent(Cauldren cauldren)
    {
        cauldrenParent=cauldren;
    }
    public int GetIngredientID() {  return ingredientID; }
    public void SetIngredientID(int id) { ingredientID = id; }
}
