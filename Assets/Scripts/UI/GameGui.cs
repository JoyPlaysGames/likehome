using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGui : MonoBehaviour
{
    public GameObject taskParent;
    public GameObject taskItem;


    public Text levelTimer;
    public Text levelScore;

    public Sprite recipieIcon;
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
