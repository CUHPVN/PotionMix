using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragVector : Singleton<DragVector>
{
    private Camera m_Camera;
    [SerializeField] private Vector2 startPos;
    [SerializeField] private Vector2 currentPos;
    [SerializeField] private Vector2 endPos;
    public Vector2 delta;
    bool m_IsDragging = false;

    private void Awake()
    {
        m_Camera = Camera.main;
    }
    void Update()
    {
        Run();
    }
    void Run()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            m_IsDragging = true;
        }
        if(m_IsDragging) currentPos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
        if(m_IsDragging) Debug.DrawLine(startPos, currentPos);
        if (Input.GetMouseButtonUp(0))
        {
            endPos = m_Camera.ScreenToWorldPoint(Input.mousePosition);
            m_IsDragging = false;
        }
        delta = currentPos - startPos;
    }
}
