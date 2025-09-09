using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientVisual : MonoBehaviour
{
    [SerializeField] Material chosedMaterial;
    [SerializeField] Material normalMaterial;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] int chosedorderLayer = 10;
    [SerializeField] int normalorderlayer = 5;
    public void IsChosed()
    {
        spriteRenderer.material = chosedMaterial;
        spriteRenderer.sortingOrder = chosedorderLayer;
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
    }
    public void NotChosed()
    {
        spriteRenderer.material = normalMaterial;
        spriteRenderer.sortingOrder = normalorderlayer;
        transform.localScale = Vector3.one;
    }
    public void SetSprite(Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}
