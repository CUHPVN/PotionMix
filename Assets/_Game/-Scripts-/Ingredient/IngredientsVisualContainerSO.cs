using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "IngredientsVisualContainerSO",menuName = "SO/IngredientsVisualContainerSO")]
public class IngredientsVisualContainerSO : ScriptableObject
{
    [SerializeField] private List<Sprite> spriteRenderers = new();
    public List<Sprite> visual = new();

    [ContextMenu("Shuffle")]
    public void Shuffle()
    {
        visual = new List<Sprite>(spriteRenderers);
        ListExtensions.Shuffle(visual);
    }
    public Sprite GetVisual(int index)
    {
        if(index >= visual.Count) return null;
        return visual[index];
    }
}

