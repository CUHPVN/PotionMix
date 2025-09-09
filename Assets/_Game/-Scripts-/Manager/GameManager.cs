using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        Input.multiTouchEnabled = false;
        Application.targetFrameRate = 60;
    }
    void Start()
    {
        UIManager.Instance.OpenUI<CanvasGamePlay>();
    }

    void Update()
    {
        
    }
}
