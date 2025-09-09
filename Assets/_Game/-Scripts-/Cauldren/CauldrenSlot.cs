using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CauldrenSlot : MonoBehaviour
{
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
    public void SetIngredientInstance(Ingredient ingredient)
    {
        this.ingredient = ingredient;
        if (ingredient == null) return;
        ingredient.ChangePosInstance(transform.position);
    }
    public int GetIngredientID() {  return ingredientID; }
    public void SetIngredientID(int id) { ingredientID = id; }
}
