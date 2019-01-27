using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{

    public List<ItemPrefabConfig> prefabs;

    // Start is called before the first frame update
    void Awake()
    {
        The.gameLogic = this;
    }

    public GameObject GetPrefabByKind(IngredientKind kind)
    {
        for(int i = 0; i < prefabs.Count; i++)
        {
            if (prefabs[i].itemKind == kind) return prefabs[i].prefab;
        }
        return null;
    }

    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class ItemPrefabConfig
{
    public IngredientKind itemKind;
    public GameObject prefab;
    public Sprite sprite;
}