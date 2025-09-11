using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : Singleton<InputHandler>
{
    public InputPhase inputPhase=InputPhase.None;
    public InputType inputType=InputType.None;
    private Camera _camera;
    private Vector2 startPos;
    private Vector2 currentPos;
    public Vector2 delta;


    private void Awake()
    {
        _camera = Camera.main;

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inputPhase = InputPhase.OnMouseDown;
        }
        else if (Input.GetMouseButton(0)) { inputPhase = InputPhase.OnMouse; } 
        else if(Input.GetMouseButtonUp(0))
        {
            inputPhase = InputPhase.OnMouseUp;
        }
        else
        {
            inputPhase = InputPhase.None;
        }
        UpdateDragVector();
        CheckType();
    }
    void UpdateDragVector()
    {
        if (inputPhase == InputPhase.OnMouseDown)
        {
            startPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        if(inputPhase != InputPhase.None) currentPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        delta = currentPos - startPos;
    }
    void CheckType()
    {
        if(delta.sqrMagnitude > 0.1f)
        {
            inputType = InputType.Drag;
        }
        if (inputPhase==InputPhase.OnMouseDown)
            if (inputType != InputType.Selected) inputType = InputType.Selected;
        if (inputPhase==InputPhase.OnMouseUp) 
            if (inputType == InputType.Drag) inputType = InputType.None;

    }
    public Vector2 GetDragVector() => delta;
    public Vector2 GetCurrentMousePos() => currentPos;
}
public enum InputPhase
{
    None=0,
    OnMouseDown=1,
    OnMouse=2,
    OnMouseUp=3,
}
public enum InputType
{
    None=0,
    Selected=1,
    Drag=2,
    EndDrag=3,
}
