using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGui : MonoBehaviour
{
    public GameObject taskParent;
    public GameObject taskItem;

    // Start is called before the first frame update
    void Awake()
    {
        The.gameGui = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
