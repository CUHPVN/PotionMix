using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : Singleton<LevelManager> 
{
    [SerializeField] private int width=3;
    [SerializeField] private int height=3;
    [SerializeField] private float offset=1.5f;
    public IngredientsVisualContainerSO IngredientsVisualContainerSO;
    private void Awake()
    {
        IngredientsVisualContainerSO.Shuffle();
    }
    void Start()
    {
        for(int i = 0; i < height; i++)
        {
            for(int j= 0; j < width; j++)
            {
                Vector2 pos = new Vector2(j - (float)width / 2+0.5f, i - (float)height / 2+0.5f)*offset;
                Cauldren cauldren = SimplePool.Spawn<Cauldren>(PoolType.Cauldren, pos, Quaternion.identity);
                cauldren.OnInit();
            }
        }
    }

    void Update()
    {
        
    }
}
