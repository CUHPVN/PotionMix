using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private LayerMask slotLayer;
    [SerializeField] private LayerMask cauldrenLayer;

    [SerializeField] private CauldrenSlot currentSlot;
    [SerializeField] private Ingredient currentIngredient;

    [SerializeField] public bool isSelected=false;
    [SerializeField] public bool isFocused=false;

    private void Awake()
    {
    }

    private void Update()
    {
        if (InputHandler.Instance.inputPhase == InputPhase.OnMouseDown) StartDrag();
        else if(InputHandler.Instance.inputPhase == InputPhase.OnMouse) CheckDrag();
        else if (InputHandler.Instance.inputPhase == InputPhase.OnMouseUp) EndDrag();
    }

    private void StartDrag()
    {
        CauldrenSlot hitSlot = GetSlotUnderMouse();
        if (hitSlot == null)
        {
            return;
        }
        isFocused = true;
        //Ingredient oldIngredient = beforeSlot ? beforeSlot.GetIngredient() : null;
        Ingredient ingredient = hitSlot.GetIngredient();

        if (InputHandler.Instance.inputType == InputType.Selected)
        {
            if (!isSelected)
            {
                if (ingredient == null) return;
                currentSlot = hitSlot;
                currentIngredient = ingredient;
                currentIngredient.IsChosed();
                isSelected = true;
            }
            else
            {
                if (ingredient == null)
                {
                    HandleDropToSlot(currentIngredient, currentSlot, hitSlot);
                    Release();
                    isSelected = false;
                }
                else
                {
                    currentIngredient.MoveFail();
                    currentSlot = hitSlot;
                    currentIngredient = ingredient;
                    currentIngredient.IsChosed();
                    isSelected = true;
                }
            }
        }
    }
    private void CheckDrag()
    {
        if(InputHandler.Instance.inputType == InputType.Drag&& isFocused)
        {
            isSelected = false;
        }
    }
    private void EndDrag()
    {
        isFocused = false;
        if (isSelected || currentIngredient==null) return;
        CauldrenSlot hitSlot = GetSlotUnderMouse();
        if (hitSlot == null)
        {
            Cancel();
            Release();
            return;
        }
        Ingredient ingredient= hitSlot.GetIngredient();
        if(HandleDropToSlot(currentIngredient, currentSlot, hitSlot))
        {
            Release();
        }

    }

    private bool HandleDropToSlot(Ingredient ingredient, CauldrenSlot fromSlot, CauldrenSlot toSlot)
    {
        if (fromSlot == toSlot)
        {
            ingredient.MoveFail();
            return false;
        }
        else if (toSlot.GetIngredient() == null)
        {
            SwapIngredient(fromSlot, toSlot);
        }
        else if (toSlot.GetEmptySlotFormParent()!=null)
        {
            SwapIngredient(fromSlot, toSlot.GetEmptySlotFormParent());
        }
        else
        {
            ingredient.MoveFail();
            return false;
        }
        return true;
    }


    private CauldrenSlot GetSlotUnderMouse()
    {
        Vector2 pos = InputHandler.Instance.GetCurrentMousePos();
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, slotLayer);
        return hit.collider ? hit.collider.GetComponent<CauldrenSlot>() : null;
    }


    private void SwapIngredient(CauldrenSlot a, CauldrenSlot b)
    {
        Ingredient temp = a.GetIngredient();
        a.SetIngredient(b.GetIngredient());
        b.SetIngredient(temp);
    }
    private void Cancel()
    {
        currentIngredient.MoveFail();
    }
    private void Release()
    {
        currentIngredient = null;
        currentSlot = null;
    }

}
