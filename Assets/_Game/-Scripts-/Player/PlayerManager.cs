using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask slotLayer;
    [SerializeField] private LayerMask cauldrenLayer;

    private CauldrenSlot currentSlot;
    private CauldrenSlot beforeSlot;

    public DragState dragState = DragState.None;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) StartDrag();
        CheckSelected();
        if (Input.GetMouseButtonUp(0)) EndDrag();
    }

    private void StartDrag()
    {
        CauldrenSlot hitSlot = GetSlotUnderMouse();
        if (hitSlot == null) return;

        Ingredient oldIngredient = beforeSlot ? beforeSlot.GetIngredient() : null;
        Ingredient ingredient = hitSlot.GetIngredient();

        if (dragState == DragState.ClickSelected)
        {
            HandleClickDrop(ingredient);
            ResetDrag();
        }
        else
        {
            if (ingredient == null) return;
            if (hitSlot != beforeSlot)
            {
                dragState = DragState.ClickSelected;
                ingredient.IsChosed();
                beforeSlot = hitSlot;
                currentSlot = hitSlot;
            }
            else
            {
                dragState = DragState.ClickSelected;
                beforeSlot = null;
                currentSlot = hitSlot;
            }
        }
        if (oldIngredient != null) CancelDrag(oldIngredient);
    }
    void CheckSelected()
    {
        if (dragState == DragState.ClickSelected)
        {
            if (DragVector.Instance.delta.magnitude > 0.2f)
            {
                dragState = DragState.Dragging;
            }
        }
    }
    private void EndDrag()
    {
        Ingredient ingredient = currentSlot ? currentSlot.GetIngredient() : null;

        if (ingredient == null)
        {
            ResetDrag();
            return;
        }
        if (dragState == DragState.Dragging)
        {
            ingredient.NotChosed();
            CauldrenSlot hitSlot = GetSlotUnderMouse();
            Cauldren hitCauldren = GetCauldrenUnderMouse();

            if (hitSlot != null)
            {
                HandleDropToSlot(ingredient, currentSlot, hitSlot, hitCauldren);
            }
            else if (hitCauldren != null)
            {
                HandleDropToCauldren(ingredient, currentSlot, hitCauldren);
            }
            else
            {
                ingredient.ReturnCauldren();
            }

            ResetDrag();
        }
    }

    private void HandleClickDrop(Ingredient ingredient)
    {
        CauldrenSlot hitSlot = GetSlotUnderMouse();
        Cauldren hitCauldren = GetCauldrenUnderMouse();

        if (hitSlot != null)
        {
            HandleDropToSlot(ingredient, currentSlot, hitSlot, hitCauldren);
        }
        else if (hitCauldren != null)
        {
            HandleDropToCauldren(ingredient, currentSlot, hitCauldren);
        }
        else
        {
            ingredient.ReturnCauldren();
        }
    }



    private void HandleDropToSlot(Ingredient ingredient, CauldrenSlot fromSlot, CauldrenSlot toSlot, Cauldren cauldren)
    {
        if (fromSlot == toSlot)
        {
            ingredient.ReturnCauldren();
        }
        else if (toSlot.GetIngredient() == null)
        {
            SwapIngredient(fromSlot, toSlot);
        }
        else if (cauldren != null && cauldren.CheckHaveSlot())
        {
            SwapIngredient(fromSlot, cauldren.GetEmptySlot());
        }
        else
        {
            ingredient.ReturnCauldren();
        }
    }

    private void HandleDropToCauldren(Ingredient ingredient, CauldrenSlot fromSlot, Cauldren cauldren)
    {
        if (cauldren.FindSlot(fromSlot))
        {
            ingredient.ReturnCauldren();
        }
        else if (cauldren.CheckHaveSlot())
        {
            SwapIngredient(fromSlot, cauldren.GetEmptySlot());
        }
        else
        {
            ingredient.ReturnCauldren();
        }
    }

    private CauldrenSlot GetSlotUnderMouse()
    {
        Vector2 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, slotLayer);
        return hit.collider ? hit.collider.GetComponent<CauldrenSlot>() : null;
    }

    private Cauldren GetCauldrenUnderMouse()
    {
        Vector2 pos = _camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, Mathf.Infinity, cauldrenLayer);
        return hit.collider ? hit.collider.transform.parent.GetComponent<Cauldren>() : null;
    }

    private void SwapIngredient(CauldrenSlot a, CauldrenSlot b)
    {
        Ingredient temp = a.GetIngredient();
        a.SetIngredient(b.GetIngredient());
        b.SetIngredient(temp);
    }

    private void CancelDrag(Ingredient ingredient)
    {
        ingredient.NotChosed();
        ingredient.ReturnCauldren();
        ResetDrag();
    }

    private void ResetDrag()
    {
        dragState = DragState.None;
        beforeSlot = null;
        currentSlot = null;
    }   

    public enum DragState { None=0, Dragging=1, ClickSelected=2 };
}
