using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : GameUnit
{
    [SerializeField] private IngredientVisual visual;
    [SerializeField] private float moveSpeed = 10f;
    private int ingredientID;
    private float elapseTime=0;
    private bool chosed=false;
    private MoveState moveState=MoveState.None;
    private Vector3 newPos;
    private Vector3 latePos;
    public void Awake()
    {
        OnInit();
    }
    public void OnInit()
    {
        ingredientID = Random.Range(0, LevelManager.Instance.IngredientsVisualContainerSO.visual.Count);
        SetSprite(ingredientID);
    }
    public void IsChosed()
    {
        chosed = true;
        latePos = transform.position;
        visual.IsChosed();
    }
    
    public void NotChosed()
    {
        chosed=false;
        visual.NotChosed();
    }
    private void Update()
    {
        OnChosed();
        MoveUpdate();
    }
    public void OnChosed()
    {
        if (!chosed) return;
        if (PlayerManager.Instance.dragState==PlayerManager.DragState.Dragging)
        {
            transform.position = latePos + (Vector3)DragVector.Instance.delta;
        }
    }
    public void ReturnCauldren()
    {
        NotChosed();
        MoveToNewPos();
    }
    public void MoveUpdate()
    {
        if(moveState==MoveState.Move)
        {
            elapseTime += Time.deltaTime;
            transform.position = Vector2.Lerp(latePos, newPos, elapseTime * moveSpeed);
            if (Vector2.Distance(transform.position, newPos) < 0.1f)
            {
                latePos = newPos;
                transform.position = latePos;
                moveState = MoveState.None;
                elapseTime = 0;
            }
        }
    }
    public void MoveToNewPos()
    {
        latePos = transform.position;
        elapseTime = 0;
        moveState = MoveState.Move;

    }
    public void ChangePos(Vector2 pos)
    {
        newPos = pos;
        NotChosed();
        MoveToNewPos();
    }
    public void ChangePosInstance(Vector2 pos)
    {
        newPos = pos;
        NotChosed();
        transform.position= newPos;
    }
    public void SetSprite(int id)
    {
        visual.SetSprite(LevelManager.Instance.IngredientsVisualContainerSO.visual[id]);
    }
    public enum MoveState
    {
        None=0,
        Move=1,
    }
}
