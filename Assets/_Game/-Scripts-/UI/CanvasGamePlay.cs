using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasGamePlay : UICanvas
{
    public void AddPoint()
    {
        Debug.Log("Add");
    }
    public void SettingsButton()
    {
        Close(0);
        UIManager.Instance.OpenUI<CanvasSettings>();
    }
    public void ReloadSceneButton()
    {
        SceneManager.LoadScene("Game");
    }
}
